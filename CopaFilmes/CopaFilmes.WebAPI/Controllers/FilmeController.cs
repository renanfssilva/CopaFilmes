using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CopaFilmes.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ILogger<FilmeController> _logger;
        private IMapper _mapper;

        public FilmeController(IFilmeRepository filmeRepository, ILogger<FilmeController> logger, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("filmes")]
        [HttpGet]
        public IActionResult GetFilmes()
        {
            try
            {
                var filmes = _filmeRepository.GetFilmes();
                _logger.LogInformation("Todos os filmes da api foram consultados.");

                var filmesResult = _mapper.Map<IEnumerable<FilmeDto>>(filmes);
                return Ok(filmesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Não foi possível consultar todos os filmes.\n{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("vencedores")]
        [HttpGet]
        public IActionResult GetVencedores([FromBody]string[] idsSelecionados)
        {
            try
            {
                var filmesSelecionados = _filmeRepository.GetFilmesOrdenadosPorTitulo(idsSelecionados.ToList()).ToList();
                _logger.LogInformation("Todos os ids de filmes foram consultados.");

                var vencedores = ObtenhaVencedores(filmesSelecionados);

                var filmesResult = _mapper.Map<IEnumerable<FilmeDto>>(vencedores);
                return Ok(filmesResult);
            }
            catch (Exception)
            {
                _logger.LogError("Não foi possível descobrir quais foram os finalistas do campeonato.");
                return StatusCode(500, "Internal server error");
            }
        }

        private static IEnumerable<Filme> ObtenhaVencedores(List<Filme> filmesSelecionados)
        {
            var turnos = new List<Turno>();
            var quantidadeDeTurnos = (int)Math.Floor(Math.Log2(filmesSelecionados.Count) - 1);
            var quantidadeDeFilmes = filmesSelecionados.Count;

            for (var i = 0; i < quantidadeDeTurnos; ++i)
            {
                turnos.Add(new Turno());
                turnos[i].Disputas = new List<Disputa>();
                quantidadeDeFilmes = filmesSelecionados.Count;

                if (i == 0)
                {
                    for (var j = 0; j < quantidadeDeFilmes / (2 * (i + 1)); ++j)
                    {
                        turnos[i].Disputas.Add(new Disputa());
                        turnos[i].Disputas[j].Filme1 = filmesSelecionados[j];
                        turnos[i].Disputas[j].Filme2 = filmesSelecionados[quantidadeDeFilmes - j - 1];
                    }

                    filmesSelecionados = turnos[i].Disputas.Select(ObtenhaVencedorDaDisputa).ToList();
                }
                else
                {
                    for (var j = 0; j < quantidadeDeFilmes; j += 2)
                    {
                        turnos[i].Disputas.Add(new Disputa());
                        turnos[i].Disputas[j / 2].Filme1 = filmesSelecionados[j];
                        turnos[i].Disputas[j / 2].Filme2 = filmesSelecionados[j + 1];
                    }

                    filmesSelecionados = turnos[i].Disputas.Select(ObtenhaVencedorDaDisputa).ToList();
                }
            }

            return filmesSelecionados.OrderByDescending(filme => filme.Nota).ThenBy(filme => filme.Titulo);

            //Se a quantidade de filmes for sempre 8, pode ser utilizado o código comentado abaixo:

            //return new List<Filme>
            //{
            //    ObtenhaVencedorDaDisputa(new Disputa
            //    {
            //        Filme1 = ObtenhaVencedorDaDisputa(
            //            new Disputa
            //            {
            //                Filme1 = filmesSelecionados[0],
            //                Filme2 = filmesSelecionados[7]
            //            }),
            //        Filme2 = ObtenhaVencedorDaDisputa(
            //            new Disputa
            //            {
            //                Filme1 = filmesSelecionados[1],
            //                Filme2 = filmesSelecionados[6]
            //            })
            //    }),
            //    ObtenhaVencedorDaDisputa(new Disputa
            //    {
            //        Filme1 = ObtenhaVencedorDaDisputa(
            //            new Disputa
            //            {
            //                Filme1 = filmesSelecionados[2],
            //                Filme2 = filmesSelecionados[5]
            //            }),
            //        Filme2 = ObtenhaVencedorDaDisputa(
            //            new Disputa
            //            {
            //                Filme1 = filmesSelecionados[3],
            //                Filme2 = filmesSelecionados[4]
            //            })
            //    })
            //};
        }

        private static Filme ObtenhaVencedorDaDisputa(Disputa disputa)
        {
            if (disputa.Filme1.Nota > disputa.Filme2.Nota)
            {
                return disputa.Filme1;
            }

            if (disputa.Filme1.Nota < disputa.Filme2.Nota)
            {
                return disputa.Filme2;
            }

            return string.Compare(disputa.Filme1.Titulo, disputa.Filme2.Titulo, StringComparison.InvariantCultureIgnoreCase) < 0 ? disputa.Filme1 : disputa.Filme2;
        }
    }
}
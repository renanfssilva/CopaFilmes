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
        private const int QuantidadeDeTurnos = 3;
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

        private IEnumerable<Filme> ObtenhaVencedores(IReadOnlyList<Filme> filmesSelecionados)
        {
            return new List<Filme>
            {
                ObtenhaVencedorDaDisputa(new Disputa
                {
                    Filme1 = ObtenhaVencedorDaDisputa(
                        new Disputa
                        {
                            Filme1 = filmesSelecionados[0],
                            Filme2 = filmesSelecionados[7]
                        }),
                    Filme2 = ObtenhaVencedorDaDisputa(
                        new Disputa
                        {
                            Filme1 = filmesSelecionados[1],
                            Filme2 = filmesSelecionados[6]
                        })
                }),
                ObtenhaVencedorDaDisputa(new Disputa
                {
                    Filme1 = ObtenhaVencedorDaDisputa(
                        new Disputa
                        {
                            Filme1 = filmesSelecionados[2],
                            Filme2 = filmesSelecionados[5]
                        }),
                    Filme2 = ObtenhaVencedorDaDisputa(
                        new Disputa
                        {
                            Filme1 = filmesSelecionados[3],
                            Filme2 = filmesSelecionados[4]
                        })
                })
            };
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
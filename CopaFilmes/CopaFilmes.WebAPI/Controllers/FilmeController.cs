using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CopaFilmes.WebAPI.Controllers
{
    [ApiController]
    [Route("api/filmes")]
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

        [HttpGet("{id}")]
        public IActionResult GetVencedores(string filmesSelecionados)
        {
            try
            {
                var finalistas = new List<FilmeDto>();

                finalistas.Add(new FilmeDto
                {
                    Id = filmesSelecionados
                });

                finalistas.Add(new FilmeDto
                {
                    Id = filmesSelecionados
                });

                return Ok(finalistas);
            }
            catch (Exception)
            {
                _logger.LogError("Não foi possível descobrir quais foram os finalistas do campeonato.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
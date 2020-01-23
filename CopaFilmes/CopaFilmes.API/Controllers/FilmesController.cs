using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.API.Controllers
{
    [Route("api/[controller]")]
    public class FilmesController : Controller
    {
        private readonly FilmeContext _context;

        public FilmesController(FilmeContext context)
        {
            _context = context;
        }

        // GET api/valores
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var movies = _context.Filmes.ToList();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Alguma coisa deu errado: {ex.Message}");
            }
        }
    }
}

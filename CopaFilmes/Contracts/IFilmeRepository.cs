using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IFilmeRepository
    {
        IEnumerable<Filme> GetFilmes();
        IEnumerable<Filme> GetFilmesOrdenadosPorTitulo(IEnumerable<string> id);
    }
}

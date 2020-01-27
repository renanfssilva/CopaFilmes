using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        public IEnumerable<Filme> GetFilmes()
        {
            var client = new RestClient("http://copafilmes.azurewebsites.net/api/filmes");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (!response.IsSuccessful) return null;

            var content = JsonConvert.DeserializeObject<JToken>(response.Content);

            //Get the movies list from api
            var filmes = content.Select(filme => new Filme
            {
                Id = (string)filme["id"],
                Titulo = (string)filme["titulo"],
                Ano = (int)filme["ano"],
                Nota = (double)filme["nota"]
            }).ToList();

            return filmes;
        }

        public IEnumerable<Filme> GetFilmesPorId(string[] filmesIds)
        {
            var todosFilmes = GetFilmes();

            return (from filme in todosFilmes
                    from id in filmesIds
                    where filme.Id.Equals(id)
                    select filme
                    ).ToList();
        }
    }
}

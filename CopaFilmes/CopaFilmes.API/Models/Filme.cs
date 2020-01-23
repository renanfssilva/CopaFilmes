using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopaFilmes.API.Models
{
    [Table("Filme")]
    public class Filme
    {
        [Key]
        public string Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public float Nota { get; set; }
    }
}

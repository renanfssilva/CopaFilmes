using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Filme
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Ano é obrigatório.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Nota é obrigatória.")]
        public double Nota { get; set; }
    }
}

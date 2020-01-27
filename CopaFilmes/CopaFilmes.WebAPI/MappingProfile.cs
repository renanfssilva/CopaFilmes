using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace CopaFilmes.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Filme, FilmeDto>();
        }
    }
}

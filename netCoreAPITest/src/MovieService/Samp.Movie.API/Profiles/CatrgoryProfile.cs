using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class CatrgoryProfile : Profile
    {
        public CatrgoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>();
        }
    }
}
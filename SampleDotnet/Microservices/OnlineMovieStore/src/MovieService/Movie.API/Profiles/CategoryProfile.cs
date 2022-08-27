using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>();
        }
    }
}
using AutoMapper;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.Database.Entities;

namespace SampleProject.Movie.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>();
        }
    }
}
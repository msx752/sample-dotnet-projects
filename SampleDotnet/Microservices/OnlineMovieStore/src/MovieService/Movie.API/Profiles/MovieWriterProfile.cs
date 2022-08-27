using AutoMapper;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.Database.Entities;

namespace SampleProject.Movie.API.Profiles
{
    public class MovieWriterProfile : Profile
    {
        public MovieWriterProfile()
        {
            CreateMap<MovieEntity, MovieDto>();
            CreateMap<WriterEntity, WriterDto>();
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<DirectorEntity, DirectorDto>();
            CreateMap<MovieCategoryEntity, MovieCategoryDto>();
            CreateMap<MovieDirectorEntity, MovieDirectorDto>();
            CreateMap<MovieWriterEntity, MovieWriterDto>();
        }
    }
}
using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class MovieDirectorProfile : Profile
    {
        public MovieDirectorProfile()
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
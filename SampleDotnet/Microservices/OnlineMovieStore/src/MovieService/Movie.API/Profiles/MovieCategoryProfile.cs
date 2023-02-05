﻿using AutoMapper;
using Movie.Database.Entities;
using SampleProject.Movie.API.Models.Dtos;

namespace SampleProject.Movie.API.Profiles
{
    public class MovieCategoryProfile : Profile
    {
        public MovieCategoryProfile()
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
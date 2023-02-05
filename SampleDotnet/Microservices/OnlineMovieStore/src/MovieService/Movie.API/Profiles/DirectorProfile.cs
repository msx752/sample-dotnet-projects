using AutoMapper;
using Movie.Database.Entities;
using SampleProject.Movie.API.Models.Dtos;

namespace SampleProject.Movie.API.Profiles
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<DirectorEntity, DirectorDto>();
        }
    }
}
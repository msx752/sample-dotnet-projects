using AutoMapper;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.Database.Entities;

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
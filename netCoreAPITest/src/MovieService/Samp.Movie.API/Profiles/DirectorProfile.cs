using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<DirectorEntity, DirectorDto>();
        }
    }
}
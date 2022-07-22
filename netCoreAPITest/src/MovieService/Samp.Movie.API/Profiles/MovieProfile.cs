using AutoMapper;
using Samp.Contract.Cart.Responses;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieEntity, MovieDto>();
            CreateMap<MovieEntity, MovieEntityResponseMessage>();
        }
    }
}
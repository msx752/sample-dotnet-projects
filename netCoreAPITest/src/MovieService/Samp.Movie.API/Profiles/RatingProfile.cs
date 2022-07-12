using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Models
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RatingEntity, RatingDto>();
        }
    }
}
using AutoMapper;
using Movie.Database.Entities;
using SampleProject.Movie.API.Models.Dtos;

namespace SampleProject.Movie.API.Models
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RatingEntity, RatingDto>();
        }
    }
}
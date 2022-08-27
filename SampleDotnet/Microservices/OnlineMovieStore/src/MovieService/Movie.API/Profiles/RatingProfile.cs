using AutoMapper;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.Database.Entities;

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
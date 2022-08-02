using AutoMapper;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.Database.Entities;

namespace Samp.Movie.API.Profiles
{
    public class WriterProfile : Profile
    {
        public WriterProfile()
        {
            CreateMap<WriterEntity, WriterDto>();
        }
    }
}
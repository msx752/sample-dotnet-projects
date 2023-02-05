using AutoMapper;
using Movie.Database.Entities;
using SampleProject.Movie.API.Models.Dtos;

namespace SampleProject.Movie.API.Profiles
{
    public class WriterProfile : Profile
    {
        public WriterProfile()
        {
            CreateMap<WriterEntity, WriterDto>();
        }
    }
}
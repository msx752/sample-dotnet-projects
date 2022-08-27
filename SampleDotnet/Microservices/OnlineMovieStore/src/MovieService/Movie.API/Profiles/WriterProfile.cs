using AutoMapper;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.Database.Entities;

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
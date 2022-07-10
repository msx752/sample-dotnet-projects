using AutoMapper;
using Dtnt.API.Personals.Models.Dtos;
using netCoreAPI.Database.Entities;
using netCoreAPI.Static.Requests;

namespace netCoreAPI.Profiles
{
    public class PersonalProfile : Profile
    {
        public PersonalProfile()
        {
            CreateMap<PersonalEntity, PersonalDto>();
            CreateMap<PersonalModel, PersonalEntity>();
        }
    }
}
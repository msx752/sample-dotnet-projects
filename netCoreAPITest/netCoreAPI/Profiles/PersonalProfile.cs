using AutoMapper;
using netCoreAPI.Controllers.Requests;
using netCoreAPI.Models.Dtos;
using netCoreAPI.Models.Entities;

namespace netCoreAPI.Profiles
{
    public class PersonalProfile : Profile
    {
        public PersonalProfile()
        {
            CreateMap<PersonalEntity, PersonalDto>();
            CreateMap<PersonalRequest, PersonalEntity>();
        }
    }
}
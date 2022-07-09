using AutoMapper;
using netCoreAPI.Controllers.Dtos;
using netCoreAPI.Controllers.Requests;
using netCoreAPI.Data.Entities;

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
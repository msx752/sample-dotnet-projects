using AutoMapper;
using Samp.API.Personal.Models.Dtos;
using Samp.API.Personal.Models.Requests;
using Samp.Database.Personal.Entities;

namespace Samp.API.Personal.Profiles
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
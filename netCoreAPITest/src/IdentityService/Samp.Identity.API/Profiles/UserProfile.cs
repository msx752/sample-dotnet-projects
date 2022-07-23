using AutoMapper;
using Samp.Identity.API.Models.Dto;
using Samp.Identity.API.Models.Requests;
using Samp.Identity.Database.Entities;

namespace Samp.Identity.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserCreateModel, UserEntity>();
            CreateMap<UserUpdateModel, UserEntity>();
        }
    }
}
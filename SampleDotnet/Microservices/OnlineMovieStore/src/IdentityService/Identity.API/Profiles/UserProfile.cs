using AutoMapper;
using Identity.Database.Entities;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;

namespace SampleProject.Identity.API.Profiles
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
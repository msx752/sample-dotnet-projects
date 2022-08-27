using AutoMapper;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;
using SampleProject.Identity.Database.Entities;

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
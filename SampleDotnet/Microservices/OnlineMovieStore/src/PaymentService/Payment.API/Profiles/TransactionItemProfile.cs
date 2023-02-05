using AutoMapper;
using Payment.Database.Entities;
using SampleProject.Payment.API.Models.Dtos;

namespace SampleProject.Payment.API.Profiles
{
    public class TransactionItemProfile : Profile
    {
        public TransactionItemProfile()
        {
            CreateMap<TransactionItemEntity, TransactionItemDto>();
        }
    }
}
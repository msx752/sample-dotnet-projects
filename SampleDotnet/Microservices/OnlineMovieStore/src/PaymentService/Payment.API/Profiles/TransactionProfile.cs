using AutoMapper;
using Payment.Database.Entities;
using SampleProject.Payment.API.Models.Dtos;

namespace SampleProject.Payment.API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionEntity, TransactionDto>();
        }
    }
}
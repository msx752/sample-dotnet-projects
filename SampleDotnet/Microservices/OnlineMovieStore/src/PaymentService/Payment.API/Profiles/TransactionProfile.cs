using AutoMapper;
using SampleProject.Payment.API.Models.Dtos;
using SampleProject.Payment.Database.Entities;

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
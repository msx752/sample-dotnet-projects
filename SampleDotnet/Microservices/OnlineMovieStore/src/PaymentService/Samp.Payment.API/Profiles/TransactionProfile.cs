using AutoMapper;
using Samp.Payment.API.Models.Dtos;
using Samp.Payment.Database.Entities;

namespace Samp.Payment.API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionEntity, TransactionDto>();
        }
    }
}
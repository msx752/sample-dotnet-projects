using AutoMapper;
using Samp.Payment.API.Models.Dtos;
using Samp.Payment.Database.Entities;

namespace Samp.Payment.API.Profiles
{
    public class TransactionItemProfile : Profile
    {
        public TransactionItemProfile()
        {
            CreateMap<TransactionItemEntity, TransactionItemDto>();
        }
    }
}
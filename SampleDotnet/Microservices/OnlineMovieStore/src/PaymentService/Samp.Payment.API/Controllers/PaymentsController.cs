using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samp.Contract;
using Samp.Contract.Payment;
using Samp.Contract.Payment.Cart;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Result;
using Samp.Payment.API.Models.Dtos;
using Samp.Payment.Database.Entities;
using Samp.Payment.Database.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace Samp.ayment.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : BaseController
    {
        private readonly ISharedRepository<PaymentDbContext> repository;
        private readonly IMessageBus messageBus;

        public PaymentsController(
            IMapper _mapper
            , ISharedRepository<PaymentDbContext> repository
            , IMessageBus messageBus)
            : base(_mapper)
        {
            this.repository = repository;
            this.messageBus = messageBus;
        }

        [HttpGet("History")]
        public IActionResult PaymentHistory()
        {
            var transactionEntities = repository.Table<TransactionEntity>()
                   .Where(f => f.UserId == LoggedUserId)
                   .Include(f => f.TransactionItems.Where(x => !x.IsDeleted))
                   .ToList();

            return new OkResponse(mapper.Map<List<TransactionDto>>(transactionEntities));
        }

        [HttpPost("Create/{cartId}")]
        public async Task<IActionResult> CreatePayment(Guid cartId)
        {
            var lock_response = await messageBus.Call<CartStatusResponseMessage, CartStatusRequestMessage>(new()
            {
                CartStatus = "LockedOnPayment",
                CartId = cartId,
                ActivityUserId = LoggedUserId,
            });

            if (!string.IsNullOrEmpty(lock_response.Message.BusErrorMessage))
            {
                return new BadRequestResponse(lock_response.Message.BusErrorMessage);
            }
            else
            {
                var cartEntityResponse = await messageBus.Call<CartEntityResponseMessage, CartEntityRequestMessage>(new()
                {
                    ActivityUserId = LoggedUserId,
                    CartId = cartId,
                });

                if (!string.IsNullOrEmpty(cartEntityResponse.Message.BusErrorMessage))
                {
                    return new BadRequestResponse(cartEntityResponse.Message.BusErrorMessage);
                }

                TransactionEntity transactionEntity = new TransactionEntity();
                transactionEntity.UserId = LoggedUserId;
                transactionEntity.CartId = cartId;

                double totalPrice = 0;
                foreach (var item in cartEntityResponse.Message.Items.GroupBy(f => f.ProductId))
                {
                    double calculatedPrice = Math.Round((item.Count() * item.First().SalesPrice), 2);
                    TransactionItemEntity transactionItemEntity = new TransactionItemEntity()
                    {
                        Transaction = transactionEntity,
                        ProductTitle = item.First().Title,
                        ProductId = item.First().ProductId,
                        ProductPrice = item.First().SalesPrice,
                        ProductPriceCurrency = item.First().SalesPriceCurrency,
                        Quantity = item.Count(),
                        CalculatedPrice = $"{calculatedPrice} {item.First().SalesPriceCurrency}",
                    };

                    transactionEntity.TransactionItems.Add(transactionItemEntity);

                    totalPrice += calculatedPrice;
                }
                transactionEntity.TotalCalculatedPrice = $"{totalPrice} {transactionEntity.TransactionItems.First().ProductPriceCurrency}";

                repository.Table<TransactionEntity>().Insert(transactionEntity);
                repository.Commit(LoggedUserId);

                var paid_response = await messageBus.Call<CartStatusResponseMessage, CartStatusRequestMessage>(new()
                {
                    CartStatus = "Paid",
                    CartId = cartId,
                    ActivityUserId = LoggedUserId,
                });

                if (!string.IsNullOrEmpty(paid_response.Message.BusErrorMessage))
                {
                    return new BadRequestResponse(paid_response.Message.BusErrorMessage);
                }

                return new OkResponse($"successfully paid '{transactionEntity.TotalCalculatedPrice}'");
            }
        }
    }
}
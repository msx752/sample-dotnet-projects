using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samp.Basket.Database.Entities;
using Samp.Basket.Database.Migrations;
using Samp.Cart.API.Models.Dtos;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;

namespace Samp.Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : BaseController
    {
        private readonly ISharedRepository<CartDbContext> repository;

        public BasketsController(
            IMapper mapper
            , ISharedRepository<CartDbContext> repository)
            : base(mapper)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetBasket()
        {
            var entity = repository.Table<CartEntity>()
                .Where(f => f.UserId == LoggedUserId)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new CartEntity
                {
                    UserId = LoggedUserId,
                };
                repository.Table<CartEntity>().Insert(entity);
                repository.Commit(LoggedUserId);
            }
            return new OkResponse(mapper.Map<BasketDto>(entity));
        }
    }
}
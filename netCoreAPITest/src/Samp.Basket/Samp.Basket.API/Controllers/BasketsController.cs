using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samp.Basket.API.Models.Dtos;
using Samp.Basket.Database.Entities;
using Samp.Basket.Database.Migrations;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;

namespace Samp.Basket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : BaseController
    {
        private readonly ISharedRepository<BasketDbContext> repository;

        public BasketsController(
            IMapper mapper
            , ISharedRepository<BasketDbContext> repository)
            : base(mapper)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetBasket()
        {
            var entity = repository.Table<BasketEntity>()
                .Where(f => f.UserId == LoggedUserId)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new BasketEntity
                {
                    UserId = LoggedUserId,
                };
                repository.Table<BasketEntity>().Insert(entity);
                repository.Commit(LoggedUserId);
            }
            return new OkResponse(mapper.Map<BasketDto>(entity));
        }
    }
}
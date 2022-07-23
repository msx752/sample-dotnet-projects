using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samp.Contract;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;
using Samp.Payment.Database.Migrations;

namespace Samp.ayment.API.Controllers
{
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
            return new OkResponse("not implemented yet");
        }

        [HttpGet("Create")]
        public IActionResult CreatePayment()
        {
            return new OkResponse("not implemented yet");
        }
    }
}
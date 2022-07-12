using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Samp.Auth.API.Models.Dto;
using Samp.Auth.API.Models.Requests;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;
using Samp.Identity.API.Helpers;
using Samp.Identity.Core.Migrations;
using Samp.Identity.Database.Entities;

namespace Samp.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : BaseController
    {
        private readonly ISharedRepository<IdentityDbContext> repository;
        private readonly ITokenHelper tokenHelper;

        public IdentityController(
            IMapper mapper
            , ISharedRepository<IdentityDbContext> repository
            , ITokenHelper tokenHelper)
            : base(mapper)
        {
            this.repository = repository;
            this.tokenHelper = tokenHelper;
        }

        [HttpPost("Token")]
        public ActionResult Token([FromForm] TokenRequest model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));
            }

            var user = repository.Table<UserEntity>()
                .Where(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password))
                .FirstOrDefault();

            if (user == null)
            {
                return new UnauthorizedResponse("invalid credentials.");
            }

            var response = tokenHelper.Authenticate(user);
            return new OkResponse(response);
        }
    }
}
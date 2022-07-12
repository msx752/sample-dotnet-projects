using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Security.Principal;

namespace Samp.Auth.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
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

            var claims = new[] {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Id.ToString()),
            };
            var response = tokenHelper.Authenticate(user, claims);
            return new OkResponse(response);
        }
    }
}
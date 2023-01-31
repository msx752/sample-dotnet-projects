using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Core.Model.Base;
using SampleProject.Identity.API.Helpers;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;
using SampleProject.Identity.Core.Migrations;
using SampleProject.Identity.Database.Entities;
using SampleProject.Result;
using System.Security.Claims;

namespace SampleProject.Identity.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly IUnitOfWork<IdentityDbContext> _uow;
        private readonly ITokenHelper _tokenHelper;

        public TokenController(
            IMapper mapper
            , IUnitOfWork<IdentityDbContext> uow
            , ITokenHelper tokenHelper)
            : base(mapper)
        {
            this._uow = uow;
            this._tokenHelper = tokenHelper;
        }

        [HttpPost]
        public ActionResult Post([FromForm] TokenRequest model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));
            }

            if (model.grant_type.Equals("password", StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return new BadRequestResponse("Username and Password fields can not be empty.");
                }

                var user = _uow.Table<UserEntity>()
                    .FirstOrDefault(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password));

                if (user == null)
                {
                    return new UnauthorizedResponse("invalid credentials.");
                }

                var claims = new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Id.ToString()),
                };
                TokenDto response = _tokenHelper.Authenticate(user, claims);
                response.User = mapper.Map<UserDto>(user);
                return new OkResponse(response);
            }
            return new BadRequestResponse($"invalid grant_type value:'{model.grant_type}'");
        }
    }
}
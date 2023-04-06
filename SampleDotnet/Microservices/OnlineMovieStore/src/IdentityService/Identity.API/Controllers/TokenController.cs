using AutoMapper;
using Identity.Database;
using Identity.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Model.Base;
using SampleProject.Identity.API.Helpers;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;
using SampleDotnet.Result;
using System.Security.Claims;
using SampleDotnet.RepositoryFactory.Interfaces;

namespace SampleProject.Identity.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(
            IMapper mapper
            , ITokenHelper tokenHelper
            , IUnitOfWork unitOfWork)
            : base(mapper)
        {
            this._tokenHelper = tokenHelper;
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TokenRequest model)
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

                using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
                {
                    var user = await repository
                        .FirstOrDefaultAsync<UserEntity>(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password));

                    if (user == null)
                    {
                        return new UnauthorizedResponse("invalid credentials.");
                    }

                    var claims = new[] {
                        new Claim("id", user.Id.ToString()),
                        new Claim("name", user.Id.ToString()),
                    };
                    TokenDto response = _tokenHelper.Authenticate(user, claims);

                    _unitOfWork.SaveChanges();

                    response.User = mapper.Map<UserDto>(user);
                    return new OkResponse(response);
                }
            }
            return new BadRequestResponse($"invalid grant_type value:'{model.grant_type}'");
        }
    }
}
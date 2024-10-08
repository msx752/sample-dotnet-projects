﻿using AutoMapper;
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


namespace SampleProject.Identity.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IDbContextFactory<IdentityDbContext> _dbContextFactory;

        public TokenController(
            IMapper mapper
            , ITokenHelper tokenHelper
            , IDbContextFactory<IdentityDbContext> factoryIdentityDbContext)
            : base(mapper)
        {
            this._tokenHelper = tokenHelper;
            _dbContextFactory = factoryIdentityDbContext;
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

                using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
                {
                    var user = await dbcontext.Users.FirstOrDefaultAsync(f => f.Username.Equals(model.Username) && f.Password.Equals(model.Password));

                    if (user == null)
                    {
                        return new UnauthorizedResponse("invalid credentials.");
                    }

                    var claims = new[] {
                        new Claim("id", user.Id.ToString()),
                        new Claim("name", user.Id.ToString()),
                    };
                    TokenDto response = _tokenHelper.Authenticate(user, claims);

                    await dbcontext.SaveChangesAsync();

                    response.User = mapper.Map<UserDto>(user);
                    return new OkResponse(response);
                }
            }
            return new BadRequestResponse($"invalid grant_type value:'{model.grant_type}'");
        }
    }
}
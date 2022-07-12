using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Samp.Auth.API.Models.Dto;
using Samp.Auth.API.Models.Requests;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;
using Samp.Identity.Core.Entities;
using Samp.Identity.Core.Migrations;

namespace Samp.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : BaseController
    {
        private readonly IOptions<IdentityApplicationSettings> appSettings;
        private readonly ISharedRepository<SampIdentityContext> repository;

        public IdentityController(IMapper mapper
            , IOptions<IdentityApplicationSettings> appSettings
            , ISharedRepository<SampIdentityContext> repository)
            : base(mapper)
        {
            this.appSettings = appSettings;
            this.repository = repository;
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

            var response = new TokenDto();
            return new OkResponse(response);
        }
    }
}
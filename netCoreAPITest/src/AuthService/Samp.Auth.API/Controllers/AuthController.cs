using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samp.Core.Model.Base;
using Samp.Core.Results;

namespace Samp.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMapper mapper)
            : base(mapper)
        {
        }

        [HttpGet("Token")]
        public ActionResult Token()
        {
            return new OkResponse();
        }
    }
}
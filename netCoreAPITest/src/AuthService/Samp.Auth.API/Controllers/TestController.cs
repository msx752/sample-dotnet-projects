using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samp.Core.Results;

namespace Samp.Identity.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            return new OkResponse();
        }
    }
}
using AutoMapper;
using Dtnt.API.Personals.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Core.Interfaces.Repositories.Shared;
using netCoreAPI.Core.Models.Base;
using netCoreAPI.Core.Results;
using netCoreAPI.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Dtnt.API.Personals.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorizeExamplesController : BaseController
    {
        public AuthorizeExamplesController(ISharedRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        /// <summary>
        /// require authorization
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return new OkResponse(Mapper.Map<List<PersonalDto>>(MyRepo.Db<PersonalEntity>().All().ToList()));
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samp.API.Personal.Models.Dtos;
using Samp.Core.Interfaces.Repositories.Shared;
using Samp.Core.Model.Base;
using Samp.Core.Results;
using Samp.Database.Personal.Entities;
using Samp.Database.Personal.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace Samp.API.Personal.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorizeExamplesController : BaseController
    {
        public AuthorizeExamplesController(ISharedRepository<MyContext> sharedRepository, IMapper mapper)
            : base(mapper)
        {
            MyContext = sharedRepository;
        }

        public ISharedRepository<MyContext> MyContext { get; set; }

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
            return new OkResponse(Mapper.Map<List<PersonalDto>>(MyContext.Table<PersonalEntity>().All().ToList()));
        }
    }
}
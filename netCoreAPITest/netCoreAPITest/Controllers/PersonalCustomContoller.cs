using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCoreAPITest.Data.Migrations;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreAPITest.Controllers
{
    [ApiController]
    [Route("api/PersonalCustom")]
    public class PersonalCustomContoller : ControllerBase
    {
        private readonly ApiContext _context;

        public PersonalCustomContoller(ApiContext context)
        {
            _context = context;
        }

        //GET api/PersonalCustom/All
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Content(JsonConvert.SerializeObject(await _context.Personals.ToListAsync(), Formatting.Indented));
        }

        //GET api/PersonalCustom/id/1
        [HttpGet()]
        [Route("id/{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var p = _context.Personals.FirstOrDefault(f => f.Id == id);
            //or
            p = _context.Personals.Find(id);//search in PrimaryKeys
            //
            if (p == null)
            {
                return NotFound();
            }
            return new JsonResult(p);
        }

        //GET api/PersonalCustom/Name/Mustafa Salih
        [HttpGet()]
        [Route("Name/{name:length(3,50)}")]
        public IActionResult GetByName([FromRoute]string name)
        {
            var p = _context.Personals.FirstOrDefault(f => f.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
            if (p == null)
            {
                return NotFound("error msg");
            }
            return Content(JsonConvert.SerializeObject(p, Formatting.Indented));
        }

        //GET api/PersonalCustom/Surname/AVCI
        [HttpGet()]
        [Route("Surname/{sname:length(3,50)}")]
        public IActionResult GetBySurname([FromRoute]string sname)
        {
            var p = _context.Personals.FirstOrDefault(f => f.Surname.Equals(sname, System.StringComparison.InvariantCultureIgnoreCase));
            if (p == null)
            {
                return NotFound();
            }
            return Content(JsonConvert.SerializeObject(p, Formatting.Indented));
        }
        //GET api/PersonalCustom/Search?q=sa
        [HttpGet()]
        [Route("[action]")]
        public async Task<IActionResult> Search()
        {
            if (!Request.Query.ContainsKey("q"))
                return BadRequest();
            var q = Request.Query["q"];
            var p = await _context.Personals
                .Where(f => f.Name.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1 || f.Surname.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1)
                .ToListAsync();
            if (p == null)
            {
                return NotFound();
            }

            return Content(JsonConvert.SerializeObject(p, Formatting.Indented));
        }
    }
}
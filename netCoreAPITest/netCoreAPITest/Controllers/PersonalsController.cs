using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCoreAPITest.Data.Migrations;
using netCoreAPITest.Data.Tables;
using netCoreAPITest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public PersonalsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // DELETE: api/Personals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonalDto>> DeletePersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }

            _context.Personals.Remove(personal);
            await _context.SaveChangesAsync();

            return _mapper.Map<PersonalDto>(personal);
        }

        // GET: api/Personals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDto>> GetPersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);

            if (personal == null)
            {
                return NotFound();
            }

            return _mapper.Map<PersonalDto>(personal);
        }

        // GET: api/Personals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> GetPersonals()
        {
            return await _context.Personals.ToListAsync();//new async list :)
        }
        // POST: api/Personals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Personal>> PostPersonal(PersonalDto personaldto)
        {
            if (!ModelState.IsValid)
            {
                var err_msgs = ModelState.Values;
                return BadRequest();
            }
            Personal personal = _mapper.Map<Personal>(personaldto);
            _context.Personals.Add(personal);// auto increment id ;)
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonal", new { id = personal.Id }, personal);
        }

        // PUT: api/Personals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonal(int id, PersonalDto personaldto)
        {
            if (!ModelState.IsValid)
            {
                var err_msgs = ModelState.Values;
                return BadRequest();
            }
            /* if (id != personal.Id)
             {
                 return BadRequest();
             }*/

            Personal personal = _mapper.Map<Personal>(personaldto);
            personal.Id = id;//it may problem xD
            _context.Entry(personal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool PersonalExists(int id)
        {
            return _context.Personals.Any(e => e.Id == id);
        }
    }
}
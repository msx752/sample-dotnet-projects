using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Model.Tables;
using netCoreAPI.Model.ViewModels;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalRestfulController : MainController
    {
        public PersonalRestfulController(IMyRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        // DELETE: api/PersonalRestful/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            //auto saveChanges triggered
            MyRepo.PersonalRepo.Delete(personal);
            return JsonResponse<PersonalViewModel>(personal);
        }

        // GET: api/PersonalRestful/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            return JsonResponse<PersonalViewModel>(personal);
        }

        // GET: api/PersonalRestful
        [HttpGet]
        public IActionResult Get()
        {
            return JsonResponse<List<PersonalViewModel>>(MyRepo.PersonalRepo.GetAll());
        }

        // POST: api/PersonalRestful
        [HttpPost]
        public IActionResult Post(PersonalViewModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            Personal personal = Mapper.Map<Personal>(personalViewModel);
            personal = MyRepo.PersonalRepo.Add(personal);//auto saveChanges triggered
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
            */
            return Get(personal.Id);
        }

        // PUT: api/PersonalRestful/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonalViewModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            else if (id != personalViewModel.Id)
                return BadRequest();
            Personal personal = Mapper.Map<Personal>(personalViewModel);
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
             */
            MyRepo.PersonalRepo.Update(personal);//auto saveChanges triggered
            return NoContent();
        }
    }
}
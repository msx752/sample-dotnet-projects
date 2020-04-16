using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Model.Tables;
using netCoreAPI.Model.ViewModels;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Core.Controllers
{
    [ApiController]
    [Route("api/PersonalCustom")]
    public class PersonalCustomContoller : MainController
    {
        public PersonalCustomContoller(IMyRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        //GET api/PersonalCustom/All
        [HttpGet]
        [Route("All")]
        public IActionResult All()
        {
            return JsonResponse<List<PersonalViewModel>>(MyRepo.Db<Personal>().All().ToList());
        }

        // DELETE: api/PersonalCustom/5
        [HttpDelete("{id:int}")]
        public IActionResult DeletePersonal(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            MyRepo.Db<Personal>().Delete(personal);
            //manually saveChanges triggered
            MyRepo.Commit();
            return JsonResponse<PersonalViewModel>(personal);
        }

        //GET api/PersonalCustom/id/1
        [HttpGet()]
        [Route("id/{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            return JsonResponse<PersonalViewModel>(personal);
        }

        //GET api/PersonalCustom/Name/Mustafa Salih
        [HttpGet()]
        [Route("Name/{name:length(3,50)}")]
        public IActionResult GetByName([FromRoute]string name)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return NotFound("error msg");
            return JsonResponse<PersonalViewModel>(personal);
        }

        //GET api/PersonalCustom/Surname/AVCI
        [HttpGet()]
        [Route("Surname/{sname:length(3,50)}")]
        public IActionResult GetBySurname([FromRoute]string sname)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Surname.Equals(sname, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return NotFound();
            return JsonResponse<PersonalViewModel>(personal);
        }

        //GET api/PersonalCustom/Search?q=sa
        [HttpGet()]
        [Route("[action]")]
        public IActionResult Search()
        {
            if (!Request.Query.ContainsKey("q"))
                return BadRequest();
            var personals = MyRepo.Db<Personal>()
                .Where(f => f.Name.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1 || f.Surname.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1)
                .ToList();
            if (personals.Count == 0)
                return NotFound();
            return JsonResponse<List<PersonalViewModel>>(personals);
        }
    }
}
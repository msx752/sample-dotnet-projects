using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Core.Controllers.Base;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.Tables;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalCustomContoller : MainController
    {
        public PersonalCustomContoller(IMyRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        /// <summary>
        /// GET api/PersonalCustom/All
        /// </summary>
        /// <returns>List PersonalDto</returns>
        [HttpGet]
        [Route("All")]
        public IActionResult All()
        {
            return JsonResponse<List<PersonalDto>>(MyRepo.Db<Personal>().All().ToList());
        }

        /// <summary>
        /// DELETE: api/PersonalCustom/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeletePersonal(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            MyRepo.Db<Personal>().Delete(personal);
            //manually saveChanges triggered
            MyRepo.Commit();
            return JsonResponse<PersonalDto>(personal);
        }

        /// <summary>
        /// GET api/PersonalCustom/id/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return NotFound();
            return JsonResponse<PersonalDto>(personal);
        }

        /// <summary>
        /// GET api/PersonalCustom/Name/Mustafa Salih
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Name/{name:length(3,50)}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return NotFound("error msg");
            return JsonResponse<PersonalDto>(personal);
        }

        /// <summary>
        /// GET api/PersonalCustom/Surname/SAVCI
        /// </summary>
        /// <param name="sname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Surname/{sname:length(3,50)}")]
        public IActionResult GetBySurname([FromRoute] string sname)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Surname.Equals(sname, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return NotFound();
            return JsonResponse<PersonalDto>(personal);
        }

        /// <summary>
        /// GET api/PersonalCustom/Search?q=sa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult Search()
        {
            if (!Request.Query.ContainsKey("q"))
                return BadRequest();
            var personals = MyRepo.Db<Personal>()
                .Where(f => f.Name.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1)
                .ToList();
            if (personals.Count == 0)
                return NotFound();
            return JsonResponse<List<PersonalDto>>(personals);
        }
    }
}
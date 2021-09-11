using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Core.Controllers.Base;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.Entities;
using netCoreAPI.Model.ResponseModels;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalCustomController : MainController
    {
        public PersonalCustomController(IMyRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        /// <summary>
        /// GET api/PersonalCustom/All
        /// </summary>
        /// <returns>List PersonalDto</returns>
        [HttpGet]
        [Route("All")]
        public ActionResult<BaseResponseModel<PersonalDto>> All()
        {
            return new SuccessResponseModel<PersonalDto>(Mapper.Map<List<PersonalDto>>(MyRepo.Db<Personal>().All().ToList()));
        }

        /// <summary>
        /// DELETE: api/PersonalCustom/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public ActionResult<BaseResponseModel<PersonalDto>> DeletePersonal(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            MyRepo.Db<Personal>().Delete(personal);
            MyRepo.SaveChanges();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/PersonalCustom/id/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{id:int}")]
        public ActionResult<BaseResponseModel<PersonalDto>> GetById([FromRoute] int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/PersonalCustom/Name/Mustafa Salih
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Name/{name:length(3,50)}")]
        public ActionResult<BaseResponseModel<PersonalDto>> GetByName([FromRoute] string name)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/PersonalCustom/Surname/SAVCI
        /// </summary>
        /// <param name="sname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Surname/{sname:length(3,50)}")]
        public ActionResult<BaseResponseModel<PersonalDto>> GetBySurname([FromRoute] string sname)
        {
            var personal = MyRepo.Db<Personal>()
                .FirstOrDefault(f => f.Surname.Equals(sname, System.StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/PersonalCustom/Search?q=sa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public ActionResult<BaseResponseModel<PersonalDto>> Search()
        {
            if (!Request.Query.ContainsKey("q"))
                return new BadRequestResponseModel<PersonalDto>();

            var personals = MyRepo.Db<Personal>()
                .Where(f => f.Name.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(Request.Query["q"], System.StringComparison.InvariantCultureIgnoreCase) > -1)
                .ToList();
            if (personals.Count == 0)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<List<PersonalDto>>(personals));
        }
    }
}
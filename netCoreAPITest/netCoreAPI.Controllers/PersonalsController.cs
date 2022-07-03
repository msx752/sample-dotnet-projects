using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Controllers.Base;
using netCoreAPI.Model.Dtos;
using netCoreAPI.Model.Entities;
using netCoreAPI.Model.Models;
using netCoreAPI.Model.ResponseModels;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalsController : MainController
    {
        public PersonalsController(ISharedRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        /// <summary>
        ///  DELETE: api/PersonalRestful/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<BaseResponseModel<PersonalDto>> Delete(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            personal = MyRepo.Db<Personal>().Delete(personal).Entity;
            MyRepo.SaveChanges();
            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/PersonalRestful/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BaseResponseModel<PersonalDto>> Get(int id)
        {
            var personal = MyRepo.Db<Personal>().GetById(id);
            if (personal == null)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/PersonalRestful
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponseModel<PersonalDto>> Get()
        {
            return new SuccessResponseModel<PersonalDto>(Mapper.Map<List<PersonalDto>>(MyRepo.Db<Personal>().All().ToList()));
        }

        /// <summary>
        ///  POST: api/PersonalRestful
        /// </summary>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseResponseModel<PersonalDto>> Post([FromBody] PersonalModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponseModel<PersonalDto>(ModelState.Values.SelectMany(v => v.Errors));

            var personalEntity = Mapper.Map<Personal>(personalViewModel);

            var personal = MyRepo.Db<Personal>().Add(personalEntity);
            MyRepo.SaveChanges();
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
            */
            return new SuccessResponseModel<PersonalDto>(Mapper.Map<PersonalDto>(personal.Entity));
        }

        /// <summary>
        ///  PUT: api/PersonalRestful/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<BaseResponseModel<PersonalDto>> Put(int id, [FromBody] PersonalModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponseModel<PersonalDto>(ModelState.Values.SelectMany(v => v.Errors));
            var personalEntityDb = MyRepo.Db<Personal>().GetById(id);

            if (personalEntityDb == null)
                return new BadRequestResponseModel<PersonalDto>("entity not found");

            var personalEntity = Mapper.Map<Personal>(personalViewModel);
            personalEntity.Id = id;
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
             */
            personalEntity = MyRepo.Db<Personal>().Update(personalEntity).Entity;
            MyRepo.SaveChanges();
            return new SuccessResponseModel<PersonalDto>();
        }

        #region Custom Endpoints

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
        public ActionResult<BaseResponseModel<PersonalDto>> Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
                return new BadRequestResponseModel<PersonalDto>();

            var personals = MyRepo.Db<Personal>()
                .Where(f => f.Name.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1);
            if (personals.Count() == 0)
                return new NotFoundResponseModel<PersonalDto>();

            return new SuccessResponseModel<PersonalDto>(Mapper.Map<List<PersonalDto>>(personals));
        }

        #endregion Custom Endpoints
    }
}
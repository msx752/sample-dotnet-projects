using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Controllers.Requests;
using netCoreAPI.Core.Results;
using netCoreAPI.Models.Dtos;
using netCoreAPI.Models.Entities;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalsController : _BaseController
    {
        public PersonalsController(ISharedRepository myRepository, IMapper mapper)
            : base(myRepository, mapper)
        {
        }

        /// <summary>
        ///  DELETE: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var personal = MyRepo.Db<PersonalEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            personal = MyRepo.Db<PersonalEntity>().Delete(personal);
            MyRepo.SaveChanges();

            return new OkResponse(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var personal = MyRepo.Db<PersonalEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResponse(Mapper.Map<List<PersonalDto>>(MyRepo.Db<PersonalEntity>().All().ToList()));
        }

        /// <summary>
        ///  POST: api/Personals
        /// </summary>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] PersonalRequest personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var personalEntity = Mapper.Map<PersonalEntity>(personalViewModel);

            var personal = MyRepo.Db<PersonalEntity>().Add(personalEntity);
            MyRepo.SaveChanges();
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
            */
            return new OkResponse(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  PUT: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PersonalRequest personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var personalEntityDb = MyRepo.Db<PersonalEntity>().GetById(id);

            if (personalEntityDb == null)
                return new BadRequestResponse("entity not found");

            var personalEntity = Mapper.Map<PersonalEntity>(personalViewModel);
            personalEntity.Id = id;
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
             */
            personalEntity = MyRepo.Db<PersonalEntity>().Update(personalEntity);
            MyRepo.SaveChanges();

            return new OkResponse();
        }

        #region Custom Endpoints

        /// <summary>
        /// GET api/Personals/Name/CUSTOMER_NAME
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Name/{name:length(3,50)}")]
        public ActionResult GetByName([FromRoute] string name)
        {
            var personal = MyRepo.Db<PersonalEntity>()
                .FirstOrDefault(f => f.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/Personals/Surname/CUSTOMER_SURNAME
        /// </summary>
        /// <param name="sname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Surname/{sname:length(3,50)}")]
        public ActionResult GetBySurname([FromRoute] string sname)
        {
            var personal = MyRepo.Db<PersonalEntity>()
                .FirstOrDefault(f => f.Surname.Equals(sname, System.StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(Mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        /// GET api/Personals/Search?q=sa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public ActionResult Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
                return new BadRequestResponse();

            var personals = MyRepo.Db<PersonalEntity>()
                .Where(f => f.Name.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1);
            if (personals.Count() == 0)
                return new NotFoundResponse();

            return new OkResponse(Mapper.Map<List<PersonalDto>>(personals));
        }

        #endregion Custom Endpoints
    }
}
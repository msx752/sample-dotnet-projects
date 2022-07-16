using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samp.API.Personal.Models.Dtos;
using Samp.API.Personal.Models.Requests;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;
using Samp.Database.Personal.Entities;
using Samp.Database.Personal.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Samp.API.Personal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalsController : BaseController
    {
        public PersonalsController(ISharedRepository<MyContext> sharedRepository, IMapper mapper)
            : base(mapper)
        {
            MyContext = sharedRepository;
        }

        public ISharedRepository<MyContext> MyContext { get; set; }

        /// <summary>
        ///  DELETE: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var personal = MyContext.Table<PersonalEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            MyContext.Table<PersonalEntity>().Delete(personal);
            MyContext.Commit(Guid.NewGuid());

            return new OkResponse(mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var personal = MyContext.Table<PersonalEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<PersonalDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResponse(mapper.Map<List<PersonalDto>>(MyContext.Table<PersonalEntity>().All().ToList()));
        }

        /// <summary>
        ///  POST: api/Personals
        /// </summary>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] PersonalModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var personalEntity = mapper.Map<PersonalEntity>(personalViewModel);

            MyContext.Table<PersonalEntity>().Insert(personalEntity);
            MyContext.Commit(Guid.NewGuid());
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
            */
            return new OkResponse(mapper.Map<PersonalDto>(personalEntity));
        }

        /// <summary>
        ///  PUT: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PersonalModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            if (!MyContext.Table<PersonalEntity>().Exists(id))
                return new BadRequestResponse("entity not found");

            var personalEntity = mapper.Map<PersonalEntity>(personalViewModel);
            personalEntity.Id = id;
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
             */
            MyContext.Table<PersonalEntity>().Update(personalEntity);
            MyContext.Commit(Guid.NewGuid());

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
            var personal = MyContext.Table<PersonalEntity>()
                .FirstOrDefault(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<PersonalDto>(personal));
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
            var personal = MyContext.Table<PersonalEntity>()
                .FirstOrDefault(f => f.Surname.Equals(sname, StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<PersonalDto>(personal));
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

            var personals = MyContext.Table<PersonalEntity>()
                .Where(f => f.Name.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(q, System.StringComparison.InvariantCultureIgnoreCase) > -1);

            if (personals.Count() == 0)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<List<PersonalDto>>(personals));
        }

        #endregion Custom Endpoints
    }
}
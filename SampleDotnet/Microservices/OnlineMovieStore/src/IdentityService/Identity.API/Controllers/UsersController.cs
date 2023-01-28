using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Core.Model.Base;
using SampleProject.Result;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;
using SampleProject.Identity.Core.Migrations;
using SampleProject.Identity.Database.Entities;

namespace SampleProject.Identity.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork<IdentityDbContext> repository;

        public UsersController(
            IMapper mapper
            , IUnitOfWork<IdentityDbContext> repository
            )
            : base(mapper)
        {
            this.repository = repository;
        }

        /// <summary>
        ///  DELETE: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var personal = repository.Table<UserEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            repository.Table<UserEntity>().Delete(personal);
            repository.SaveChanges(LoggedUserId);

            return new OkResponse(mapper.Map<UserDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var personal = repository.Table<UserEntity>().GetById(id);

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<UserDto>(personal));
        }

        /// <summary>
        ///  GET: api/Personals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return new OkResponse(mapper.Map<List<UserDto>>(repository.Table<UserEntity>().AsQueryable().ToList()));
        }

        /// <summary>
        ///  POST: api/Personals
        /// </summary>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] UserCreateModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var UserEntity = mapper.Map<UserEntity>(personalViewModel);

            repository.Table<UserEntity>().Insert(UserEntity);
            repository.SaveChanges(LoggedUserId);
            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
            */
            return new OkResponse(mapper.Map<UserDto>(UserEntity));
        }

        /// <summary>
        ///  PUT: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] UserUpdateModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var userEntity = repository.Table<UserEntity>().Find(keyValues: id);
            if (userEntity == null)
                return new BadRequestResponse("entity not found");

            userEntity.Name = personalViewModel.Name;
            userEntity.Surname = personalViewModel.Surname;
            userEntity.Email = personalViewModel.Email;

            /*
             To protect from overposting attacks, please enable the specific properties you want to bind to, for
             more details see https://aka.ms/RazorPagesCRUD.
             */
            repository.Table<UserEntity>().Update(userEntity);
            repository.SaveChanges(LoggedUserId);

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
            var personal = repository.Table<UserEntity>()
                .FirstOrDefault(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<UserDto>(personal));
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
            var personal = repository.Table<UserEntity>()
                .FirstOrDefault(f => f.Surname.Equals(sname, StringComparison.InvariantCultureIgnoreCase));

            if (personal == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<UserDto>(personal));
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

            var personals = repository.Table<UserEntity>()
                .Where(f => f.Name.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) > -1 ||
                            f.Surname.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) > -1);

            if (personals.Count() == 0)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<List<UserDto>>(personals));
        }

        #endregion Custom Endpoints
    }
}
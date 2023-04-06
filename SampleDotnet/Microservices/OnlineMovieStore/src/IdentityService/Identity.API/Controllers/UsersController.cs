using AutoMapper;
using Identity.Database;
using Identity.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Model.Base;
using SampleProject.Identity.API.Models.Dto;
using SampleProject.Identity.API.Models.Requests;
using SampleDotnet.Result;
using SampleDotnet.RepositoryFactory.Interfaces;

namespace SampleProject.Identity.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            IMapper mapper
            , IUnitOfWork unitOfWork)
            : base(mapper)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  DELETE: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var personal = await repository.FirstOrDefaultAsync<UserEntity>(f => f.Id == id);

                if (personal == null)
                    return new NotFoundResponse();

                repository.Delete(personal);

                await _unitOfWork.SaveChangesAsync();

                return new OkResponse(mapper.Map<UserDto>(personal));
            }
        }

        /// <summary>
        ///  GET: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var personal = await repository.FirstOrDefaultAsync<UserEntity>(f => f.Id == id);

                if (personal == null)
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<UserDto>(personal));
            }
        }

        /// <summary>
        ///  GET: api/Personals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                return new OkResponse(mapper.Map<List<UserDto>>(await repository.AsQueryable<UserEntity>().ToListAsync()));
            }
        }

        /// <summary>
        ///  POST: api/Personals
        /// </summary>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserCreateModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var UserEntity = mapper.Map<UserEntity>(personalViewModel);

                await repository.InsertAsync(UserEntity);

                await _unitOfWork.SaveChangesAsync();
                /*
                 To protect from overposting attacks, please enable the specific properties you want to bind to, for
                 more details see https://aka.ms/RazorPagesCRUD.
                */
                return new OkResponse(mapper.Map<UserDto>(UserEntity));
            }
        }

        /// <summary>
        ///  PUT: api/Personals/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personalViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserUpdateModel personalViewModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var userEntity = await repository.FirstOrDefaultAsync<UserEntity>(f => f.Id == id);
                if (userEntity == null)
                    return new BadRequestResponse("entity not found");

                userEntity.Name = personalViewModel.Name;
                userEntity.Surname = personalViewModel.Surname;
                userEntity.Email = personalViewModel.Email;

                /*
                 To protect from overposting attacks, please enable the specific properties you want to bind to, for
                 more details see https://aka.ms/RazorPagesCRUD.
                 */
                repository.Update(userEntity);

                await _unitOfWork.SaveChangesAsync();

                return new OkResponse();
            }
        }

        #region Custom Endpoints

        /// <summary>
        /// GET api/Personals/Name/CUSTOMER_NAME
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Name/{name:length(3,50)}")]
        public async Task<ActionResult> GetByName([FromRoute] string name)
        {
            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var personal = await repository
                    .FirstOrDefaultAsync<UserEntity>(f => f.Name.Equals(name));

                if (personal == null)
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<UserDto>(personal));
            }
        }

        /// <summary>
        /// GET api/Personals/Surname/CUSTOMER_SURNAME
        /// </summary>
        /// <param name="sname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Surname/{sname:length(3,50)}")]
        public async Task<ActionResult> GetBySurname([FromRoute] string sname)
        {
            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var personal = await repository
                    .FirstOrDefaultAsync<UserEntity>(f => f.Surname.Equals(sname));

                if (personal == null)
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<UserDto>(personal));
            }
        }

        /// <summary>
        /// GET api/Personals/Search?q=sa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
                return new BadRequestResponse();

            using (var repository = _unitOfWork.CreateRepository<IdentityDbContext>())
            {
                var personals = repository.Where<UserEntity>(f =>
                        f.Name.Contains(q)
                        || f.Surname.Contains(q)
                    );

                if (!await personals.AnyAsync())
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<List<UserDto>>(await personals.ToListAsync()));
            }
        }

        #endregion Custom Endpoints
    }
}
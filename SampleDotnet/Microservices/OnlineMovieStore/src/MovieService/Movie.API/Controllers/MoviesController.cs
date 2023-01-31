using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Core.Model.Base;
using SampleProject.Result;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.API.Models.Requests;
using SampleProject.Movie.API.Models.Responses;
using SampleProject.Movie.Database.Entities;
using SampleProject.Movie.Database.Migrations;

namespace SampleProject.Movie.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IUnitOfWork<MovieDbContext> _uow;

        public MoviesController(IMapper mapper
            , IUnitOfWork<MovieDbContext> repository)
            : base(mapper)
        {
            this._uow = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            MovieIndexViewModel movieIndexModel = new MovieIndexViewModel();

            var hightRatingEntity = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .Where(f => f.Rating.AverageRating >= 70)
                .Take(5)
                .ToList();
            movieIndexModel.HighRatings = mapper.Map<List<MovieDto>>(hightRatingEntity);

            var allEntity = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .ToList();
            movieIndexModel.All = mapper.Map<List<MovieDto>>(allEntity);

            var recentyAddedEntities = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .Take(4)
                .ToList();
            movieIndexModel.RecentlyAdded = mapper.Map<List<MovieDto>>(recentyAddedEntities);

            return new OkResponse(movieIndexModel);
        }

        [HttpGet("HighRatings")]
        public ActionResult HighRatings()
        {
            var hightRatingEntity = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .Where(f => f.Rating.AverageRating >= 70)
                .Take(5)
                .ToList();

            return new OkResponse(mapper.Map<List<MovieDto>>(hightRatingEntity));
        }

        [HttpGet("RecentlyAdded")]
        public ActionResult RecentlyAdded()
        {
            var recentyAddedEntities = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .Take(4)
                .ToList();

            return new OkResponse(mapper.Map<List<MovieDto>>(recentyAddedEntities));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(string Id)
        {
            var entity = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(x => x.Rating)
                .Include(x => x.MovieWriters)
                .ThenInclude(x => x.Writer)
                .Include(x => x.MovieDirectors)
                .ThenInclude(x => x.Director)
                .Include(x => x.Categories)
                .ThenInclude(x => x.Category)
                .Where(f => f.Id == Id)
                .ToList();

            if (entity == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<List<MovieDto>>(entity));
        }

        [HttpGet("Search")]
        public ActionResult Search([FromQuery] MovieSearchModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var entity = _uow.Table<MovieEntity>()
                .AsQueryable()
                .Include(f => f.Rating)
                .Where(f => f.Title.Contains(model.Query, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            if (entity == null)
                return new NotFoundResponse();

            return new OkResponse(mapper.Map<List<MovieDto>>(entity));
        }

        [HttpGet("CategoryBy/{Id}")]
        public ActionResult GetFilteredByCategoryId(int Id)
        {
            var entityCategory = _uow.Table<CategoryEntity>().GetById(Id);
            if (entityCategory == null)
                return new NotFoundResponse("Category not found: " + Id);

            var entity = _uow.Table<MovieCategoryEntity>()
                .AsQueryable()
                .Include(f => f.Movie)
                .ThenInclude(f => f.Rating)
                .Where(f => f.CategoryId == entityCategory.Id)
                .Select(f => f.Movie)
                .ToList();

            return new OkResponse(mapper.Map<List<MovieDto>>(entity));
        }

        [HttpGet("Categories")]
        public ActionResult GetCategories() //TODO: move to CategoriesController
        {
            var entity = _uow.Table<CategoryEntity>()
                .AsQueryable()
                .Include(f => f.Categories)
                .Where(f => f.Categories.Any())
                .ToList();

            return new OkResponse(mapper.Map<List<CategoryDto>>(entity));
        }
    }
}
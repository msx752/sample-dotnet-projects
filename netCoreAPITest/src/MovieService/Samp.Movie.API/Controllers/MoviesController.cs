using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Result;
using Samp.Movie.API.Models.Dtos;
using Samp.Movie.API.Models.Requests;
using Samp.Movie.API.Models.Responses;
using Samp.Movie.Database.Entities;
using Samp.Movie.Database.Migrations;

namespace Samp.Movie.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly ISharedRepository<MovieDbContext> repository;

        public MoviesController(IMapper mapper
            , ISharedRepository<MovieDbContext> repository)
            : base(mapper)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            MovieIndexViewModel movieIndexModel = new MovieIndexViewModel();

            var hightRatingEntity = repository.Table<MovieEntity>()
                .All()
                .Include(f => f.Rating)
                .Where(f => f.Rating.AverageRating >= 70)
                .Take(5)
                .ToList();
            movieIndexModel.HighRatings = mapper.Map<List<MovieDto>>(hightRatingEntity);

            var allEntity = repository.Table<MovieEntity>()
                .All()
                .Include(f => f.Rating)
                .ToList();
            movieIndexModel.All = mapper.Map<List<MovieDto>>(allEntity);

            var recentyAddedEntities = repository.Table<MovieEntity>()
                .All()
                .Include(f => f.Rating)
                .Take(4)
                .ToList();
            movieIndexModel.RecentlyAdded = mapper.Map<List<MovieDto>>(recentyAddedEntities);

            return new OkResponse(movieIndexModel);
        }

        [HttpGet("HighRatings")]
        public ActionResult HighRatings()
        {
            var hightRatingEntity = repository.Table<MovieEntity>()
                .All()
                .Include(f => f.Rating)
                .Where(f => f.Rating.AverageRating >= 70)
                .Take(5)
                .ToList();

            return new OkResponse(mapper.Map<List<MovieDto>>(hightRatingEntity));
        }

        [HttpGet("RecentlyAdded")]
        public ActionResult RecentlyAdded()
        {
            var recentyAddedEntities = repository.Table<MovieEntity>()
                .All()
                .Include(f => f.Rating)
                .Take(4)
                .ToList();

            return new OkResponse(mapper.Map<List<MovieDto>>(recentyAddedEntities));
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(string Id)
        {
            var entity = repository.Table<MovieEntity>()
                .All()
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

            var entity = repository.Table<MovieEntity>()
                .All()
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
            var entityCategory = repository.Table<CategoryEntity>().GetById(Id);
            if (entityCategory == null)
                return new NotFoundResponse("Category not found: " + Id);

            var entity = repository.Table<MovieCategoryEntity>()
                .All()
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
            var entity = repository.Table<CategoryEntity>()
                .All()
                .Include(f => f.Categories)
                .Where(f => f.Categories.Any())
                .ToList();

            return new OkResponse(mapper.Map<List<CategoryDto>>(entity));
        }
    }
}
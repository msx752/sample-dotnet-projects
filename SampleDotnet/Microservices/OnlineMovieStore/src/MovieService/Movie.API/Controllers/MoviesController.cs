using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Database;
using Movie.Database.Entities;
using SampleProject.Core.Model.Base;
using SampleProject.Movie.API.Models.Dtos;
using SampleProject.Movie.API.Models.Requests;
using SampleProject.Movie.API.Models.Responses;
using SampleDotnet.Result;


namespace SampleProject.Movie.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IDbContextFactory<MovieDbContext> _dbContextFactory;

        public MoviesController(IMapper mapper
            , IDbContextFactory<MovieDbContext> dbContextFactory)
            : base(mapper)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            MovieIndexViewModel movieIndexModel = new MovieIndexViewModel();

            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var hightRatingEntity = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .Where(f => f.Rating != null && f.Rating.AverageRating >= 70)
                    .Take(5)
                    .ToListAsync();
                movieIndexModel.HighRatings = mapper.Map<List<MovieDto>>(hightRatingEntity);

                var allEntity = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .ToListAsync();
                movieIndexModel.All = mapper.Map<List<MovieDto>>(allEntity);

                var recentyAddedEntities = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .Take(4)
                    .ToListAsync();
                movieIndexModel.RecentlyAdded = mapper.Map<List<MovieDto>>(recentyAddedEntities);

                return new OkResponse(movieIndexModel);
            }
        }

        [HttpGet("HighRatings")]
        public async Task<ActionResult> HighRatings()
        {
            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var hightRatingEntity = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .Where(f => f.Rating != null && f.Rating.AverageRating >= 70)
                    .Take(5)
                    .ToListAsync();

                return new OkResponse(mapper.Map<List<MovieDto>>(hightRatingEntity));
            }
        }

        [HttpGet("RecentlyAdded")]
        public async Task<ActionResult> RecentlyAdded()
        {
            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var recentyAddedEntities = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .Take(4)
                    .ToListAsync();

                return new OkResponse(mapper.Map<List<MovieDto>>(recentyAddedEntities));
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return new BadRequestResponse();

            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = await dbcontext.Movies
                    .AsNoTracking()
                    .Include(x => x.Rating)
                    .Include(x => x.MovieWriters)
                    .ThenInclude(x => x.Writer)
                    .Include(x => x.MovieDirectors)
                    .ThenInclude(x => x.Director)
                    .Include(x => x.Categories)
                    .ThenInclude(x => x.Category)
                    .Where(f => !string.IsNullOrWhiteSpace(f.Id) && f.Id == Id)
                    .ToListAsync();

                if (!entity.Any())
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<List<MovieDto>>(entity));
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult> Search([FromQuery] MovieSearchModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = dbcontext.Movies
                    .AsNoTracking()
                    .Include(f => f.Rating)
                    .Where(f => f.Title != null && f.Title.Contains(model.Query));

                if (!await entity.AnyAsync())
                    return new NotFoundResponse();

                return new OkResponse(mapper.Map<List<MovieDto>>(await entity.ToListAsync()));
            }
        }

        [HttpGet("CategoryBy/{Id}")]
        public async Task<ActionResult> GetFilteredByCategoryId(long Id)
        {
            if (Id == 0)
                return new BadRequestResponse();

            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entityCategory = await dbcontext.Categories.FindAsync(Id);
                if (entityCategory == null)
                    return new NotFoundResponse("Category not found: " + Id);

                var entity = await dbcontext.MovieCategories
                    .AsNoTracking()
                    .Include(f => f.Movie)
                    .ThenInclude(f => f.Rating)
                    .Where(f => f.CategoryId != 0 && f.CategoryId == entityCategory.Id)
                    .Select(f => f.Movie)
                    .ToListAsync();

                return new OkResponse(mapper.Map<List<MovieDto>>(entity));
            }
        }

        [HttpGet("Categories")]
        public async Task<ActionResult> GetCategories() //TODO: move to CategoriesController
        {
            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = await dbcontext.Categories
                    .AsNoTracking()
                    .Include(f => f.Categories)
                    .Where(f => f.Categories.Any())
                    .ToListAsync();

                return new OkResponse(mapper.Map<List<CategoryDto>>(entity));
            }
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Static.Services;

namespace netCoreAPI.Core.Controllers
{
    public abstract class MainController : ControllerBase
    {
        private readonly IMapper _mapper;

        internal MainController(IMyRepository myRepository, IMapper mapper)
        {
            MyRepo = myRepository;
            _mapper = mapper;
        }

        internal IMapper Mapper => _mapper;
        internal IMyRepository MyRepo { get; }

        /// <summary>
        /// common auto mapper control method
        /// </summary>
        /// <typeparam name="T"> outer type of auto-mapper</typeparam>
        /// <param name="data"> inner type of auto-mapper such as type of database table</param>
        /// <returns></returns>
        internal IActionResult JsonResponse<T>(object data) => new JsonResult(Mapper.Map<T>(data));
    }
}
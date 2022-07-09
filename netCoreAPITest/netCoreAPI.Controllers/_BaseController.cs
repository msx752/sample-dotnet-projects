using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Static.Services;

namespace netCoreAPI.Controllers
{
    public abstract class _BaseController : ControllerBase
    {
        private readonly IMapper _mapper;

        internal _BaseController(ISharedRepository myRepository, IMapper mapper)
        {
            MyRepo = myRepository;
            _mapper = mapper;
        }

        internal IMapper Mapper => _mapper;
        internal ISharedRepository MyRepo { get; }
    }
}
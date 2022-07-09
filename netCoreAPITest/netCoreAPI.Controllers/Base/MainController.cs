using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Static.Services;

namespace netCoreAPI.Controllers.Base
{
    public abstract class MainController : ControllerBase
    {
        private readonly IMapper _mapper;

        internal MainController(ISharedRepository myRepository, IMapper mapper)
        {
            MyRepo = myRepository;
            _mapper = mapper;
        }

        internal IMapper Mapper => _mapper;
        internal ISharedRepository MyRepo { get; }
    }
}
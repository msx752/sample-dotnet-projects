using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Core.Interfaces.Repositories.Shared;

namespace netCoreAPI.Core.Models.Base
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IMapper _mapper;

        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected IMapper Mapper => _mapper;
    }
}
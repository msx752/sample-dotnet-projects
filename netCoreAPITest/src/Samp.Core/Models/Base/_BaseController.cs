using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Samp.Core.Model.Base
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
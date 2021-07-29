using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using netCoreAPI.Model.ResponseModels;
using netCoreAPI.Static.Services;
using System.Collections.Generic;
using System.Net;

namespace netCoreAPI.Core.Controllers.Base
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
    }
}
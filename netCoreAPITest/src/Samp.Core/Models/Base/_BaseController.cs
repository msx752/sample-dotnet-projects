using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Samp.Core.Model.Base
{
    public abstract class BaseController : ControllerBase
    {
        public readonly IMapper mapper;

        protected BaseController(IMapper _mapper)
        {
            this.mapper = _mapper;
            LoggedUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        public Guid LoggedUserId { get; set; }
    }
}
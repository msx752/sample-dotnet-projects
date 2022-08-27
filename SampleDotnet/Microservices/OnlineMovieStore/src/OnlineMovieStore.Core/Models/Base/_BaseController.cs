using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace SampleProject.Core.Model.Base
{
    public abstract class BaseController : ControllerBase
    {
        public readonly IMapper mapper;

        protected BaseController(IMapper _mapper)
        {
            this.mapper = _mapper;
        }

        public Guid LoggedUserId
        {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var id = this.User.Claims.First(x => x.Type == "id").Value;
                    return Guid.Parse(id);
                }
                else
                {
#if DEBUG
                    return Guid.Parse("00000000-0000-0000-0000-000000000001");
#else
                    return Guid.Parse("00000000-0000-0000-0000-000000000000");
#endif
                }
            }
        }
    }
}
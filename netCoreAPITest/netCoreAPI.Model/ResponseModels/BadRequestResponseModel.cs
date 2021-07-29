using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace netCoreAPI.Model.ResponseModels
{
    public class BadRequestResponseModel<T> : BaseResponseModel<T> where T : class
    {
        public BadRequestResponseModel() : base()
        {
        }

        public BadRequestResponseModel(IEnumerable<ModelError> modelStateErrors) : this(string.Join(',', modelStateErrors))
        {
        }

        public BadRequestResponseModel(string userFriendlyMessage) : base(userFriendlyMessage, HttpStatusCode.BadRequest)
        {
        }

        private BadRequestResponseModel(T singleModel, string userFriendlyMessage) : base(singleModel, userFriendlyMessage, HttpStatusCode.BadRequest)
        {
        }

        private BadRequestResponseModel(IEnumerable<T> multipleModels, string userFriendlyMessage) : base(multipleModels, userFriendlyMessage, HttpStatusCode.BadRequest)
        {
        }
    }
}
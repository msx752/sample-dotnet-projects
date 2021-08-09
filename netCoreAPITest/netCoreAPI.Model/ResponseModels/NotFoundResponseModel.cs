using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace netCoreAPI.Model.ResponseModels
{
    public class NotFoundResponseModel<T> : BaseResponseModel<T> where T : class
    {
        public NotFoundResponseModel() : base(null, HttpStatusCode.NotFound)
        {
        }

        public NotFoundResponseModel(IEnumerable<ModelError> modelStateErrors) : this(string.Join(',', modelStateErrors))
        {
        }

        public NotFoundResponseModel(string userFriendlyMessage) : base(userFriendlyMessage, HttpStatusCode.NotFound)
        {
        }

        private NotFoundResponseModel(T singleModel, string userFriendlyMessage) : base(singleModel, userFriendlyMessage, HttpStatusCode.NotFound)
        {
        }

        private NotFoundResponseModel(IEnumerable<T> multipleModels, string userFriendlyMessage) : base(multipleModels, userFriendlyMessage, HttpStatusCode.NotFound)
        {
        }
    }
}
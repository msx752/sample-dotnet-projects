using System.Collections.Generic;
using System.Net;

namespace netCoreAPI.Model.ResponseModels
{
    public class InternalErrorResponseModel<T> : BaseResponseModel<T> where T : class
    {
        public InternalErrorResponseModel() : base(null, HttpStatusCode.InternalServerError)
        {
        }

        public InternalErrorResponseModel(string userFriendlyMessage) : base(userFriendlyMessage, HttpStatusCode.InternalServerError)
        {
        }

        private InternalErrorResponseModel(T singleModel, string userFriendlyMessage) : base(singleModel, userFriendlyMessage, HttpStatusCode.InternalServerError)
        {
        }

        private InternalErrorResponseModel(IEnumerable<T> multipleModels, string userFriendlyMessage) : base(multipleModels, userFriendlyMessage, HttpStatusCode.InternalServerError)
        {
        }
    }
}
using System.Collections.Generic;
using System.Net;

namespace netCoreAPI.Model.ResponseModels
{
    public class SuccessResponseModel<T> : BaseResponseModel<T> where T : class
    {
        public SuccessResponseModel() : base(null, HttpStatusCode.OK)
        {
        }

        public SuccessResponseModel(string userFriendlyMessage) : base(userFriendlyMessage, HttpStatusCode.OK)
        {
        }

        public SuccessResponseModel(T singleModel) : base(singleModel, null, HttpStatusCode.OK)
        {
        }

        public SuccessResponseModel(IEnumerable<T> multipleModels) : base(multipleModels, null, HttpStatusCode.OK)
        {
        }
    }
}
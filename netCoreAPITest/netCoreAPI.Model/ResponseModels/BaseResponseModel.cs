using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace netCoreAPI.Model.ResponseModels
{
    public class BaseResponseModel<T> where T : class
    {
        public BaseResponseModel()
        {
            Result = new List<T>();
        }

        public BaseResponseModel(string userFriendlyMessage, HttpStatusCode httpStatusCode) : this()
        {
            this.StatusCode = httpStatusCode;
            this.UserFriendlyMessage = userFriendlyMessage;
        }

        public BaseResponseModel(T singleModel, string userFriendlyMessage, HttpStatusCode httpStatusCode) : this(userFriendlyMessage, httpStatusCode)
        {
            Result.Add(singleModel);
        }

        public BaseResponseModel(IEnumerable<T> multipleModels, string userFriendlyMessage, HttpStatusCode httpStatusCode) : this(userFriendlyMessage, httpStatusCode)
        {
            Result.AddRange(multipleModels);
        }

        public string UserFriendlyMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T> Result { get; private set; }
    }
}
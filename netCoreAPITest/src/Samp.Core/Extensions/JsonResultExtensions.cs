using Microsoft.AspNetCore.Http;
using Samp.Core.Interfaces;
using Samp.Core.Results.Abstracts;
using System;
using System.Globalization;

namespace Samp.Core.Extensions
{
    public static class JsonResultExtensions
    {
        #region Request Tracking Id

        /// <summary>
        /// adds RequestId for the <see cref="BaseResponseModel"/> object.
        /// </summary>
        /// <param name="body"></param>
        public static void SetRequestTrackingId(this IResponseModel body)
        {
            body.Stats.RId = System.Diagnostics.Activity.Current?.RootId;
        }

        #endregion Request Tracking Id

        #region Measurement of The Response Time

        /// <summary>
        /// adds ResponseTime, time set in <see cref="ResponseTimeMeasurementExtensions"/> for the <see cref="BaseResponseModel"/> object.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="context"></param>
        public static void SetMeasuredResponsTime(this IResponseModel body, HttpContext context)
        {
            var requestStartDateTime = DateTime.Parse(context.Items[Constants.RequestStartTime].ToString());
            var elapsedResponseTime = DateTime.UtcNow - requestStartDateTime;
            body.Stats.ElapsedMilliseconds = elapsedResponseTime.TotalMilliseconds.ToString("####0.0", CultureInfo.InvariantCulture);
        }

        #endregion Measurement of The Response Time
    }
}
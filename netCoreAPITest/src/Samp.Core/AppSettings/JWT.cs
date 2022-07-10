using System;

namespace Samp.Core.AppSettings
{
    public class JWT
    {
        public string ValidAudience { get; set; }
        public Uri ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
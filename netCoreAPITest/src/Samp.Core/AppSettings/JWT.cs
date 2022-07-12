using System;

namespace Samp.Core.AppSettings
{
    public class JWT
    {
        public JWT()
        {
            AccessTokenExpiresIn = 1;
            RefreshTokenExpiresIn = 8350;
        }

        public string ValidAudience { get; set; }
        public Uri ValidIssuer { get; set; }
        public string AccessTokenSecret { get; set; }
        public string RefreshTokenSecret { get; set; }

        /// <summary>
        /// hours
        /// </summary>
        public int RefreshTokenExpiresIn { get; set; }

        /// <summary>
        /// hours
        /// </summary>

        public int AccessTokenExpiresIn { get; set; }
    }
}
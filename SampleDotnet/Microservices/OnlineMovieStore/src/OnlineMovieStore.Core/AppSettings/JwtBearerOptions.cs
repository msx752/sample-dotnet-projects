using System;

namespace SampleProject.Core.AppSettings
{
    public class JwtBearerOptions
    {
        public JwtBearerOptions()
        {
            AccessTokenExpiresIn = 1;
            RefreshTokenExpiresIn = 8350;
        }

        public string ValidAudience { get; set; }
        public Uri ValidIssuer { get; set; }
        public string Secret { get; set; }

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
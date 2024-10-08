﻿using Identity.Database;
using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SampleProject.Core.AppSettings;
using SampleProject.Identity.API.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleProject.Identity.API.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IDbContextFactory<IdentityDbContext> _dbContextFactory;
        private readonly JwtBearerOptions jwt;

        public TokenHelper(
            IOptions<IdentityApplicationSettings> appSettings)
        {
            jwt = appSettings.Value.JwtBearerOptions;
        }

        public TokenDto Authenticate(UserEntity user, IEnumerable<Claim> claims = null)
        {
            var refreshToken = GenerateRefreshToken(out DateTime refreshTokenExpiresAt);

            var generatedRefreshToken = new RefreshTokenEntity()
            {
                RefreshToken = refreshToken,
                User = user
            };
            user.RefreshTokens.Add(generatedRefreshToken);

            var accessToken = GenerateAccessToken(claims, out DateTime AccessTokenExpiresAt);

            return new TokenDto()
            {
                refresh_token = refreshToken,
                access_token = accessToken,
                ExpiresAt = AccessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
            };
        }

        public string GenerateRefreshToken(out DateTime expiresAt)
        {
            var symmetricSecurityKeyRefreshToken = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(jwt.Secret));
            var signingCredentialsRefreshToken = new SigningCredentials(symmetricSecurityKeyRefreshToken, SecurityAlgorithms.HmacSha256);
            return GenerateJwtSecurityToken(signingCredentialsRefreshToken, jwt.RefreshTokenExpiresIn, out expiresAt, null);
        }

        public bool ValidateRefreshToken(string refresh_token)
        {
            try
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(jwt.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                jwtSecurityTokenHandler.ValidateToken(refresh_token, tokenValidationParameters, out SecurityToken _);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenerateAccessToken(IEnumerable<Claim> userClaims, out DateTime expiresAt)
        {
            var datetimeNow = DateTime.UtcNow;
            var symmetricSecurityKeyAccessToken = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(jwt.Secret));
            var signingCredentialsAccessToken = new SigningCredentials(symmetricSecurityKeyAccessToken, SecurityAlgorithms.HmacSha256);
            return GenerateJwtSecurityToken(signingCredentialsAccessToken, jwt.AccessTokenExpiresIn, out expiresAt, userClaims);
        }

        private string GenerateJwtSecurityToken(SigningCredentials signingCredentials, int expiresIn, out DateTime expiresAt, IEnumerable<Claim> claims = null)
        {
            var dtNow = DateTime.UtcNow;
            expiresAt = dtNow.AddHours(expiresIn);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = dtNow,
                Expires = expiresAt,
                SigningCredentials = signingCredentials,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            //JwtSecurityToken securityToken = new(jwt.ValidIssuer.ToString(), jwt.ValidAudience,
            //    claims,
            //    dtNow,
            //    expiresAt,
            //    signingCredentials);
            //return securityToken;
        }
    }
}
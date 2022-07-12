using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Samp.Auth.API.Models.Dto;
using Samp.Core.AppSettings;
using Samp.Core.Interfaces.Repositories;
using Samp.Identity.Core.Migrations;
using Samp.Identity.Database.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Samp.Identity.API.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly ISharedRepository<IdentityDbContext> repository;
        private readonly TokenValidationParameters tokenValidationParameters;
        private readonly JWT jwt;

        public TokenHelper(
            IOptions<IdentityApplicationSettings> appSettings
            , ISharedRepository<IdentityDbContext> repository
            , TokenValidationParameters tokenValidationParameters)
        {
            this.repository = repository;

            jwt = appSettings.Value.JWT;
            this.tokenValidationParameters = tokenValidationParameters;
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
            repository.Commit(user.Id);
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
            var symmetricSecurityKeyRefreshToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.RefreshTokenSecret));
            var signingCredentialsRefreshToken = new SigningCredentials(symmetricSecurityKeyRefreshToken, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = GenerateJwtSecurityToken(signingCredentialsRefreshToken, jwt.RefreshTokenExpiresIn, out expiresAt, null);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public bool ValidateRefreshToken(string refresh_token)
        {
            try
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
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
            var symmetricSecurityKeyAccessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.AccessTokenSecret));
            var signingCredentialsAccessToken = new SigningCredentials(symmetricSecurityKeyAccessToken, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = GenerateJwtSecurityToken(signingCredentialsAccessToken, jwt.AccessTokenExpiresIn, out expiresAt, userClaims);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private JwtSecurityToken GenerateJwtSecurityToken(SigningCredentials signingCredentials, int expiresIn, out DateTime expiresAt, IEnumerable<Claim> claims = null)
        {
            var dtNow = DateTime.UtcNow;
            expiresAt = dtNow.AddHours(expiresIn);

            JwtSecurityToken securityToken = new(jwt.ValidIssuer.ToString(), jwt.ValidAudience,
                claims,
                dtNow,
                expiresAt,
                signingCredentials);
            return securityToken;
        }
    }
}
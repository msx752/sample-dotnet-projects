using Samp.Auth.API.Models.Dto;
using Samp.Identity.Database.Entities;
using System.Security.Claims;

namespace Samp.Identity.API.Helpers
{
    public interface ITokenHelper
    {
        TokenDto Authenticate(UserEntity user, IEnumerable<Claim> claims = null);

        string GenerateAccessToken(IEnumerable<Claim> userClaims, out DateTime expiresAt);

        string GenerateRefreshToken(out DateTime expiresAt);

        bool ValidateRefreshToken(string refresh_token);
    }
}
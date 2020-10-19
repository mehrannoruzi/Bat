using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Bat.AspNetCore
{
    public interface IJwtService
    {
        JwtToken CreateToken(SecurityTokenDescriptor securityTokenDescriptor);
        JwtToken CreateToken(string userData, JwtSettings jwtSettings);
        JwtToken CreateToken(List<Claim> userClaims, JwtSettings jwtSettings);
        ClaimsPrincipal GetClaimsPrincipal(string token, JwtSettings jwtSettings);
        SecurityToken ReadToken(string token);
        JwtSecurityToken ReadJwtToken(string token);
        TokenValidationTime GetTokenExpireTime(string token, JwtSettings jwtSettings);
    }
}
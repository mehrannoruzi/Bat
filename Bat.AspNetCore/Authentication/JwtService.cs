using System.Security.Claims;

namespace Bat.AspNetCore;

public class JwtService : IJwtService
{
    public JwtToken CreateToken(SecurityTokenDescriptor securityTokenDescriptor)
        => new JwtToken(securityTokenDescriptor);

    public JwtToken CreateToken(string userData, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->"); // Longer than 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>"); //Must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var climsDictionary = new Dictionary<string, object>
            {
                { "userData", userData }
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.UserData, userData)
            }),
            Claims = climsDictionary,
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            IssuedAt = DateTime.Now,
            Expires = jwtSettings.ExpirationMinutes != 0 ? DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes) : DateTime.UtcNow.AddMinutes(30),
            NotBefore = jwtSettings.NotBeforeMinutes != 0 ? DateTime.UtcNow.AddMinutes(jwtSettings.NotBeforeMinutes) : DateTime.UtcNow.AddMinutes(0),
        };

        return new JwtToken(tokenDescriptor);
    }

    public JwtToken CreateToken(List<Claim> userClaims, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->"); // Longer than 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>"); //Must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var climsDictionary = new Dictionary<string, object>();
        userClaims.ForEach(item =>
        {
            climsDictionary.Add(item.Type, item.Value);
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Claims = climsDictionary,
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            IssuedAt = DateTime.Now,
            Expires = jwtSettings.ExpirationMinutes != 0 ? DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes) : DateTime.UtcNow.AddMinutes(30),
            NotBefore = jwtSettings.NotBeforeMinutes != 0 ? DateTime.UtcNow.AddMinutes(jwtSettings.NotBeforeMinutes) : DateTime.UtcNow.AddMinutes(0),
        };

        return new JwtToken(tokenDescriptor);
    }

    public ClaimsPrincipal GetClaimsPrincipal(string token, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->"); // Longer than 16 character
        var issuerSigningKey = new SymmetricSecurityKey(secretKey);

        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>"); //Must be 16 character
        var tokenDecryptionKey = new SymmetricSecurityKey(encryptionkey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrEmpty(jwtSettings.Issuer),
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = !string.IsNullOrEmpty(jwtSettings.Audience),
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            TokenDecryptionKey = tokenDecryptionKey,
            IssuerSigningKey = issuerSigningKey
        };

        var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW, StringComparison.InvariantCultureIgnoreCase)) return null;

        return principal;
    }

    public SecurityToken ReadToken(string token)
        => new JwtSecurityTokenHandler().ReadToken(token);

    public JwtSecurityToken ReadJwtToken(string token)
        => new JwtSecurityTokenHandler().ReadJwtToken(token);

    public TokenValidationTime GetTokenExpireTime(string token, JwtSettings jwtSettings)
    {
        var secretKey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ? jwtSettings.SecretKey : "<-- Mehran@Norouzi|123456789987654321|Mehran@Norouzi -->"); // Longer than 16 character
        var issuerSigningKey = new SymmetricSecurityKey(secretKey);

        var encryptionkey = Encoding.UTF8.GetBytes(!string.IsNullOrWhiteSpace(jwtSettings.Encryptionkey) ? jwtSettings.Encryptionkey : "<Mehran@Norouzi>"); //Must be 16 character
        var tokenDecryptionKey = new SymmetricSecurityKey(encryptionkey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrEmpty(jwtSettings.Issuer),
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = !string.IsNullOrEmpty(jwtSettings.Audience),
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
            TokenDecryptionKey = tokenDecryptionKey,
            IssuerSigningKey = issuerSigningKey
        };

        new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        return new TokenValidationTime { ValidFrom = jwtSecurityToken.ValidFrom.AddHours(3.5), ValidTo = jwtSecurityToken.ValidTo.AddHours(3.5) };
    }
}
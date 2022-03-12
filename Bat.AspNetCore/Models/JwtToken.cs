namespace Bat.AspNetCore;

public class JwtToken
{
    public string Token { get; set; }
    public string TokenType { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }

    public JwtToken() { }

    public JwtToken(SecurityTokenDescriptor securityTokenDescriptor)
    {
        var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);
        Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        TokenType = "Bearer";
        ExpireTime = securityToken.ValidTo.AddHours(3.5);
        RefreshToken = Randomizer.GetRandomString(32);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.BLL.Jwt;

public class JwtGenerator
{
    private static readonly TimeSpan ExpirationSpan = TimeSpan.FromHours(1);
    private readonly IConfiguration _configuration;
    
    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration[IdentityConstants.CONFIG_SECTION_KEY]!));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken token = new(
            issuer: _configuration[IdentityConstants.CONFIG_SECTION_ISSUER],
            audience: _configuration[IdentityConstants.CONFIG_SECTION_AUDIENCE],
            claims: claims,
            expires: DateTime.Now + ExpirationSpan,
            signingCredentials: credentials);
    
        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
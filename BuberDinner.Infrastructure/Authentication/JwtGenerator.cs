using BuberDinner.Application.Commons.Interfaces;
using BuberDinner.Application.Commons.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtGenerator : IJWTGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        public JwtGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettingOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettingOptions.Value;
        }
        public string GenerateToken(Guid Uid, string FirstName, string LastName)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,LastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var security = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpriryMinutes),
                claims: claims,
                signingCredentials: signingCredentials,
                audience: _jwtSettings.Audience
            );
            return new JwtSecurityTokenHandler().WriteToken(security);
        }
    }
}

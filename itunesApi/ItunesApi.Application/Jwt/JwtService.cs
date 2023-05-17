using ItunesApi.Application.Dtos;
using ItunesApi.Application.helper.IJwtServices;
using ItunesApi.Domain.Model;
using ItunesApi.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ItunesApi.Application.helper
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration configuration, IScoreRepository scoreRepository)
        {
            this.configuration = configuration;
        }
        public string CreateToken(AccountDto accountDto)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountDto.Id.ToString()),
            };
            var payload = new JwtPayload(configuration.GetSection("AppSettings:Issuer").Value, configuration.GetSection("AppSettings:Audience").Value, claims, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}

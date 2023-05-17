using ItunesApi.Application.Dtos;
using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Application.helper.IJwtServices
{
    public interface IJwtService
    {
        //JwtSecurityToken Verify(string token);
        string CreateToken(AccountDto accountDto);
    }
}

using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Infrastructure.Interface
{
    public interface IAccountRepository
    {
       Task<Account> AccountAsyncEmail(string email);
       Task<Account> AccountAsyncHashLogin(Account Account);
       Task<Account> CreateAccountAsync(Account login);
       Task<Account> AccountAsyncId(int id);
       Task<List<Account>> Leaderbord();
    };
}

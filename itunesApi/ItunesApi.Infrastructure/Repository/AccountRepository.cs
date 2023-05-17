using ItunesApi.Domain.Model;
using ItunesApi.Infrastructure.Context;
using ItunesApi.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using System.Web.Http;

namespace ItunesApi.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext loginDbContext;
        public AccountRepository(AccountDbContext loginDbContext)
        {
            this.loginDbContext = loginDbContext;
        }

        public Task<Account> AccountAsyncEmail(string email)
        {
            var accountEmail = loginDbContext.Logins.FirstOrDefaultAsync(a => a.Email == email);
            return accountEmail;
        }

        public Task<Account> AccountAsyncId(int id)
        {
            var accountId = loginDbContext.Logins.FirstOrDefaultAsync(a => a.Id == id);
            return accountId;
        }

        public async Task<Account> AccountAsyncHashLogin(Account Account)
        {
            var accountUserName = await loginDbContext.Logins.FirstOrDefaultAsync(a => a.UserName == Account.UserName);
            return accountUserName;
        }

        public async Task<Account> CreateAccountAsync(Account Account)
        {
            await loginDbContext.Logins.AddAsync(Account);
            await loginDbContext.SaveChangesAsync();
            return Account;
        }

        public Task<List<Account>> Leaderbord()
        {
            var leaderboards = loginDbContext.Logins.Include(l => l.ScoreList);
            return Task.FromResult(leaderboards.ToList());
        }

    }
}

using ItunesApi.Application.Dtos;
using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Application.BusinesLogic.IBusinessLogic
{
    public interface IAccountsBL
    {
        Task<string> GetAccountsByUserName(AccountDto loginDto);
        Task<Account> CreateAccount(AccountDto loginDto);
        Task<Account> GetAccountById(int id);
        Task<List<ScoreResult>> TopLeaderboard();
        Task<List<ScoreResult>> MonthlyLeaderboard();
        Task<List<ScoreResult>> DailyLeaderboard();
        Task<List<ScoreResult>> YearlyLeaderboard();
    }
}

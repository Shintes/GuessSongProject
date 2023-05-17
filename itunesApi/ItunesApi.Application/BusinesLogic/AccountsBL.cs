using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Application.Dtos;
using ItunesApi.Application.helper.IJwtServices;
using ItunesApi.Domain.Model;
using ItunesApi.Infrastructure.Interface;
using System.Net;
using System.Security.Cryptography;
using System.Web.Http;

namespace ItunesApi.Application.BusinesLogic
{
    public class AccountsBL : IAccountsBL
    {
        private readonly IAccountRepository loginRepository;
        private readonly IJwtService jwtService;
        private readonly IScoreRepository scoreRepository;
        public AccountsBL(IAccountRepository loginRepository, IJwtService jwtService, IScoreRepository scoreRepository)
        {
            this.loginRepository = loginRepository;
            this.jwtService = jwtService;
            this.scoreRepository = scoreRepository;
        }

        public async Task<string> GetAccountsByUserName(AccountDto accountDto)
        {
            var getEmail = await loginRepository.AccountAsyncEmail(accountDto.Email);
            if (getEmail == null)
            {
                return "WrongEmail";
            }
            if (!VerifyPasswordHash(accountDto.Password, getEmail.PasswordHash, getEmail.PasswordSalt))
            {
                return "WrongPassword";
            }
            accountDto.Id = getEmail.Id;

            string createtoken = jwtService.CreateToken(accountDto);
            return createtoken;
        }

        public async Task<Account> GetAccountById(int id)
        {
            var getUserById = await loginRepository.AccountAsyncId(id);
            if (getUserById == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return getUserById;
        }
        public async Task<Account> CreateAccount(AccountDto loginDto)
        {
            CreatePasswordHash(loginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Account login = new();
            {
                login.UserName = loginDto.UserName;
                login.PasswordHash = passwordHash;
                login.PasswordSalt = passwordSalt;
                login.Email = loginDto.Email;
            };
            var createdAccount = await loginRepository.CreateAccountAsync(login);
            Score score = new();
            {
                score.AccountId = createdAccount.Id;
                score.Date = DateTime.Now;
                score.Points = 0;
            }
            await scoreRepository.CreateScoreAsync(score);
            return login;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        } 

        public Task<List<ScoreResult>> TopLeaderboard()
        {
            var leaderboard = loginRepository.Leaderbord().Result.Select(account =>
            new ScoreResult
            {
                AccountId = account.Id,
                Score = account.ScoreList.Where(w => w.AccountId == w.AccountId).Sum(p => p.Points),
                UserName = account.UserName
            }).OrderByDescending(x => x.Score);

            return Task.FromResult(leaderboard.ToList());
        }

        public Task<List<ScoreResult>> MonthlyLeaderboard()
        {
            var leaderboard = loginRepository.Leaderbord().Result.Select(account =>
                new ScoreResult
                {
                    AccountId = account.Id,
                    Score = account.ScoreList.Where(d => d.Date.Month == DateTime.Now.Month && d.Date.Year == DateTime.Now.Year).Where(w => w.AccountId == w.AccountId).Sum(p => p.Points),
                    UserName = account.UserName
                }).OrderByDescending(x => x.Score);

            return Task.FromResult(leaderboard.ToList());
        }
        public Task<List<ScoreResult>> DailyLeaderboard()
        {
            var leaderboard = loginRepository.Leaderbord().Result.Select(account =>
                new ScoreResult
                {
                    AccountId = account.Id,
                    Score = account.ScoreList.Where(d => d.Date.Date == DateTime.Now.Date).Where(w => w.AccountId == w.AccountId).Sum(p => p.Points),
                    UserName = account.UserName
                }).OrderByDescending(x => x.Score);

            return Task.FromResult(leaderboard.ToList());
        }
        public Task<List<ScoreResult>> YearlyLeaderboard()
        {
            var leaderboard = loginRepository.Leaderbord().Result.Select(account =>
                new ScoreResult
                {
                    AccountId = account.Id,
                    Score = account.ScoreList.Where(d => d.Date.Year == DateTime.Now.Year).Where(w => w.AccountId == w.AccountId).Sum(p => p.Points),
                    UserName = account.UserName
                }).OrderByDescending(x => x.Score);

            return Task.FromResult(leaderboard.ToList());
        }
    }
}

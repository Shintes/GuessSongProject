using ItunesApi.Domain.Model;
using ItunesApi.Infrastructure.Context;
using ItunesApi.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace ItunesApi.Infrastructure.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AccountDbContext  loginDbContext;
        public ScoreRepository(AccountDbContext loginDbContext)
        {
            this.loginDbContext = loginDbContext;
        }
        public async Task<Score> CreateScoreAsync(Score score)
        {
            score.Account = await loginDbContext.Logins.FirstOrDefaultAsync(a => a.Id == score.AccountId);
            await loginDbContext.Scores.AddAsync(score);
            await loginDbContext.SaveChangesAsync();
            return score;
        }

        public Task<Score> GetScoreAsync(int id)
        {
            var scoreId = loginDbContext.Scores.OrderByDescending(d=> d.Date).FirstOrDefaultAsync(b => b.Account.Id == id);
            return scoreId;
        }

        public async Task UpdateScoreAsync(Score score)
        {
            var dbScore = loginDbContext.Scores.FirstOrDefault(b => b.Id == score.Id);
            dbScore.Points = score.Points;
            dbScore.Date = DateTime.Now;
            await loginDbContext.SaveChangesAsync();

        }

    }
}

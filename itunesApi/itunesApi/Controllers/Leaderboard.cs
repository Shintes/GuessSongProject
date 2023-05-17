using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace itunesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Leaderboard : Controller
    {
        private readonly IAccountsBL getAccountsBL;
        public Leaderboard(IAccountsBL getAccountsBL)
        {
            this.getAccountsBL = getAccountsBL;
        }


        [HttpGet("leaderboard")]
        public Task<List<ScoreResult>> TopLeaderboard()
        {
            var existingLeaderboard = getAccountsBL.TopLeaderboard();
            return existingLeaderboard;
        }


        [HttpGet("monthlyLearderboard")]
        public Task<List<ScoreResult>> MonthyLeaderboard()
        {
            var existingLeaderboard = getAccountsBL.MonthlyLeaderboard();
            return existingLeaderboard;
        }

        [HttpGet("dailyLeaderboard")]
        public Task<List<ScoreResult>> DailyLeaderboard()
        {
            var existingLeaderboard = getAccountsBL.DailyLeaderboard();
            return existingLeaderboard;
        }

        [HttpGet("yearlyLeaderboard")]
        public Task<List<ScoreResult>> YearlyLeaderboard()
        {
            var existingLeaderboard = getAccountsBL.YearlyLeaderboard();
            return existingLeaderboard;
        }
    }
}

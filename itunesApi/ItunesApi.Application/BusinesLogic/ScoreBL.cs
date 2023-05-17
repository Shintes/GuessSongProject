using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Application.Dtos;
using ItunesApi.Domain.Model;
using ItunesApi.Infrastructure.Interface;

namespace ItunesApi.Application.BusinesLogic
{
    public class ScoreBL : IScoreBL
    {
        private readonly IScoreRepository scoreRepository;

        public ScoreBL(IScoreRepository scoreRepository)
        {
            this.scoreRepository = scoreRepository;
        }

        public async Task<Score> CreateScore(ScoreDto scoreDto)
        {
            
            var existingScore = await scoreRepository.GetScoreAsync(scoreDto.AccountId);
            if (existingScore != null)
            {
                if (existingScore.Date.Day != DateTime.Now.Day)
                {
                    Score score = new()
                    {
                        Points = scoreDto.Points,
                        Date = DateTime.Now,
                        AccountId = scoreDto.AccountId,
                    };
                    await scoreRepository.CreateScoreAsync(score);
                    return score;
                }
                else
                {
                    Score score = new()
                    {
                        Id = scoreDto.Id,
                        Points = scoreDto.Points + existingScore.Points,
                        AccountId = scoreDto.AccountId,
                    };
                    await scoreRepository.UpdateScoreAsync(score);
                    return score;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<Score> GetScore(int id)
        {
            var existingScore = await scoreRepository.GetScoreAsync(id);
            return existingScore;
        }

        public async Task<Score> UpdateScore(ScoreDto scoreDto)
        {
            Score score = new()
            {
                Id = scoreDto.Id,
                AccountId = scoreDto.AccountId,
                Points = scoreDto.Points,
                Date = scoreDto.Date,
            };
            await scoreRepository.UpdateScoreAsync(score);
            return score;
        }
    }
}
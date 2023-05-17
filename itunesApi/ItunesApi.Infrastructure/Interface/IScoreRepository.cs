using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Infrastructure.Interface
{
    public interface IScoreRepository
    {
        Task<Score> GetScoreAsync(int id);
        Task UpdateScoreAsync(Score score);
        Task<Score> CreateScoreAsync(Score Score);
    }
}

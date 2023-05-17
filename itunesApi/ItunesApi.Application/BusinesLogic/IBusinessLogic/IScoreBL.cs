using ItunesApi.Application.Dtos;
using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Application.BusinesLogic.IBusinessLogic
{
    public interface IScoreBL
    {
        Task<Score> GetScore(int id);
        Task<Score> CreateScore(ScoreDto scoreDto);
        Task<Score> UpdateScore(ScoreDto scoreDto);
    }
}

using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Application.Dtos;
using ItunesApi.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace itunesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : Controller
    {
        private readonly IScoreBL getScoreBL;
        public ScoreController(IScoreBL getScoreBl)
        {
            this.getScoreBL = getScoreBl;
        }
        [Authorize]
        [HttpGet("score")]
        public async Task<ActionResult<Score>> GetScore([FromQuery] int id)
        {
            var existingScore = await getScoreBL.GetScore(id);
            return existingScore;
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<ScoreDto>> UpdateScore(ScoreDto scoreDto)
        {
            var existingScore = await getScoreBL.UpdateScore(scoreDto);
            return Ok(existingScore);
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<ScoreDto>> CreateScore(ScoreDto scoreDto)
        {
            var existingScore = await getScoreBL.CreateScore(scoreDto);
            return Ok(existingScore);
        }

    }
}

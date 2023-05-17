using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
namespace itunesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetSongsController : ControllerBase
    {
        private readonly ISongsRockBL getSongsRockBL;
        private readonly ISongsRapBL getSongsRapBL;
        private readonly ISongsHitsBL getSongsHitsBL;
        public GetSongsController (ISongsRockBL getSongsRockBL, ISongsRapBL getSongsRapBL, ISongsHitsBL songsManeleBL)
        {
            this.getSongsRockBL = getSongsRockBL;
            this.getSongsRapBL = getSongsRapBL;
            this.getSongsHitsBL = songsManeleBL;
        }
        [HttpGet("rock")]
        public async Task<ActionResult> GetRockSong()
        {
            var getSongss = await getSongsRockBL.GetSongsRockCall();
            return Ok(getSongss);
        }

        [HttpGet("rap")]
        public async Task<ActionResult> GetRapSong()
        {
            var getSongss = await getSongsRapBL.GetSongsRapCall();
            return Ok(getSongss);
        }

        [HttpGet("hits")]
        public async Task<ActionResult> GetManeleSong()
        {
            var getSongss = await getSongsHitsBL.GetSongsHitsCall();
            return Ok(getSongss);
        }
    }
}

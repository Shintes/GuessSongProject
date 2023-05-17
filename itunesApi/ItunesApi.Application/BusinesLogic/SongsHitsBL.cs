using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Domain.Model;
using Newtonsoft.Json;

namespace ItunesApi.Application.BusinesLogic
{
    public class SongsHitsBL : ISongsHitsBL
    {
        public async Task<Rootobject.Songs> GetSongsHitsCall()
        {
            int limitMax = 10;
            var hitsCategory = new List<string>()
            {
                "OneRepublic", "LadyGaga", "TheWeeknd", "LilNasX", "JustinBieber", "NickiMinaj", "PostMalone", "Rihanna", "Drake", "DojaCat", "Sia", 
                "Pharrell Williams", "Adele", "MichaelJackson",  "JimiHendrix ", "BobMarley", "Eminem", "Madonna", "Queen", "Jay-Z", "Ye"
            };
            Random randomHitsCategory = new();
            int indexHits = randomHitsCategory.Next(hitsCategory.Count);
            string randomHitsCategoryIndex = hitsCategory[indexHits];

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://itunes.apple.com/search?term=" + randomHitsCategoryIndex + "&limit=" + limitMax + "&entity=musicTrack&attribute=allArtistTerm");
            response.EnsureSuccessStatusCode();
            using HttpContent content = response.Content;
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Rootobject>(responseBody);
            Random randomSong = new();
            var randomSongApi = randomSong.Next(0, limitMax);
            return result.results[randomSongApi];
        }
    }
}

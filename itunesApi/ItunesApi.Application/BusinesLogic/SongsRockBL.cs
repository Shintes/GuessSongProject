using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Domain.Model;
using Newtonsoft.Json;

namespace ItunesApi.Application.BusinesLogic
{
    public class SongsRockBL : ISongsRockBL
    {
        public async Task<Rootobject.Songs> GetSongsRockCall()
        {
            int limitMax = 10;
            var offsetRandom = new Random();
            var rockCategory = new List<string>
            {
                "Metallica", "IronMaiden", "LinkinPark", "Sabaton", "Megadeth", "Slayer", "Korn", "Katatonia", "Warbringer", "DeepPurple", "Slipknot",
                "LimpBizkit", "BlackSabbath", "JudasPriest", "RedHotChiliPeppers", "Scorpions", "Dio"
            };
            Random randomRockCategory = new();
            int indexRock = randomRockCategory.Next(rockCategory.Count);
            string randomRockCategoryIndex = rockCategory[indexRock];

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://itunes.apple.com/search?term="+ randomRockCategoryIndex + "&limit="+limitMax+"&offset="+offsetRandom.Next(200)+ "&entity=musicTrack&attribute=allArtistTerm");
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

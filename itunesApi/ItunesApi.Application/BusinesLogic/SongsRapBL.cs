using ItunesApi.Application.BusinesLogic.IBusinessLogic;
using ItunesApi.Domain.Model;
using Newtonsoft.Json;

namespace ItunesApi.Application.BusinesLogic
{
    public class SongsRapBL : ISongsRapBL
    {
        public async Task<Rootobject.Songs> GetSongsRapCall()
        {
            int limitMax = 10;
            var offsetRandom = new Random();
            var rapCategory = new List<string>
            {
                "Drake", "Eminem", "fiftyCent", "SnoopDogg", "DrDre", "Migos", "IceCube", "xzibit", "TuPac",
                "PostMalone", "Ye", "KendrickLamar", "Nas", "Redman", "FatJoe", "Macklemore",
            };
            Random randomRapCategory = new();
            int indexRap = randomRapCategory.Next(rapCategory.Count);
            string randomRapCategoryIndex = rapCategory[indexRap];

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://itunes.apple.com/search?term=" + randomRapCategoryIndex + "&limit=" + limitMax + "&offset=" + offsetRandom.Next(200) + "&entity=musicTrack&attribute=allArtistTerm");
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

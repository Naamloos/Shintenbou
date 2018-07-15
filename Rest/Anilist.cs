using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;

namespace Shintenbou.Rest
{
    public static class Anilist
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<IEnumerable<AnilistAnime>> GetAnimeByNameAsync(string name)
        {
            string schema = string.Empty;
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistAnime.graphql");

            using (var ms = new MemoryStream())
            {
                await resourceStream.CopyToAsync(ms);

                using (var sr = new StreamReader(ms))
                    schema = await sr.ReadToEndAsync();
            }
            
            var req = new AnilistRequest();
            req.Query = schema;
            req.Variables = new Dictionary<string, string>();
            req.Variables.Add("search", name.Replace("'", string.Empty));
            
            using (var reqMessage = new HttpRequestMessage())
            {
                reqMessage.Content = new StringContent(JsonConvert.SerializeObject(req));
                reqMessage.Method = HttpMethod.Post;
                var response = await _client.SendAsync(reqMessage);
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<AnilistAnime>>(content);
            }
        }
    }
}

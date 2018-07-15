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

        public static async Task<IEnumerable<AnilistAnime>> GetAnimeByName(string name)
        {
            string schema = string.Empty;
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistAnime.graphql");

            using (var ms = new MemoryStream())
            {
                resourceStream.CopyTo(ms);

                using (var sr = new StreamReader(ms))
                    schema = sr.ReadToEnd();
            }
            
            var req = new AnilistRequest();
            req.Query = schema;
            req.Variables.Add("search", name.Replace("'", string.Empty));
            
            using (var reqMessage = new HttpRequestMessage())
            {
                reqMessage.Content = JsonConvert.SerializeObject(req);
                reqMessage.Method = HttpMethod.Post;

            }
        }
    }
}

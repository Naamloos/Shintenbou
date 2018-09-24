using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Shintenbou.Rest
{
    public static class Anilist
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static KeyValuePair<int, int> _previousHeaders = new KeyValuePair<int, int>();
        private static HttpClient _client = new HttpClient();
        
        private static async Task<T> SendRequestAsync<T>(AnilistRequest request)
        {
            try
            {
                await _semaphore.WaitAsync();
                using (var reqMessage = new HttpRequestMessage())
                {
                    reqMessage.Content = new StringContent(JsonConvert.SerializeObject(request));
                    reqMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    reqMessage.Method = HttpMethod.Post;
                    reqMessage.RequestUri = new Uri("https://graphql.anilist.co/");
                    if (_previousHeaders.Key == 0) await Task.Delay(_previousHeaders.Value);
                    var response = await _client.SendAsync(reqMessage);
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _previousHeaders = new KeyValuePair<int, int>(int.Parse(response.Headers.GetValues("X-RateLimit-Remaining").First()), int.Parse(response.Headers.GetValues("X-RateLimit-Reset").First()));
                        return JsonConvert.DeserializeObject<T>(JObject.Parse(content)["data"]["Page"]["media"].ToString());
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<AnilistErrorResponse>(content);
                        Console.WriteLine($"API Error:\n{string.Join("\n", error.Errors.Select(x => x.Message))}");
                        var stream = File.CreateText($"{AppContext.BaseDirectory}/errordump.txt");
                        await stream.WriteLineAsync($"Errors:\n{string.Join("\n", error.Errors.Select(x => x.Message))}");
                        stream.Close();
                        stream.Dispose();
                        return default(T);
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public static async Task<IEnumerable<AnilistAnime>> GetAnimeByNameAsync(string name)
        {
            string schema = string.Empty;
            //Load the Schema File into a stream
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistAnime.graphql");
            //Copy it to the StreamReader and read the content
            using(var sr = new StreamReader(resourceStream))
                schema = await sr.ReadToEndAsync();
            //Initializing Anilist Request
            var req = new AnilistRequest
            {
                //Add query schema
                Query = schema,
                Variables = new Dictionary<string, string>
                {
                //Add variables, if the user didnt provide a name then have it as null.
                    {
                        "search", (name?.Replace("'", string.Empty))
                    }
                }
            };

            return await SendRequestAsync<IEnumerable<AnilistAnime>>(req);
        }

        public static async Task<IEnumerable<AnilistManga>> GetMangaByNameAsync(string name)
        {
            string schema = string.Empty;
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistManga.graphql");
            using(var sr = new StreamReader(resourceStream))
                schema = await sr.ReadToEndAsync();
            var req = new AnilistRequest
            {
                Query = schema,
                Variables = new Dictionary<string, string>
                {
                    {
                        "search", (name?.Replace("'", string.Empty))
                    }
                }
            };

            return await SendRequestAsync<IEnumerable<AnilistManga>>(req);
        }

        public static async Task<AnilistUser?> GetUserByNameAsync(string name, int page = 1, int perPage = 25)
        {
            string schema = string.Empty;
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistUser.graphql");
            using(var sr = new StreamReader(resourceStream))
                schema = await sr.ReadToEndAsync();
            var req = new AnilistRequest
            {
                Query = schema,
                Variables = new Dictionary<string, string>()
            };
            req.Variables.Add("name", (name?.Replace("'", string.Empty)));
            req.Variables.Add("pageNumber", $"{page}");
            req.Variables.Add("perPage", $"{perPage}");
            return await SendRequestAsync<AnilistUser?>(req);
        }
    }
}
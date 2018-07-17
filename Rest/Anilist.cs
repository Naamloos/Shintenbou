using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Shintenbou.Rest
{
    public static class Anilist
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<IEnumerable<AnilistAnime>> GetAnimeByNameAsync(string name)
        {
            string schema = string.Empty;
            //Load the Schema File into a stream
            var resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream("Shintenbou.Rest.Schema.AnilistAnime.graphql");
            //Copy it to the StreamReader and read the content
            using(var sr = new StreamReader(resourceStream))
                schema = await sr.ReadToEndAsync();
            //Initializing Anilist Request
            var req = new AnilistRequest();
            //Add query schema
            req.Query = schema;
            req.Variables = new Dictionary<string, string>();
            //Add variables
            req.Variables.Add("search", (name?.Replace("'", string.Empty) ?? "Attack On Titan"));
            
            using (var reqMessage = new HttpRequestMessage())
            {
                //Serialize the request into JSON and add it as body
                reqMessage.Content = new StringContent(JsonConvert.SerializeObject(req));
                //Tell the API the request is JSON
                reqMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                
                reqMessage.Method = HttpMethod.Post;
                reqMessage.RequestUri = new Uri("https://graphql.anilist.co/");
                //Get the response
                var response = await _client.SendAsync(reqMessage);
                //Read the content of the response
                var content = await response.Content.ReadAsStringAsync();
                //If the request returned successfully
                if(response.IsSuccessStatusCode)
                {
                    //Deserialize it into AnilistRepsonse
                    var data = JsonConvert.DeserializeObject<AnilistResponse>(JObject.Parse(content)["data"].ToString());
                    Console.WriteLine($"Count: {data.Page.Media.Count()}");
                    //Return the Animes
                    return data.Page.Media;
                }
                //Otherwise print the errors and return null
                else
                { 
                    //Deserialize the error
                    var error = JsonConvert.DeserializeObject<AnilistErrorResponse>(content);
                    //Write it out (for Debug mainly)
                    Console.WriteLine($"API Error:\n{string.Join("\n", error.Errors.Select(x => x.Message))}");
                    //Create a dump log (for Release build mainly)
                    var stream = File.CreateText($"{AppContext.BaseDirectory}/errordump.txt");
                    //Write to the file
                    await stream.WriteLineAsync($"Errors:\n{string.Join("\n", error.Errors.Select(x => x.Message))}");
                    stream.Close();
                    return null;
                }
            }
        }
    }
}

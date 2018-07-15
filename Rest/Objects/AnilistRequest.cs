using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, string> Variables { get; set; }
    }
}

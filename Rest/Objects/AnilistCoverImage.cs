using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistCoverImage
    {
        /// <summary>
        /// Url of the large cover image
        /// </summary>
        [JsonProperty("large")]
        public string Large { get; private set; }
        
        /// <summary>
        /// Url of the medium cover image
        /// </summary>
        [JsonProperty("medium")]
        public string Medium { get; private set; }
    }
}

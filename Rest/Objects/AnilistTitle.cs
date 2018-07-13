using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistTitle
    {
        /// <summary>
        /// A english readable name for the medium
        /// </summary>
        [JsonIgnore]
        public string EnglishReadableName => this.English ?? this.Romaji;

        /// <summary>
        /// This may be null if there's only a romaji name for the medium
        /// </summary>
        [JsonProperty("english")]
        public string English { get; private set; }

        /// <summary>
        /// The Romaji name of the medium
        /// </summary>
        [JsonProperty("romaji")]
        public string Romaji { get; private set; }
        
        /// <summary>
        /// The Native name of the medium
        /// </summary>
        [JsonProperty("native")]
        public string Native { get; private set; }
    }
}

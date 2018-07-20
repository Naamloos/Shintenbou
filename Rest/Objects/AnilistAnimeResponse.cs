using System.Collections.Generic;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistAnimeResponse
    {
        /// <summary>
        /// The Anilist Page
        /// </summary>
        [JsonProperty("Page")]
        public AnimePageInfo Page { get; private set; }
    }

    public struct AnimePageInfo
    {
        /// <summary>
        /// Total Number of pages returned
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; private set; }

        /// <summary>
        /// The list of animes that were returned
        /// </summary>
        [JsonProperty("media")]
        public IEnumerable<AnilistAnime> Media { get; private set; }
    }
}

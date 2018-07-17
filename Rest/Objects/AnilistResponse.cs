using System.Collections.Generic;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistResponse
    {
        /// <summary>
        /// The Anilist Page
        /// </summary>
        [JsonProperty("Page")]
        public PageInfo Page { get; private set; }
    }

    public struct PageInfo
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

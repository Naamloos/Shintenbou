using System.Collections.Generic;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistMangaResponse
    {
        /// <summary>
        /// The Anilist Page
        /// </summary>
        [JsonProperty("Page")]
        public MangaPageInfo Page { get; private set; }
    }

    public struct MangaPageInfo
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
        public IEnumerable<AnilistManga> Media { get; private set; }
    }
}

using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistManga
    {
        /// <summary>
        /// The Anilist Id of the Manga
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; private set; }

        /// <summary>
        /// The synopsis/description of the Manga
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }


        /// <summary>
        /// True if the Manga is only for 18+ audiences
        /// </summary>
        [JsonProperty("isAdult")]
        public bool IsAdult { get; private set; }

        /// <summary>
        /// The title of the Manga
        /// </summary>
        [JsonProperty("title")]
        public AnilistTitle Title { get; private set; }

        /// <summary>
        /// The cover image of the Manga
        /// </summary>
        [JsonProperty("coverImage")]
        public AnilistCoverImage CoverImage { get; private set; }

        /// <summary>
        /// The mean score of the Manga
        /// </summary>
        [JsonProperty("meanScore")]
        public int? MeanScore { get; private set; }

        /// <summary>
        /// The start date of the Manga
        /// </summary>
        [JsonProperty("startDate")]
        public AnilistDate StartDate { get; private set; }

        /// <summary>
        /// The end date of the Manga
        /// </summary>
        [JsonProperty("endDate")]
        public AnilistDate EndDate { get; private set; }

        /// <summary>
        /// The url to the Anilist site for this Manga
        /// </summary>
        [JsonProperty("siteUrl")]
        public string Url { get; private set; }
    }
}

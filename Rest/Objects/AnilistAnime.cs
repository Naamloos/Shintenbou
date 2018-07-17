using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistAnime
    {
        /// <summary>
        /// The Anilist Id of the Anime
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; private set; }

        /// <summary>
        /// The number of Episodes the Anime has
        /// </summary>
        [JsonProperty("episodes")]
        public int? Episodes { get; private set; }

        /// <summary>
        /// The synopsis/description of the Anime
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }


        /// <summary>
        /// True if the Anime is only for 18+ audiences
        /// </summary>
        [JsonProperty("isAdult")]
        public bool IsAdult { get; private set; }
        
        /// <summary>
        /// The title of the Anime
        /// </summary>
        [JsonProperty("title")]
        public AnilistTitle Title { get; private set; }
        
        /// <summary>
        /// The cover image of the Anime
        /// </summary>
        [JsonProperty("coverImage")]
        public AnilistCoverImage CoverImage { get; private set; }

        /// <summary>
        /// The mean score of the Anime
        /// </summary>
        [JsonProperty("meanScore")]
        public int? MeanScore { get; private set; }
    
        /// <summary>
        /// The start date of the Anime
        /// </summary>
        [JsonProperty("startDate")]
        public AnilistDate StartDate { get; private set; }
    
        /// <summary>
        /// The end date of the Anime
        /// </summary>
        [JsonProperty("endDate")]
        public AnilistDate EndDate { get; private set; }
    
        /// <summary>
        /// If the Anime is airing this will return the next episode
        /// </summary>
        [JsonProperty("nextAiringEpisode")]
        public AnilistEpisode NextEpisode { get; private set; }
    
        /// <summary>
        /// The url to the Anilist site for this Anime
        /// </summary>
        [JsonProperty("siteUrl")]
        public string Url { get; private set; }
    }
}

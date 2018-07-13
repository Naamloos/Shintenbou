using System;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public class AnilistEpisode
    {
        /// <summary>
        /// The id of the episode
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; private set; }

        /// <summary>
        /// The time of airing in unix time seconds
        /// </summary>
        [JsonProperty("airingAt")]
        public long AiringAtUnix { get; private set; }
        
        /// <summary>
        /// The time in which this episode will air in seconds
        /// </summary>
        [JsonProperty("airingIn")]
        public long AiringInUnix { get; private set; }

        /// <summary>
        /// The episode number
        /// </summary>
        [JsonProperty("episode")]
        public int Episode { get; set;}
        
        /// <summary>
        /// The date and time the episode is airing at
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset AiringAt => DateTimeOffset.FromUnixTimeSeconds(this.AiringAtUnix);
        
        /// <summary>
        /// The time the episode is airing in
        /// </summary>
        [JsonIgnore]
        public TimeSpan AiringIn => TimeSpan.FromSeconds(this.AiringInUnix);
    }
}

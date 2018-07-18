using System.Collections.Generic;
using Shintenbou.Rest.Objects;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public struct AnilistUser
    {
        [JsonProperty("name")]
        public string Username { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("avatar")]
        public AnilistCoverImage Avatar { get; private set; }

        [JsonProperty("siteUrl")]
        public string ProfileUrl { get; private set; }

        [JsonProperty("about")]
        public string About { get; private set; }

        [JsonProperty("favourites")]
        public AnilistFavourites? Favourites { get; private set; }
    }

    public struct AnilistFavourites
    {
        [JsonProperty("anime")]
        public IEnumerable<AnilistAnime> Animes { get; private set; }

        [JsonProperty("manga")]
        public IEnumerable<AnilistManga> Mangas { get; private set; }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shintenbou.Rest.Objects
{
    public struct AnilistErrorResponse
    {
        [JsonProperty("data")]
        public string Data { get; private set; }

        [JsonProperty("errors")]
        public IReadOnlyList<AnilistError> Errors { get; private set; }
    }

    public struct AnilistError
    {
        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("status")]
        public int Status { get; private set; }

        [JsonProperty("locations")]
        public IReadOnlyList<ErrorLocation> Location { get; private set; }
    }

    public struct ErrorLocation
    {
        [JsonProperty("line")]
        public int Line { get; private set; }

        [JsonProperty("column")]
        public int Column { get; private set; }
    }
}
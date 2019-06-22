using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models
{
    public class RecommendationSeed
    {
        [JsonProperty("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        [JsonProperty("afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("initialPoolSize")]
        public int InitialPoolSize { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
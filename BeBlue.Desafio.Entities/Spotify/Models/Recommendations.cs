using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models
{
    public class Recommendations : BasicModel
    {
        [JsonProperty("seeds")]
        public List<RecommendationSeed> Seeds { get; set; }

        [JsonProperty("tracks")]
        public List<SimpleTrack> Tracks { get; set; }
    }
}
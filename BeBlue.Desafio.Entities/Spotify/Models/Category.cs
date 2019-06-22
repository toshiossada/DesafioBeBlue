using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models {
    public class Category : BasicModel {
        [JsonProperty ("href")]
        public string Href { get; set; }

        [JsonProperty ("icons")]
        public List<Image> Icons { get; set; }

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }
    }
}
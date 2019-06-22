using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models
{
    public class Paging<T> : BasicModel
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        public bool HasNextPage()
        {
            return Next != null;
        }

        public bool HasPreviousPage()
        {
            return Next != null;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models
{
    public class CategoryList: BasicModel
    {
        [JsonProperty("categories")]
        public Paging<Category> Categories { get; set; }
    }
}
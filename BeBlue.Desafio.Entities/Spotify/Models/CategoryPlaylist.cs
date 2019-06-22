using Newtonsoft.Json;

namespace BeBlue.Desafio.Entities.Spotify.Models
{
    public class CategoryPlaylist : BasicModel
    {
        [JsonProperty("playlists")]
        public Paging<SimplePlaylist> Playlists { get; set; }
    }
}
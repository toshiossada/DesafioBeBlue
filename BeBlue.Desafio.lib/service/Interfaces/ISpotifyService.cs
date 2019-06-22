using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Spotify;
using BeBlue.Desafio.Entities.Spotify.Models;

namespace BeBlue.Desafio.lib.service.Interfaces {
    public interface ISpotifyService {
        CategoryList GetCategories (string country = "", string locale = "", int limit = 20, int offset = 0);
        CategoryPlaylist GetCategoryPlaylists (string categoryId, string country = "", int limit = 20, int offset = 0);
        Paging<PlaylistTrack> GetPlaylistTracks (string playlistId, string fields = "", int limit = 100, int offset = 0, string market = "");
        Recommendations GetRecommendations (List<string> artistSeed = null, List<string> genreSeed = null, List<string> trackSeed = null,
            TuneableTrack target = null, TuneableTrack min = null, TuneableTrack max = null, int limit = 20, string market = "");
        Task<bool> Seed ();
    }
}
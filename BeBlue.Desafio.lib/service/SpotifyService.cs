using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.Entities.Spotify;
using BeBlue.Desafio.Entities.Spotify.Interfaces;
using BeBlue.Desafio.Entities.Spotify.Models;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service.Interfaces;
using Newtonsoft.Json;

namespace BeBlue.Desafio.lib.service {
    public class SpotifyService : ISpotifyService {
        private readonly IClient _webClient;
        private readonly ITrackRepository _trackRepository;
        private readonly IGenreRepository _genreRepository;

        private SpotifyAuthentication _authentication { get; set; }
        private readonly string _urlToken;
        private const string APIBase = "https://api.spotify.com/v1";

        public bool UseAutoRetry { get; set; } = false;
        public IEnumerable<int> RetryErrorCodes { get; set; } = new [] { 500, 502, 503 };
        public int RetryTimes { get; set; } = 10;
        public int RetryAfter { get; set; } = 50;

        public SpotifyService (IClient webClient, ITrackRepository trackRepository, IGenreRepository genreRepository) {
            _urlToken = "https://accounts.spotify.com/api/token";
            _authentication = new SpotifyAuthentication ("b1147fe5ecdf4407a778d5af85728d6b", "5b7a0f78a40141779d0ec3421b00e913", "Bearer");
            _webClient = webClient;
            _trackRepository = trackRepository;
            _genreRepository = genreRepository;
        }
        /// <summary>
        ///     Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">
        ///     A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want to narrow the
        ///     list of returned categories to those relevant to a particular country
        /// </param>
        /// <param name="locale">
        ///     The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country
        ///     code, joined by an underscore
        /// </param>
        /// <param name="limit">The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <returns></returns>
        /// <remarks>AUTH NEEDED</remarks>
        public CategoryList GetCategories (string country = "", string locale = "", int limit = 20, int offset = 0) {
            limit = Math.Min (50, limit);
            StringBuilder builder = new StringBuilder (APIBase + "/browse/categories");
            builder.Append ("?limit=" + limit);
            builder.Append ("&offset=" + offset);
            if (!string.IsNullOrEmpty (country))
                builder.Append ("&country=" + country);
            if (!string.IsNullOrEmpty (locale))
                builder.Append ("&locale=" + locale);

            var categories = DownloadData<CategoryList> (builder.ToString ());
            return categories;
        }
        /// <summary>
        ///     Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="categoryId">The Spotify category ID for the category.</param>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0</param>
        /// <returns></returns>
        /// <remarks>AUTH NEEDED</remarks>
        public CategoryPlaylist GetCategoryPlaylists (string categoryId, string country = "", int limit = 20, int offset = 0) {
            limit = Math.Min (50, limit);
            StringBuilder builder = new StringBuilder (APIBase + "/browse/categories/" + categoryId + "/playlists");
            builder.Append ("?limit=" + limit);
            builder.Append ("&offset=" + offset);
            if (!string.IsNullOrEmpty (country))
                builder.Append ("&country=" + country);
            return DownloadData<CategoryPlaylist> (builder.ToString ());
        }
        /// <summary>
        ///     Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="fields">
        ///     Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are
        ///     returned.
        /// </param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object)</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        /// <remarks>AUTH NEEDED</remarks>
        public Paging<PlaylistTrack> GetPlaylistTracks (string playlistId, string fields = "", int limit = 100, int offset = 0, string market = "") {
            limit = Math.Min (limit, 100);
            StringBuilder builder = new StringBuilder (APIBase + "/playlists/" + playlistId + "/tracks");
            builder.Append ("?fields=" + fields);
            builder.Append ("&limit=" + limit);
            builder.Append ("&offset=" + offset);
            if (!string.IsNullOrEmpty (market))
                builder.Append ("&market=" + market);

            return DownloadData<Paging<PlaylistTrack>> (builder.ToString ());;
        }

        /// <summary>
        ///     Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <param name="artistSeed">A comma separated list of Spotify IDs for seed artists.
        /// Up to 5 seed values may be provided in any combination of seed_artists, seed_tracks and seed_genres.
        /// </param>
        /// <param name="genreSeed">A comma separated list of any genres in the set of available genre seeds.
        /// Up to 5 seed values may be provided in any combination of seed_artists, seed_tracks and seed_genres.
        /// </param>
        /// <param name="trackSeed">A comma separated list of Spotify IDs for a seed track.
        /// Up to 5 seed values may be provided in any combination of seed_artists, seed_tracks and seed_genres.
        /// </param>
        /// <param name="target">Tracks with the attribute values nearest to the target values will be preferred.</param>
        /// <param name="min">For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided</param>
        /// <param name="max">For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided</param>
        /// <param name="limit">The target size of the list of recommended tracks. Default: 20. Minimum: 1. Maximum: 100.
        /// For seeds with unusually small pools or when highly restrictive filtering is applied, it may be impossible to generate the requested number of recommended tracks.
        /// </param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.
        /// Because min_*, max_* and target_* are applied to pools before relinking, the generated results may not precisely match the filters applied.</param>
        /// <returns></returns>
        /// <remarks>AUTH NEEDED</remarks>
        public Recommendations GetRecommendations (List<string> artistSeed = null, List<string> genreSeed = null, List<string> trackSeed = null,
            TuneableTrack target = null, TuneableTrack min = null, TuneableTrack max = null, int limit = 20, string market = "") {
            limit = Math.Min (100, limit);
            StringBuilder builder = new StringBuilder ($"{APIBase}/recommendations");
            builder.Append ("?limit=" + limit);
            if (artistSeed?.Count > 0)
                builder.Append ("&seed_artists=" + string.Join (",", artistSeed));
            if (genreSeed?.Count > 0)
                builder.Append ("&seed_genres=" + string.Join (",", genreSeed));
            if (trackSeed?.Count > 0)
                builder.Append ("&seed_tracks=" + string.Join (",", trackSeed));
            if (target != null)
                builder.Append (target.BuildUrlParams ("target"));
            if (min != null)
                builder.Append (min.BuildUrlParams ("min"));
            if (max != null)
                builder.Append (max.BuildUrlParams ("max"));
            if (!string.IsNullOrEmpty (market))
                builder.Append ("&market=" + market);
            return DownloadData<Recommendations> (builder.ToString ());

        }

        public async Task<bool> Seed () {
            var rng = new Random ();
            var listaGeneros = new List<string> () { "pop", "mpb", "classical", "rock" };
            _ = await _trackRepository.DeleteAll();
            foreach (var genero in listaGeneros) {
                var recommendations = GetRecommendations (null, new List<string> () { genero }, null, null, null, null, 50, string.Empty);
                foreach (var item in recommendations.Tracks) {
                    var name = item.Name;
                    var artistName = item.Artists.First ().Name;
                    var genre = await _genreRepository.GetByNameAsync (genero);

                    var oTrack = new Track () {
                        Name = item.Name,
                        NameOfArtist = item.Artists.First ().Name,
                        IdGenre = genre.Id,
                        Price = rng.Next (1, 60)
                    };

                    try {
                        _ = _trackRepository.Create (oTrack);
                    } catch (System.Exception) {

                        continue ;
                    }
                }
            }

            return true;
        }

        #region Util
        private T DownloadData<T> (string url) where T : BasicModel {
            int triesLeft = RetryTimes + 1;
            Error lastError;

            Tuple<ResponseInfo, T> response = null;
            do {
                if (response != null) { Thread.Sleep (RetryAfter); }
                response = DownloadDataAlt<T> (url);

                response.Item2.AddResponseInfo (response.Item1);
                lastError = response.Item2.Error;

                triesLeft -= 1;

            } while (UseAutoRetry && triesLeft > 0 && lastError != null && RetryErrorCodes.Contains (lastError.Status));

            return response.Item2;
        }
        private Tuple<ResponseInfo, T> DownloadDataAlt<T> (string url) {
            Dictionary<string, string> headers = new Dictionary<string, string> ();
            GetAccessToken ();
            headers.Add ("Authorization", $"{_authentication.TokenType}  {_authentication.AccessToken}");
            return _webClient.DownloadJson<T> (url, headers);
        }
        private SpotifyAuthentication GetAccessToken () {

            var clientid = _authentication.ClientId;
            var clientsecret = _authentication.ClientSecret;

            //request to get the access token
            var encode_clientid_clientsecret = Convert.ToBase64String (Encoding.UTF8.GetBytes (string.Format ("{0}:{1}", clientid, clientsecret)));

            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create (_urlToken);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add ("Authorization: Basic " + encode_clientid_clientsecret);

            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes (request);
            webRequest.ContentLength = req_bytes.Length;

            Stream strm = webRequest.GetRequestStream ();
            strm.Write (req_bytes, 0, req_bytes.Length);
            strm.Close ();

            var accessToken = string.Empty;
            HttpWebResponse resp = (HttpWebResponse) webRequest.GetResponse ();
            using (Stream respStr = resp.GetResponseStream ()) {
                using (StreamReader rdr = new StreamReader (respStr, Encoding.UTF8)) {
                    //         should get back a string i can then turn to json and parse for accesstoken
                    dynamic json = JsonConvert.DeserializeObject<object> (rdr.ReadToEnd ());

                    rdr.Close ();

                    _authentication.AccessToken = json["access_token"];
                }
            }

            return _authentication;
        }

        #endregion
    }
}
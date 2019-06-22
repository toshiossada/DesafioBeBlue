using System.Collections.Generic;
using BeBlue.Desafio.Entities.Spotify;
using BeBlue.Desafio.Entities.Spotify.Interfaces;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service;
using Moq;
using Xunit;

namespace BeBlue.Desafio.Test.Service {
    public class SpotifyServiceTest {
        private readonly IClient _webClient;
        private readonly Mock<ITrackRepository> _trackRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly SpotifyService _spotifyService;
        private int limit = 20;
        private int offset = 0;

        public SpotifyServiceTest () {
            _webClient = new SpotifyWebClient ();
            _trackRepositoryMock = new Mock<ITrackRepository> ();
            _genreRepositoryMock = new Mock<IGenreRepository> ();
            _spotifyService = new SpotifyService (_webClient, _trackRepositoryMock.Object, _genreRepositoryMock.Object);
        }

        public static IEnumerable<object[]> lstCategories =>
            new List<object[]> {
                new object[] { "","" },
            };
        [Theory]
        [MemberData (nameof (lstCategories))]
        public void GetCategories (string country, string locale) {
            var result = _spotifyService.GetCategories (country, locale, limit, offset);
            Assert.NotNull(result);
            Assert.Equal(limit, result.Categories.Items.Count);
        }

        public static IEnumerable<object[]> lstRecommendations =>
            new List<object[]> {
                new object[] { null, new List<string>{"rock"} },
            };
        [Theory]
        [MemberData (nameof (lstRecommendations))]
        public void GetRecommendations (List<string> artistSeed, List<string> genreSeed){
            var result = _spotifyService.GetRecommendations (artistSeed, genreSeed);
            Assert.NotNull(result);
            Assert.Equal(limit, result.Tracks.Count);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service;
using Bogus;
using Moq;
using Xunit;

namespace BeBlue.Desafio.Test.Service {
    public class TrackServiceTest {
        private readonly Mock<ITrackRepository> _trackRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly TrackService _trackService;
        public TrackServiceTest () {
            var faker = new Faker ();
            _trackRepositoryMock = new Mock<ITrackRepository> ();
            _genreRepositoryMock = new Mock<IGenreRepository> ();

            _trackService = new TrackService (_trackRepositoryMock.Object, _genreRepositoryMock.Object);
        }

        [Fact]
        public async void GetAsync () {
            IEnumerable<Track> track = new List<Track> ();
            _trackRepositoryMock.Setup (r => r.GetAsync ()).Returns (Task.FromResult (track));
            var result = await _trackService.GetAsync ();

            Assert.NotNull (result);
            _trackRepositoryMock.Verify (x => x.GetAsync (), Times.Once ());
        }

        [Fact]
        public void Create () {
            var track = new Track ();
            _trackRepositoryMock.Setup (r => r.Create (track)).Returns (1);
            var result = _trackService.Create (track);

            Assert.NotEqual (0, result);
            _trackRepositoryMock.Verify (x => x.Create (track), Times.Once ());
        }

        public static IEnumerable<object[]> lstIds =>
            new List<object[]> {
                new object[] { 1 },
                new object[] { 2 },
                new object[] { 3 },
                new object[] { 4 },
            };

        [Theory]
        [MemberData (nameof (lstIds))]
        public async void GetByIdAsync (int id) {
            var track = new Track ();
            _trackRepositoryMock.Setup (r => r.GetByIdAsync (id)).Returns (Task.FromResult (track));
            var result = await _trackService.GetByIdAsync (id);

            Assert.NotNull (result);
            _trackRepositoryMock.Verify (x => x.GetByIdAsync (id), Times.Once ());
        }
        public static IEnumerable<object[]> lstIdsAndDayOfWeeks =>
            new List<object[]> {
                new object[] { 1, null },
                new object[] { 2, 4 },
                new object[] { 3, 6 },
                new object[] { 4, null },
            };
        [Theory]
        [MemberData (nameof (lstIdsAndDayOfWeeks))]
        public async void GetByIdAndDayOfWeekAsync (int id, int? dayOfWeek) {
            var track = new Track ();
            _trackRepositoryMock.Setup (r => r.GetByIdAndDayOfWeekAsync (id, dayOfWeek)).Returns (Task.FromResult (track));
            var result = await _trackService.GetByIdAndDayOfWeekAsync (id, dayOfWeek);

            Assert.NotNull (result);
            _trackRepositoryMock.Verify (x => x.GetByIdAndDayOfWeekAsync (id, dayOfWeek), Times.Once ());
        }
        public static IEnumerable<object[]> lstGenre =>
            new List<object[]> {
                new object[] { 1, 50, 1 },
                new object[] { 51, 10, 3 },
                new object[] { 18, 15, 4 },
                new object[] { 35, 20, 5 },
            };
        [Theory]
        [MemberData (nameof (lstGenre))]
        public async void GetByGenreAsync (string idGenre, int PageSize, int PageNumber) {
            IEnumerable<Track> track = new List<Track> ();
            _trackRepositoryMock.Setup (r => r.GetByGenreAsync (idGenre, PageSize, PageNumber)).Returns (Task.FromResult (track));
            var result = await _trackService.GetByGenreAsync (idGenre, PageSize, PageNumber);

            Assert.NotNull (result);
            _trackRepositoryMock.Verify (x => x.GetByGenreAsync (idGenre, PageSize, PageNumber), Times.Once ());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service.Interfaces;

namespace BeBlue.Desafio.lib.service {
    public class TrackService : ITrackService {
        private readonly ITrackRepository _trackRepository;
        private readonly IGenreRepository _genreRepository;

        public TrackService (ITrackRepository trackRepository, IGenreRepository genreRepository) {
            _trackRepository = trackRepository;
            _genreRepository = genreRepository;
        }

        public int Create (Track track) {
            return _trackRepository.Create (track);
        }

        public async Task<IEnumerable<Track>> GetAsync () {
            var result = await _trackRepository.GetAsync ();
            return result;
        }

        public async Task<Track> GetByIdAndDayOfWeekAsync (int id, int? dayOfWeek) {
            var result = await _trackRepository.GetByIdAndDayOfWeekAsync (id, dayOfWeek);
            return result;
        }

        public async Task<Track> GetByIdAsync (int id) {
            var result = await _trackRepository.GetByIdAsync (id);
            return result;
        }

        public async Task<IEnumerable<Track>> GetByGenreAsync (string name, int @PageSize = 50, int @PageNumber = 1) {
            var tracks = await _trackRepository.GetByGenreAsync (name, PageSize, PageNumber);
            return tracks;
        }
    }
}
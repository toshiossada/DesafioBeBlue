using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;

namespace BeBlue.Desafio.lib.service.Interfaces {
    public interface ITrackService {
        Task<IEnumerable<Track>> GetAsync ();
        int Create (Track track);
        Task<Track> GetByIdAsync (int id);
        Task<Track> GetByIdAndDayOfWeekAsync (int id, int? dayOfWeek);
        Task<IEnumerable<Track>> GetByGenreAsync (string idGenre, int @PageSize = 50, int @PageNumber = 1);
    }
}
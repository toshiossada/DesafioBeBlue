using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;

namespace BeBlue.Desafio.lib.repository.Interface {
    public interface ITrackRepository {
        Task<IEnumerable<Track>> GetAsync ();
        int Create (Track track);
        Task<Track> GetByIdAsync (int id);
        Task<Track> GetByIdAndDayOfWeekAsync (int id, int? dayOfWeek);
        Task<bool> Delete (int idTrack);
        Task<bool> DeleteAll ();
        Task<IEnumerable<Track>> GetByGenreAsync (string name, int @PageSize = 50, int @PageNumber = 1);
        Task<IEnumerable<Track>> GetBySaleAsync (int idSale);
    }
}
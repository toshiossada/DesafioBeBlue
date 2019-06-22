using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;

namespace BeBlue.Desafio.lib.repository.Interface
{
    public interface IGenreRepository
    {
         
        Task<IEnumerable<Genre>> GetAsync ();
        Task<Genre> GetByNameAsync (string name);
    }
}
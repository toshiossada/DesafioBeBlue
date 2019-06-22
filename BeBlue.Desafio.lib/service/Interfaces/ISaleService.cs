using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;

namespace BeBlue.Desafio.lib.service.Interfaces {
    public interface ISaleService {
        Task<IEnumerable<Sale>> GetAsync ();
        Sale Create (Sale sale);
        Task<bool> UpdateAsync (Sale sale);
        Task<Sale> GetAsync (int idSale);
        Task<IEnumerable<Sale>> GetByDateAsync (DateTime begin, DateTime end, int PageSize = 50, int PageNumber = 1);
    }
}
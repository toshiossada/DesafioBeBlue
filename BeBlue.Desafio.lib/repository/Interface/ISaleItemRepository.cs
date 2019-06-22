using System.Collections.Generic;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;

namespace BeBlue.Desafio.lib.repository.Interface
{
    public interface ISaleItemRepository
    {
         Task<IEnumerable<SaleItem>> GetBySaleAsync (int idSale);
         int Create (SaleItem saleItem);
    }
}
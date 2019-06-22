using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service.Interfaces;

namespace BeBlue.Desafio.lib.service {
    public class SaleService : ISaleService {
        private readonly ISaleRepository _saleRepository;
        public readonly ISaleItemRepository _saleItemRepository;
        private readonly ITrackRepository _trackRepository;

        public SaleService (ISaleRepository saleRepository, ISaleItemRepository saleItemRepository, ITrackRepository trackRepository) {
            _saleRepository = saleRepository;
            _saleItemRepository = saleItemRepository;
            _trackRepository = trackRepository;
        }
        public Sale Create (Sale sale) {

            var id = _saleRepository.Create (sale);
            sale.Id = id;
            var lstItens = new List<SaleItem> ();

            foreach (var item in sale.Items) {
                var itemSale = new SaleItem () {
                    IdTrack = item.Id,
                    IdSale = id,
                    Cashback = item.Price * (item.PercentCashback / 100),
                    IdDayOfWeek = item.IdDayOfWeek
                };

                var retorno = _saleItemRepository.Create (itemSale);
                itemSale.Id = retorno;
                lstItens.Add (itemSale);
            }
            sale.Total = sale.Items.Sum (r => r.Price);
            sale.TotalCashback = lstItens.Sum (r => r.Cashback);
            _ = UpdateAsync (sale);
            return sale;
        }

        public async Task<IEnumerable<Sale>> GetAsync () {
            var retorno = await _saleRepository.GetAsync ();
            foreach (var item in retorno) {
                item.Items = await _trackRepository.GetBySaleAsync (item.Id);
            }
            return retorno;
        }

        public async Task<Sale> GetAsync (int idSale) {
            var retorno = await _saleRepository.GetAsync (idSale);
            retorno.Items = await _trackRepository.GetBySaleAsync (retorno.Id);
            return retorno;
        }

        public async Task<IEnumerable<Sale>> GetByDateAsync (DateTime begin, DateTime end, int PageSize = 50, int PageNumber = 1) {
            var retorno = await _saleRepository.GetByDateAsync (begin, end, PageSize, PageNumber);
            foreach (var item in retorno) {
                item.Items = await _trackRepository.GetBySaleAsync (item.Id);
            }
            return retorno;
        }

        public async Task<bool> UpdateAsync (Sale sale) {
            try {
                _ = await _saleRepository.UpdateAsync (sale);
            } catch (System.Exception e) {

                throw e;
            }

            return true;
        }

    }
}
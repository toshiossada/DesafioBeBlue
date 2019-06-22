using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Dto;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service;
using Bogus;
using Moq;
using Xunit;

namespace BeBlue.Desafio.Test.Service {
    public class SaleServiceTest {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        public readonly Mock<ISaleItemRepository> _saleItemRepositoryMock;
        private readonly Mock<ITrackRepository> _trackRepositoryMock;
        private readonly Mock<TrackService> _trackServiceMock;

        private readonly SaleService _saleService;
        public SaleServiceTest () {
            _saleRepositoryMock = new Mock<ISaleRepository> ();
            _saleItemRepositoryMock = new Mock<ISaleItemRepository> ();
            _trackRepositoryMock = new Mock<ITrackRepository> ();

            _saleService = new SaleService (_saleRepositoryMock.Object, _saleItemRepositoryMock.Object, _trackRepositoryMock.Object);

        }

        [Fact]
        public async void GetAsync () {
            IEnumerable<Sale> sale = new List<Sale> () {
                new Sale () {
                Id = 1
                }
            };
            IEnumerable<SaleItem> saleItem = new List<SaleItem> ();

            _saleRepositoryMock.Setup (r => r.GetAsync ()).Returns (Task.FromResult (sale));
            _saleItemRepositoryMock.Setup (r => r.GetBySaleAsync (sale.First ().Id)).Returns (Task.FromResult (saleItem));
            var result = await _saleService.GetAsync ();

            Assert.NotNull (result);
            _saleRepositoryMock.Verify (x => x.GetAsync (), Times.Once ());
        }

        public static IEnumerable<object[]> lstSales =>
            new List<object[]> {
                new object[] {
                new Sale () {
                Id = 1,
                Items = new List<Track> () {
                new Track () {
                Id = 1
                }
                }
                }
                },
            };

        [Theory]
        [MemberData (nameof (lstSales))]
        public async void Create (Sale sale) {
            var itemSale = new SaleItem () {
                IdTrack = 1,
                IdSale = sale.Id,
                Cashback = 5,
                IdDayOfWeek = 0
            };

            _saleRepositoryMock.Setup (r => r.Create (sale)).Returns (1);
            _saleItemRepositoryMock.Setup (r => r.Create (itemSale)).Returns (1);
            var result = _saleService.Create (sale);

            Assert.NotNull (result);

            _saleRepositoryMock.Verify (x => x.Create (sale), Times.Once ());
        }

        [Theory]
        [MemberData (nameof (lstSales))]
        public async void UpdateAsync (Sale sale) {

            _saleRepositoryMock.Setup (r => r.UpdateAsync (sale)).Returns (Task.FromResult (true));
            var result = await _saleService.UpdateAsync (sale);

            Assert.Equal (true, result);

            _saleRepositoryMock.Verify (x => x.UpdateAsync (sale), Times.Once ());
        }


        public static IEnumerable<object[]> lstIdsSales =>
            new List<object[]> {
                new object[] { 1 },
                new object[] { 2 },
                new object[] { 3 },
                new object[] { 4 },
            };

        [Theory]
        [MemberData (nameof (lstIdsSales))]
        public async void GetByIdAsync (int id) {
            var sale = new Sale();
            IEnumerable<SaleItem> itemSale = new List<SaleItem> () ;
            _saleRepositoryMock.Setup (r => r.GetAsync (id)).Returns (Task.FromResult (sale));
            _saleItemRepositoryMock.Setup (r => r.GetBySaleAsync (id)).Returns (Task.FromResult (itemSale));
            var result = await _saleService.GetAsync (id);

            Assert.NotNull (result);

            _saleRepositoryMock.Verify (x => x.GetAsync (id), Times.Once ());
        }


        public static IEnumerable<object[]> lstData =>
            new List<object[]> {
                new object[] { DateTime.Now.AddDays(-80), DateTime.Now.AddDays(3), 50, 1 },
            };

        [Theory]
        [MemberData (nameof (lstData))]
        public async void GetByDateAsync (DateTime begin, DateTime end, int PageSize, int PageNumber) {
            IEnumerable<Sale> sale = new List<Sale>();
            IEnumerable<SaleItem> itemSale = new List<SaleItem> () ;
            _saleRepositoryMock.Setup (r => r.GetByDateAsync (begin, end, PageSize, PageNumber)).Returns (Task.FromResult (sale));
            _saleItemRepositoryMock.Setup (r => r.GetBySaleAsync (1)).Returns (Task.FromResult (itemSale));
            var result = await _saleService.GetByDateAsync (begin, end, PageSize, PageNumber);

            Assert.NotNull (result);

            _saleRepositoryMock.Verify (x => x.GetByDateAsync (begin, end, PageSize, PageNumber), Times.Once ());
        }

    }
}
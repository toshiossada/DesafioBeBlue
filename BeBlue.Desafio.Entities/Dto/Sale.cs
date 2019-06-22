using System.Collections.Generic;
using System;
namespace BeBlue.Desafio.Entities.Dto
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime DateSale { get; set; }
        public decimal Total { get; set; }
        public decimal TotalCashback { get; set; }
        public IEnumerable<Track> Items { get; set; }
    }
}
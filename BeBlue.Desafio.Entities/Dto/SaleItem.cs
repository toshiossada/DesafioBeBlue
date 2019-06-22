namespace BeBlue.Desafio.Entities.Dto
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int IdSale { get; set; }
        public int IdTrack { get; set; }
        public int IdDayOfWeek { get; set; }
        public decimal Cashback { get; set; }
    }
}
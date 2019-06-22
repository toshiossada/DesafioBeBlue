namespace BeBlue.Desafio.Entities.Dto {
    public class Track {
        public int Id { get; set; }
        public int IdGenre { get; set; }
        public int IdDayOfWeek { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string NameOfArtist { get; set; }
        public string GenreName { get; set; }
        public decimal PercentCashback { get; set; }
        public string DayOfWeekName { get; set; }
        public decimal CashBackValue { get { return Price * (PercentCashback / 100); } }
    }
}
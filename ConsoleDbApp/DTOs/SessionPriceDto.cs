namespace ConsoleDbApp.DTOs
{
    public class SessionPriceDto
    {
        public SessionPriceDto(decimal minPrice, decimal maxPrice, decimal averagePrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            AveragePrice = averagePrice;
        }

        public decimal MinPrice { get; }
        public decimal MaxPrice { get; }
        public decimal AveragePrice { get; }
    }
}


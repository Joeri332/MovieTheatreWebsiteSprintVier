using MovieTheatreDatabase;
using MovieTheatreModels.Enums;

namespace MovieTheatreModels.Dto
{
    public class PriceDto
    {
        public int PriceId { get; set; }
        public PriceCategory PriceType { get; set; }   
        public string Name { get; set; }
        public Decimal Amount { get; set; }
        public string PriceInfo { get; set; }
        public DateTime? PriceRetireDate { get; set; }
        

        public Price ToDb() =>
            new()
            {
                PriceId = PriceId,
                PriceType = PriceType,
                Name = Name,
                Amount = Amount,
                PriceInfo = PriceInfo,
                PriceRetireDate = PriceRetireDate,
                
            };
    }
}

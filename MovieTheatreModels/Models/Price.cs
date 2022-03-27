using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public class Price
    {
        [Key]
        [Display(Name = "PRICE")]
        public int PriceId { get; set; }
        public PriceCategory PriceType { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Amount { get; set; }
        public string PriceInfo { get; set; }
        public DateTime? PriceRetireDate { get; set; }
    }
}

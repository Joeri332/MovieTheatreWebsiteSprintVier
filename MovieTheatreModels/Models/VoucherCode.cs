using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class VoucherCode
    {
        [Key]
        public int VoucherCodeId { get; set; }
        public string Code { get; set; }

        public int PriceId { get; set; }
        public Price Price { get; set; }
    }
}

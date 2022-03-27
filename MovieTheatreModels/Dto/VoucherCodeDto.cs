using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class VoucherCodeDto
    {
        public int VoucherCodeId { get; set; }
        public string Code { get; set; }

        public VoucherCode ToDb() =>
            new()
            {
                VoucherCodeId = VoucherCodeId,
                Code = Code,
            };
    }
}

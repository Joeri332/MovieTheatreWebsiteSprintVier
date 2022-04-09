using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public int? UserId { get; set; }
        public int MovieTheatreRoomId { get; set; }
        public int? PriceIdSpecial { get; set; }
        public int PriceId  { get; set; }
        public int ChairCount { get; set; }

        public Reservation ToDb() =>
            new()
            {
                ReservationId = ReservationId,
                UserId = UserId,
                MovieTheatreRoomId = MovieTheatreRoomId,
                PriceIdSpecial = PriceIdSpecial,
                PriceId = PriceId,
                ReservationGuid = Guid.NewGuid(),
            };
    }
}
namespace MovieTheatreDatabase
{
    public class ReservationChairNr
    {
        public ReservationChairNr(int reservationId, int chairNr)
        {
            ReservationId = reservationId;
            ChairNr = chairNr;
        }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int ChairNr { get; set; }
    }
}
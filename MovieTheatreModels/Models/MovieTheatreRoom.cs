using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class MovieTheatreRoom
    {
        [Key]
        public int MovieTheatreRoomId { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int TheatreRoomId { get; set; }
        public TheatreRoom TheatreRoom { get; set; }
        public DateTime DateTime { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class MovieTheatreRoomDto
    {
        [Key]
        public int MovieTheatreRoomId { get; set; }
        public int MovieId { get; set; }
        public int TheatreRoomId { get; set; }


        public DateTime DateTime { get; set; }

        public MovieTheatreRoom ToDb() =>
            new()
            {
                MovieTheatreRoomId = MovieTheatreRoomId,
                MovieId = MovieId,
                TheatreRoomId = TheatreRoomId,
                DateTime = DateTime,
            };
    }
}

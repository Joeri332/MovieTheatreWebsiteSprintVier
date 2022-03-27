using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public class TheatreRoom
    {
        [Key]
        public int TheatreRoomId { get; set; }
        public string Name { get; set; }
        public TheatreRoomCategory TheatreRoomCategory { get; set; }
        public int ChairCount { get; set; }
        public string PartialViewName { get; set; }
        public List<MovieTheatreRoom> MovieTheatreRooms { get; set; }
    }
}

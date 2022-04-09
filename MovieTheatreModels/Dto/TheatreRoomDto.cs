using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class TheatreRoomDto
    {

        public int TheatreRoomId { get; set; }
        public string Name { get; set; }
        public int ChairCount { get; set; }

   
        public TheatreRoom ToDb() =>
            new()
            {
                TheatreRoomId = TheatreRoomId,
                Name = Name,
                ChairCount = ChairCount,

            };
    }
}
using System.ComponentModel.DataAnnotations;
using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string AgeRestriction { get; set; }
        public string Language { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }

        public TimeSpan Duration { get; set; }

        //Map a MovieDto object to a Movie Object
        public Movie ToDb() =>
            new()
            {
                MovieId = MovieId,
                Name = Name,
                ShortDescription = ShortDescription,
                LongDescription = LongDescription,
                ImageUrl = ImageUrl,
                Genre = Genre,
                AgeRestriction = AgeRestriction,
                Language = Language,
                Director = Director,
                Stars = Stars,
                Duration = Duration,
                
            };
    }
}
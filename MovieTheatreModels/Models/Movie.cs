using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Dto;

namespace MovieTheatreDatabase
{
    public class Movie
    {
        [Key] public int MovieId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        // [Required]
        public string AgeRestriction { get; set; }
        public string Language { get; set; }
        [Range(0.0, 10.0)]
        public double? MovieScore { get; set; }
        public TimeSpan Duration { get; set; }
        public List<MovieTheatreRoom> MovieTheatreRooms { get; set; }
        public string Director { get; set; }
        public string Stars { get; set; }


        public string GetAgeRestriction()
        {
            switch (this.AgeRestriction)
            {
                case "All":
                    return "/img/AgeIcons/ALL.webp";

                case "6":
                    return "/img/AgeIcons/SIX.png";

                case "9":
                    return "/img/AgeIcons/NINE.png";

                case "12":
                    return "/img/AgeIcons/TWELVE.png";

                case "14":
                    return "/img/AgeIcons/FOURTEEN.png";

                case "16":
                    return "/img/AgeIcons/SIXTEEN.svg";

                case "18":
                    return "/img/AgeIcons/EIGHTEEN.png";

                default: return this.AgeRestriction;
            }
        }
        //Map a MovieDto object to a Movie Object
        public MovieDto ToDto() =>
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

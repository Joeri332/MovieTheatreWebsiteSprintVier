using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class SurveyDto
    {
        public int SurveyId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiresOn { get; set; }

        //Map a MovieDto object to a Movie Object
        public Survey ToDb() =>
            new()
            {
                SurveyId = SurveyId,
                Description = Description,
                CreatedDate = CreatedDate,
                ExpiresOn = ExpiresOn,

            };
    }
}
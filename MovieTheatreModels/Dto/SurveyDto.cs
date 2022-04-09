using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class SurveyDto
    {
        public int SurveyId { get; set; }

        public string Name { get; set; } 
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiresOn { get; set; }

        public Survey ToDb() =>
            new()
            {
                SurveyId = SurveyId,
                Name = Name,
                Description = Description,
                CreatedDate = CreatedDate,
                ExpiresOn = ExpiresOn,

            };
    }
}
using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class Survey
    {
        [Key] public int SurveyId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiresOn { get; set; }
    } 
}

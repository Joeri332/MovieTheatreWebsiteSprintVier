using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class SurveyUser
    {
        [Key] public int SurveyUserId { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<SurveyUserAnswer> SurveyUserAnswers { get; set; }

    } 
}

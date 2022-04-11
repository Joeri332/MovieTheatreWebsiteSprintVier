using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class SurveyQuestion
    {
        [Key] public int SurveyQuestionId { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public List<SurveyUserAnswer> SurveyUserAnswers { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }

    } 
}

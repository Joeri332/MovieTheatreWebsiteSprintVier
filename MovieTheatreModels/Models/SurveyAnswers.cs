using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public class SurveyAnswers
    {
        [Key] public int SurveyAnswersId { get; set; }
        public int SurveyId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string name { get; set; }

    } 
}

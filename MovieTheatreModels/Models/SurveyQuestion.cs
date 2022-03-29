using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public class SurveyQuestion
    {
        [Key] public int SurveyQuestionId { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
        public QuestionOptionEnums QuestionOptionEnums { get; set; }

    } 
}

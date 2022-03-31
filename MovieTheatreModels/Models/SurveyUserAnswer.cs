using System.ComponentModel.DataAnnotations;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public class SurveyUserAnswer
    {
        [Key] public int SurveyUserAnswerId { get; set; }

        public int? SurveyUserId { get; set; }
        public SurveyUser SurveyUser { get; set; }
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public QuestionOptionEnums QuestionOptionEnums { get; set; }

    } 
}

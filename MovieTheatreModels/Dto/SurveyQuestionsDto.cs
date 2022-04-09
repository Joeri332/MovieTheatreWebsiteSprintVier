using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class SurveyQuestionDto
    {
        public int SurveyQuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }


        public SurveyQuestion ToDb() =>
            new()
            {
                SurveyQuestionId = SurveyQuestionId,
                SurveyId = SurveyId,
                Text = Text,
                QuestionType = QuestionType,

            };
    }
}
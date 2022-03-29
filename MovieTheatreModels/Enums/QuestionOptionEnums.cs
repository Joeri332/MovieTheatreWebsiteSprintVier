using System.ComponentModel.DataAnnotations;

namespace MovieTheatreModels.Enums
{
    public enum QuestionOptionEnums
    {
        Bad,
        [Display(Name = "Below average")]
        BelowAverage,
        Average,
        [Display(Name = "Above average")]
        AboveAverage,
        Perfect,

    }
}

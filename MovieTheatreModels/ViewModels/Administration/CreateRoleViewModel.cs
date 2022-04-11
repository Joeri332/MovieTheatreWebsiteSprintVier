using System.ComponentModel.DataAnnotations;

namespace MovieTheatreModels.ViewModels.Administration
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace MovieTheatreModels.Models
{
    public class Contact
    {
        [Key]
        [Display(Name = "CONTACT")]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}

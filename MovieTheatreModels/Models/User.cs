using System.ComponentModel.DataAnnotations;

namespace MovieTheatreDatabase
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}

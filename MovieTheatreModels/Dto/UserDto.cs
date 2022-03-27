using MovieTheatreDatabase;

namespace MovieTheatreModels.Dto
{
    public class UserDto
    {

        public int UserId { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }

        //Map a MovieDto object to a Movie Object
        public User ToDb() =>
            new()
            {
                UserId = UserId,
                Username = Username,
                Password = Password,
                Email = Email,

            };
    }
}
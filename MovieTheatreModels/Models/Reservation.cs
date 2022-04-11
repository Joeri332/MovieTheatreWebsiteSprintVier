using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieTheatreDatabase
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public Guid ReservationGuid { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int MovieTheatreRoomId { get; set; }
        public MovieTheatreRoom MovieTheatreRoom { get; set; }
        public int? PriceIdSpecial { get; set; }
        public int PriceId { get; set; }
        public Price Prices { get; set; }
        public List<ReservationChairNr> ReservationChairNr { get; set; }

    }
}
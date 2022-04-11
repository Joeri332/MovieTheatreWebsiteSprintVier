using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public sealed class MovieTheatreTestDatabaseContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MovieTheatreTestDatabaseContext(DbContextOptions<MovieTheatreTestDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTheatreRoom> MovieTheatreRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TheatreRoom> TheatreRooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<VoucherCode> VoucherCodes { get; set; }
        public DbSet<ReservationChairNr> ReservationChairNr { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        public DbSet<SurveyUser> SurveyUser { get; set; }
        public DbSet<SurveyUserAnswer> SurveyUserAnswers { get; set; }
    }
}

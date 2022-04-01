using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheatreModels.Enums;

namespace MovieTheatreDatabase
{
    public sealed class MovieTheatreDatabaseContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public MovieTheatreDatabaseContext(DbContextOptions<MovieTheatreDatabaseContext> options) : base(options)
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




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationChairNr>()
                .HasKey(x => new { x.ReservationId, x.ChairNr });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    MovieId = 1,
                    Name = "Tyson's Run",
                    ShortDescription = "Tyson's does run things",
                    LongDescription =
                        "When 15-year-old Tyson attends public school for the first time, his life is changed forever. While helping his father clean up after the football team, Tyson befriends champion marathon runner Aklilu. Never letting his autism hold him back, Tyson becomes determined to run his first marathon in hopes of winning his father's approval.",
                    ImageUrl = "/img/tysons_run.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Drama",
                    AgeRestriction = "9",
                    Director = "Kim Bass",
                    Stars = "Major Dodson, Amy Smart, Rory Cochrane",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 2,
                    Name = "Against the Ice",
                    ShortDescription = "Against the Ice does ice things",
                    LongDescription =
                        "In 1909, Denmark’s Arctic Expedition led by Captain Ejnar Mikkelsen (Nikolaj Coster-Waldau) was attempting to disprove the United States’ claim to Northeast Greenland. This claim was based on the assumption that Greenland was broken up into two different pieces of land. Leaving his crew behind with the ship, Mikkelsen embarks on a journey across the ice with his inexperienced crew member, Iver Iversen (Joe Cole). The two men succeed in finding the proof that Greenland is one island, but returning to the ship takes longer and is much harder than expected. Battling extreme hunger, fatigue and a polar bear attack, they finally arrive to find their ship crushed in the ice and the camp abandoned. Hoping to be rescued, they now must fight to stay alive. As the days grow longer, their mental hold on reality starts to fade, breeding mistrust and paranoia, a dangerous cocktail in their fight for survival.",
                    ImageUrl = "/img/against_the_ice.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Actie",
                    AgeRestriction = "12",
                    Director = "Peter Flinth",
                    Stars = "Nikolaj Coster-Waldau, Joe Cole, Heida Reed",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 3,
                    Name = "The Weekend Away",
                    ShortDescription = "The Weekend Away does away things",
                    LongDescription =
                        "A weekend getaway to Croatia goes awry when a woman (Leighton Meester) is accused of killing her best friend (Christina Wolfe) and her efforts to get to the truth uncover a painful secret.",
                    ImageUrl = "/img/the_weekend_away.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Crime",
                    AgeRestriction = "14",
                    Director = "Kim Farrant",
                    Stars = "Leighton Meester, Christina Wolfe, Ziad Barki",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 4,
                    Name = "Fresh",
                    ShortDescription = "Fresh does Fresh things with a hand",
                    LongDescription =
                        "Follows Noa (Daisy Edgar-Jones), who meets the alluring Steve (Sebastian Stan) at a grocery store and – given her frustration with dating apps – takes a chance and gives him her number. After their first date, Noa is smitten and accepts Steve’s invitation to a romantic weekend getaway. Only to find that her new paramour has been hiding some unusual appetites.",
                    ImageUrl = "/img/fresh.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Comedy",
                    AgeRestriction = "12",
                    Director = "Mimi Cave",
                    Stars = "Daisy Edgar-Jones, Sebastian Stan, Jojo T. Gibbs",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 5,
                    Name = "The Desperate Hour",
                    ShortDescription = "The Weekend does Away things",
                    LongDescription =
                        "Recently widowed mother Amy Carr (Academy Award®-nominee Naomi Watts) is doing her best to restore normalcy to the lives of her young daughter and teenage son in their small town. As she's on a jog in the woods, she finds her town thrown into chaos as a shooting takes place at her son's school. Miles away, on foot in the dense forest, Amy desperately races against time to save her son.",
                    ImageUrl = "/img/the_desperate_hour.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Thriller",
                    AgeRestriction = "12",
                    Director = "Phillip Noyce",
                    Stars = "Naomi Watts, Colton Gobbo, Andrew Chown",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 6,
                    Name = "Blacklight",
                    ShortDescription = "Blacklight does Blacklight things",
                    LongDescription =
                        "In the tense action thriller BLACKLIGHT, LIAM NEESON stars as Travis Block, a freelance government operative living on the fringes and coming to terms with his shadowy past. When he discovers an undercover team that’s targeting U.S. citizens, Block finds himself in the crosshairs of the FBI director (AIDAN QUINN) he once helped protect. But as Block attempts redemption by enlisting a journalist (EMMY RAVER-LAMPMAN) to get the truth out, his daughter and granddaughter are threatened — and a danger that has existed on the margins.",
                    ImageUrl = "/img/blacklight.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Thriller",
                    AgeRestriction = "12",
                    Director = "Mark Williams",
                    Stars = "Liam Neeson, Adidan Quinn, Taylor John Smith",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 7,
                    Name = "Uncharted ",
                    ShortDescription = "Uncharted does Uncharted things",
                    LongDescription =
                        "Based on the prequel storyline from the Naughty Dog video game for PlayStation, the story is focused on the young thief Drake, and his first encounter with the professional rogue, Sullivan.",
                    ImageUrl = "/img/uncharted.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Actie",
                    AgeRestriction = "16",
                    Director = "Ruben Fleischer",
                    Stars = "Tom Holland, Mark Wahlberg, Antonio Banderas",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 8,
                    Name = "Moonfall",
                    ShortDescription = "Moonfall does Moonfall things",
                    LongDescription =
                        "In Moonfall, a mysterious force knocks the Moon from its orbit around Earth and sends it hurtling on a collision course with life as we know it. With mere weeks before impact and the world on the brink of annihilation, NASA executive and former astronaut Jo Fowler (Academy Award® winner Halle Berry) is convinced she has the key to saving us all – but only one astronaut from her past, Brian Harper (Patrick Wilson, “Midway”) and a conspiracy theorist K.C. Houseman (John Bradley, “Game of Thrones”) believes her. These unlikely heroes will mount an impossible last-ditch mission into space, leaving behind everyone they love, only to find out that our Moon is not what we think it is.",
                    ImageUrl = "/img/moonfall.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Science Fiction",
                    AgeRestriction = "12",
                    Director = "Roland Emmerich",
                    Stars = "Halle Berry, Patrick Wilson, John Bredley",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 9,
                    Name = "After the Pandemic",
                    ShortDescription = "Pandemic does the Pandemic things",
                    LongDescription =
                        "A dark sci-fi thriller set around the Covid-19 pandemic, the film is set in a post-apocalyptic world where a global airborne pandemic has wiped out 90% of the Earth's population and only the young and immune have endured as scavengers. For Ellie and Quinn, the daily challenges to stay alive are compounded when they become hunted by the merciless Stalkers. Their perilous journey culminates with a sacrifice, death, and redemption.",
                    ImageUrl = "/img/after_the_pandemic.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Science Fiction",
                    AgeRestriction = "16",
                    Director = "Richard Lowry",
                    Stars = "Eve James, Kannon Smith, Lelyn Mac",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 10,
                    Name = "No Exit",
                    ShortDescription = "No Exit does No Exit things",
                    LongDescription =
                        "In No Exit, Havana Rose Liu (“Mayday”) makes her feature film leading role debut as Darby, a young woman en route to a family emergency who is stranded by a blizzard and forced to find shelter at a highway rest area with a group of strangers. When she stumbles across an abducted girl in a van in the parking lot, it sets her on a terrifying life-or-death struggle to discover who among them is the kidnapper.",
                    ImageUrl = "/img/no_exit.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Thriller",
                    AgeRestriction = "16",
                    Director = "Damien Power",
                    Stars = "Havana Rose Liu, Danny Ramirez, David Rysdahl",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 11,
                    Name = "The Ledge",
                    ShortDescription = "The Ledge does Ledge things",
                    LongDescription =
                        "A rock climbing adventure between two friends turns into a terrifying nightmare. After Kelly (Brittany Ashworth) captures the murder of her best friend on camera, she becomes the next target of a tight-knit group of friends who will stop at nothing to destroy the evidence and anyone in their way. Desperate for her safety, she begins a treacherous climb up a mountain cliff and her survival instincts are put to the test when she becomes trapped with the killers just 20 feet away.",
                    ImageUrl = "/img/the_ledge.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Thriller",
                    AgeRestriction = "12",
                    Director = "Howard J. Ford",
                    Stars = "Brittany Ashworth, Ben Lamb, Nathan Welsh",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 12,
                    Name = "The Matrix Resurrections",
                    ShortDescription = "Matrix does The Matrix things",
                    LongDescription =
                        "The long-awaited fourth film in the groundbreaking franchise that redefined a genre. The new film reunites original stars Keanu Reeves and Carrie-Anne Moss in the iconic roles they made famous, Neo and Trinity.",
                    ImageUrl = "/img/matrix.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Science Fiction",
                    AgeRestriction = "12",
                    Director = "Lana Wachowski",
                    Stars = "Keanu Reeves, Carrie-Anne Moss, Yahya Abdul-Mateen II",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 13,
                    Name = "Spider-Man: No Way Home",
                    ShortDescription = "Spider-Man does spider things",
                    LongDescription =
                        "For the first time in the cinematic history of Spider-Man, our friendly neighborhood hero is unmasked and no longer able to separate his normal life from the high-stakes of being a Super Hero. When he asks for help from Doctor Strange the stakes become even more dangerous, forcing him to discover what it truly means to be Spider-Man.",
                    ImageUrl = "/img/spiderman.jpg",
                    Duration = new TimeSpan(2, 15, 30),
                    Genre = "Actie",
                    AgeRestriction = "12",
                    Director = "Jon Watts",
                    Stars = "Tom Holland, Zendaya, Benedict Cumberbatch",
                    Language = "Engels"
                },
                new Movie()
                {
                    MovieId = 14,
                    Name = "The Batman",
                    ShortDescription = "The Batman does Batman things",
                    LongDescription =
                        "From Warner Bros. Pictures comes The Batman, with director Matt Reeves (the “Planet of the Apes” films) at the helm and with Robert Pattinson (“Tenet,” “The Lighthouse,” “Good Time”) starring as Gotham City’s vigilante detective, Batman, and billionaire Bruce Wayne.",
                    ImageUrl = "/img/the_batman.jpg",
                    Duration = new TimeSpan(1, 15, 30),
                    Genre = "Actie",
                    AgeRestriction = "12",
                    Director = "Matt Reeves",
                    Stars = "Robert Pattinson, Zoë Kravitz, Jeffrey Wright",
                    Language = "Engels"
                });

            // TODO maybe translation table for less data pollution, otherwise this is fine.
            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, Username = "Sjaak", Email = "Sjaak123@hotmail.com" }
            );
            //Regular Short data:
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 1, PriceType = PriceCategory.RegularShort, Name = "Normal Price", Amount = 8.50M, PriceInfo = "Regular price of a movie <120 minutes " }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 2, PriceType = PriceCategory.RegularShort, Name = "Kids Discount", Amount = 7.00M, PriceInfo = "Discount for children up to and including 11 years, Discount on performances up to 6 p.m. that are in Dutch. " }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 3, PriceType = PriceCategory.RegularShort, Name = "Student Discount", Amount = 7.00M, PriceInfo = "On presentation of a student cart from Monday to Thursday" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 4, PriceType = PriceCategory.RegularShort, Name = "Reduction 65+", Amount = 7.00M, PriceInfo = "On shows of the 65+ pass. The discount is valid from Monday to Thursday, excluding holidays and/or public holidays. " }
            );

            //Regular Long data
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 5, PriceType = PriceCategory.RegularLong, Name = "Normal Price", Amount = 9.00M, PriceInfo = "Regular price of a film >120minutes " }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 6, PriceType = PriceCategory.RegularLong, Name = "Kids Discount", Amount = 7.50M, PriceInfo = "Discount for children up to and including 11 years, Discount on performances up to 6 p.m. that are in Dutch. " }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 7, PriceType = PriceCategory.RegularLong, Name = "Student Discount", Amount = 7.50M, PriceInfo = "On presentation of a student cart from Monday to Thursday" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 8, PriceType = PriceCategory.RegularLong, Name = "Reduction 65+", Amount = 7.50M, PriceInfo = "On shows of the 65+ pass. The discount is valid from Monday to Thursday, excluding holidays and/or public holidays. " }
            );
            

            //Movie3D Long data
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 9, PriceType = PriceCategory.Movie3DLong, Name = "Normal 3DMovie", Amount = 11.50M, PriceInfo = "Surcharge for 3D movies" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 10, PriceType = PriceCategory.Movie3DLong, Name = "Kids Discount 3D Movie", Amount = 10.00M, PriceInfo = "Surcharge for 3D movies" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 11, PriceType = PriceCategory.Movie3DLong, Name = "Student Discount 3D Movie", Amount = 10.00M, PriceInfo = "Surcharge for 3D movies" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 12, PriceType = PriceCategory.Movie3DLong, Name = "Reduction 65+ 3D Movie", Amount = 10.00M, PriceInfo = "Surcharge for 3D movies" }
            );            

            //Movie3D Short data
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 13, PriceType = PriceCategory.Movie3DShort, Name = "Normal 3DMovie", Amount = 11.00M, PriceInfo = "Regular price of a film >120minutes " }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 14, PriceType = PriceCategory.Movie3DShort, Name = "Kids Discount 3D Movie", Amount = 9.50M, PriceInfo = "Surcharge for 3D movies" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 15, PriceType = PriceCategory.Movie3DShort, Name = "Student Discount 3D Movie", Amount = 9.50M, PriceInfo = "Surcharge for 3D movies" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 16, PriceType = PriceCategory.Movie3DShort, Name = "Reduction 65+ 3D Movie", Amount = 9.50M, PriceInfo = "Surcharge for 3D movies" }
            );         

            //Code data
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 17, PriceType = PriceCategory.Code, Name = "National Cinema Voucher", Amount = 0.00M, PriceInfo = "MaDiWoDo tickets can be exchanged at the box office from Monday to Thursday. They are not valid on holidays and/or public holidays. " }
            );

            //SpecialArrangements
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 35, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 1.00 - Soft Drink Special (Medium)", Amount = 1.00M, PriceInfo = "Soft Drink Special" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 36, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 1.00 - Popcorn Special (Medium)", Amount = 1.00M, PriceInfo = "Popcorn Special (salt or sweet)" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 37, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 2.00 - Soft Drink & Popcorn Special (Medium)", Amount = 2.00M, PriceInfo = "Popcorn Special (salt or sweet)" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 38, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 1.50 - Soft Drink Special (Large)", Amount = 1.00M, PriceInfo = "Soft Drink Special" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 39, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 1.50 - Popcorn Special (Large)", Amount = 1.00M, PriceInfo = "Popcorn Special (salt or sweet)" }
            );
            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 40, PriceType = PriceCategory.SpecialArrangements, Name = "+ € 3.00 - Soft Drink & Popcorn Special (Large)", Amount = 2.00M, PriceInfo = "Popcorn Special (salt or sweet)" }
            );

            //Rooms 1 to 3 each have 8 rows of 15 seats
            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 1, Name = "One", ChairCount = 120, PartialViewName = "_PartialViewTheatreRoomOne", TheatreRoomCategory = TheatreRoomCategory.ThreeD }
            );

            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 2, Name = "Two", ChairCount = 120, PartialViewName = "_PartialViewTheatreRoomTwo", TheatreRoomCategory = TheatreRoomCategory.ThreeD }
            );

            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 3, Name = "Three", ChairCount = 120, PartialViewName = "_PartialViewTheatreRoomThree", TheatreRoomCategory = TheatreRoomCategory.Regular }
            );
            //4 is a small(er) room and has 60 seats in 6 rows of 10.
            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 4, Name = "Four", ChairCount = 60, PartialViewName = "_PartialViewTheatreRoomFour", TheatreRoomCategory = TheatreRoomCategory.Regular }
            );
            //5 and 6 are the small halls with both 50 seats, in the front 2 times 10 seats, in the back 2 times 15 seats.
            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 5, Name = "Five", ChairCount = 50, PartialViewName = "_PartialViewTheatreRoomFive", TheatreRoomCategory = TheatreRoomCategory.Regular }
            );

            modelBuilder.Entity<TheatreRoom>().HasData(
                new TheatreRoom() { TheatreRoomId = 6, Name = "Six", ChairCount = 50, PartialViewName = "_PartialViewTheatreRoomSix", TheatreRoomCategory = TheatreRoomCategory.Regular }
            );
            var datetime = DateTime.Today.AddDays(6);

            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 1, MovieId = 1, TheatreRoomId = 1, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 2, MovieId = 2, TheatreRoomId = 2, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 3, MovieId = 3, TheatreRoomId = 3, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 4, MovieId = 4, TheatreRoomId = 4, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 5, MovieId = 5, TheatreRoomId = 5, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 6, MovieId = 6, TheatreRoomId = 6, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 7, MovieId = 7, TheatreRoomId = 1, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 8, MovieId = 8, TheatreRoomId = 2, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 9, MovieId = 9, TheatreRoomId = 3, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 10, MovieId = 10, TheatreRoomId = 4, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 11, MovieId = 11, TheatreRoomId = 5, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 12, MovieId = 12, TheatreRoomId = 6, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 13, MovieId = 13, TheatreRoomId = 1, DateTime = datetime }
            );
            modelBuilder.Entity<MovieTheatreRoom>().HasData(
                new MovieTheatreRoom() { MovieTheatreRoomId = 14, MovieId = 14, TheatreRoomId = 2, DateTime = datetime }
            );

            modelBuilder.Entity<Survey>().HasData(
                new Survey() { SurveyId = 1, Name = "MovieTheatre Survey",  Description = "Experience Survey", CreatedDate = datetime}
            );

            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion() {SurveyQuestionId = 1, SurveyId = 1, Text = "How was the quality of the movie?", QuestionType = "Experience" }
            );

            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion() { SurveyQuestionId = 2, SurveyId = 1, Text = "How  were the seatings?", QuestionType = "Experience" }
            );
            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion() { SurveyQuestionId = 3, SurveyId = 1, Text = "Was the movietheatre clean?", QuestionType = "Experience" }
            );
            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion() { SurveyQuestionId = 4, SurveyId = 1, Text = "How was the sound?", QuestionType = "Experience" }
            );
            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion() { SurveyQuestionId = 5, SurveyId = 1, Text = "What was your overall experience?", QuestionType = "Experience" }
            );
  


            modelBuilder.Entity<ReservationChairNr>().HasData(
                new ReservationChairNr(1, 1),
                new ReservationChairNr(1, 2),
                new ReservationChairNr(1, 3),
                new ReservationChairNr(1, 4)
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation() { ReservationId = 1, UserId = 1, MovieTheatreRoomId = 1, PriceId = 1, ReservationGuid = Guid.NewGuid(), }
            );

            modelBuilder.Entity<VoucherCode>().HasData(
                new VoucherCode()
                {
                    VoucherCodeId = 1,
                    Code = "free",
                    PriceId = 17
                });
        }
    }
}

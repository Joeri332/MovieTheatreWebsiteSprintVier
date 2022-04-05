using System;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTheatreModels.Dto;
using MovieTheatreWebsite.Controllers;
using Xunit;

namespace MovieTheatreWebsite.Tests
{
    public abstract class TestMovie
    {
        private readonly MovieTheatreDatabaseContext _context;

        public TestMovie(MovieTheatreDatabaseContext context)
        {
            _context = context;
            Seed();
        }

        private void Seed()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var one = new Movie()
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

            };

            var two = new Movie()
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
            };

            var three = new Movie()
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
            };
            _context.AddRange(one, two, three);
            _context.SaveChanges();

        }

        [Fact]
        public async Task Test_Create_POST_InvalidModelState()
        {
            // Arrange
            var movie = new MovieDto()
            {
                MovieId = 3,
                Name = "The Weekend Away",
                ShortDescription = "The Weekend Away does away things",
                LongDescription =
                    "A weekend getaway to Croatia goes awry when a woman (Leighton Meester) is accused of killing her best friend (Christina Wolfe) and her efforts to get to the truth uncover a painful secret.",
                ImageUrl = "/img/the_weekend_away.jpg",
                Duration = new TimeSpan(2, 15, 30),
                Director = "Kim Farrant",
                Stars = "Leighton Meester, Christina Wolfe, Ziad Barki",

            };
            var controller = new MoviesController(_context);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await controller.Create(movie);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);

        }
    }
}


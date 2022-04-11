using System;
using System.Collections.Generic;
using System.Drawing;
using MovieTheatreDatabase;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTheatreModels.Dto;
using MovieTheatreWebsite.Controllers;
using NSubstitute;
using Xunit;



namespace MovieTheatreWebsite.Tests
{
    public class MovieControllerTest
    {
        public MovieControllerTest()
        {
            ContextOptions = new DbContextOptionsBuilder<MovieTheatreDatabaseContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .Options;
            _context = new MovieTheatreDatabaseContext(ContextOptions);
            Seed();
        }

        protected DbContextOptions<MovieTheatreDatabaseContext> ContextOptions { get; }
        private MovieTheatreDatabaseContext _context { get; set; }
        private void Seed()
        {
            _context.Database.EnsureDeleted();

            var one = new Movie()
            { 
                Name = "Test One",
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

            var two = new Movie()
            {
                Name = "Test Two",
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

            var three = new Movie()
            {
                Name = "Test Three",
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
            _context.ChangeTracker.Clear();
        }



        [Fact]
        public void Test_Create_GET_ReturnsViewResultNullModel()
        {
            // Arrange
            var controller = new MoviesController(_context);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Test_Update_POST_ReturnsViewResult_ValidModelState()
        {
            // Arrange
            var movie = new MovieDto()
            {
                MovieId = 2,
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
            var controller = new MoviesController(_context);

            // Act
            var result = await controller.Edit(2, movie);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Test_MovieControllerPostTask()
        {
            // Arrange
            var controller = new MoviesController(_context);
            var movie = new MovieDto()
            {
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
            var result = await controller.Create(movie);

            Assert.NotNull(_context.Movies.FirstOrDefault(x => x.Name == movie.Name));
        }

        [Fact]
        public async Task Test_Create_POST_ValidModelState()
        {
            // Arrange
            var r = new MovieDto()
            {
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

            var controller = new MoviesController(_context);

            // Act
            var result = await controller.Create(r);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Test_Index_GET_ReturnsViewResult_WithAListOfMovies()
        {
            // Arrange
            var controller = new MoviesController(_context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Test_Create_POST_InvalidModelState()
        {
            // Arrange
            var movie = new MovieDto()
            {
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
            var model = Assert.IsAssignableFrom<MovieDto>(viewResult.ViewData.Model);
            Assert.Null(model.Name);
        }

        [Fact]
        public async Task Test_Update_POST_ReturnsViewResult_InValidModelState()
        {
            {
                // Arrange

                var movie = new MovieDto()
                {
                    MovieId = 2,
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
                var result = await controller.Edit(2, movie);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<MovieDto>(viewResult.ViewData.Model);
                Assert.Equal(movie.Director, model.Director);
            }
        }

        [Fact]
        public async Task Test_Delete_POST_ReturnsViewResult_InValidModelState()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                // Arrange
                int testId = 1;

                var controller = new MoviesController(_context);

                // Act
                var result = await controller.DeleteConfirmed(testId);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Null(redirectToActionResult.ControllerName);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }
    }
}

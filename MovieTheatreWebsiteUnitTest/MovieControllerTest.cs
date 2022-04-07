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
    public abstract class MovieControllerTest
    {
        #region Seeding
       

        public MovieControllerTest(DbContextOptions<MovieTheatreDatabaseContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<MovieTheatreDatabaseContext> ContextOptions { get; }
        private void Seed()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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
                context.AddRange(one, two, three);
                context.SaveChanges();
            }
        }
        #endregion



        [Fact]
        public void Test_Create_GET_ReturnsViewResultNullModel()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                // Arrange
                var controller = new MoviesController(context);

                // Act
                var result = controller.Create();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Null(viewResult.ViewData.Model);
            }
        }

        [Fact]
        public async Task Test_Update_POST_ReturnsViewResult_ValidModelState()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                // Arrange
                
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
                var controller = new MoviesController(context);

                // Act
                var result = await controller.Create(movie);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Movie>(viewResult.ViewData.Model);
                Assert.Equal(movie.Name, model.Name);
                Assert.Equal(movie.ShortDescription, model.ShortDescription);
            }
        }

        [Fact]
        public async Task Test_MovieControllerPostTask()
        {
            // Arrange
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                var controller = new MoviesController(context);
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

                Assert.NotNull(context.Movies.FirstOrDefault(x => x.Name == movie.Name));
            }
        }

        [Fact]
        public async Task Test_Create_POST_ValidModelState()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
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

                var controller = new MoviesController(context);

                // Act
                var result = await controller.Create(r);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Null(redirectToActionResult.ControllerName);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }

        [Fact]
        public async void Test_Index_GET_ReturnsViewResult_WithAListOfMovies()
        {
            using (var context = new MovieTheatreDatabaseContext(ContextOptions))
            {
                // Arrange
                var controller = new MoviesController(context);

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(viewResult.ViewData.Model);
                Assert.Equal(17, model.Count());
            }
        }
    }
}

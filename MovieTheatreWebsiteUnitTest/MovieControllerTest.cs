using System;
using System.Drawing;
using MovieTheatreDatabase;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheatreModels.Dto;
using MovieTheatreWebsite.Controllers;
using NSubstitute;
using Xunit;

namespace MovieTheatreWebsite.Tests
{
    public abstract class MovieControllerTest
    {
        protected DbContextOptions<MovieTheatreDatabaseContext> ContextOptions { get; }
        public MovieControllerTest(DbContextOptions<MovieTheatreDatabaseContext> contextOptions)
        {
            ContextOptions = contextOptions;
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
                    Name = "Tyson's Run", ShortDescription = "Tyson's does run things",
                    LongDescription =
                        "When 15-year-old Tyson attends public school for the first time, his life is changed forever. While helping his father clean up after the football team, Tyson befriends champion marathon runner Aklilu. Never letting his autism hold him back, Tyson becomes determined to run his first marathon in hopes of winning his father's approval.",
                    ImageUrl = "/img/tysons_run.jpg", Duration = new TimeSpan(2, 15, 30),
                    Director = "Kim Bass",
                    Stars = "Major Dodson, Amy Smart, Rory Cochrane"
                };
                var result = await controller.Create(movie);
                /*new Movie()
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
                }*/
                Assert.NotNull(context.Movies.FirstOrDefault(x => x.Name == movie.Name));
            }
        }
    }
}
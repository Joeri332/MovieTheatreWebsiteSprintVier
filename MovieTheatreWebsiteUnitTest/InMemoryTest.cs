using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using MovieTheatreWebsite.Tests;

namespace MovieTheatreWebsiteUnitTest
{
    public class InMemoryTest : MovieControllerTest
    {
        public InMemoryTest()
            : base(
                new DbContextOptionsBuilder<MovieTheatreDatabaseContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}

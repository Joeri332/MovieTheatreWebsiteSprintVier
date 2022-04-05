#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;

namespace MovieTheatreWebsite.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class MovieTheatreRoomsController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public MovieTheatreRoomsController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: MovieTheatreRooms
        public async Task<IActionResult> Index()
        {
            var movieTheatreRooms = await _context.MovieTheatreRooms
                .Include(x => x.TheatreRoom)
                .Include(x => x.Movie)
                .ToListAsync();

            return View(movieTheatreRooms);
        }

        // GET: MovieTheatreRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheatreRoom = await _context.MovieTheatreRooms
                    .Include(x => x.TheatreRoom)
                    .Include(x => x.Movie)
                    .FirstOrDefaultAsync(m => m.MovieTheatreRoomId == id);

            if (movieTheatreRoom == null)
            {
                return NotFound();
            }

            return View(movieTheatreRoom);
        }

        // GET: MovieTheatreRooms/Create
        public IActionResult Create()
        {
            ViewData["MoviesNameselect"] = new SelectList(_context.Movies, "MovieId", "Name");
            ViewData["TheatreRoomNameSelect"] = new SelectList(_context.TheatreRooms, "TheatreRoomId", "Name");
            return View();
        }

        // POST: MovieTheatreRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieTheatreRoomId,MovieId,TheatreRoomId,DateTime")] MovieTheatreRoomDto movieTheatreRoom)
        {
            if (ModelState.IsValid)
            {
                _context.MovieTheatreRooms.Add(movieTheatreRoom.ToDb());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieTheatreRoom);
        }

        // GET: MovieTheatreRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheatreRoom = await _context.MovieTheatreRooms.FindAsync(id);
            if (movieTheatreRoom == null)
            {
                return NotFound();
            }
           
            ViewData["MoviesNameselect"] = new SelectList(_context.Movies, "MovieId", "Name");
            ViewData["TheatreRoomNameSelect"] = new SelectList(_context.TheatreRooms, "TheatreRoomId", "Name");
            return View(movieTheatreRoom);
        }

        // POST: MovieTheatreRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieTheatreRoomId,MovieId,TheatreRoomId,DateTime")] MovieTheatreRoomDto movieTheatreRoom)
        {
            if (id != movieTheatreRoom.MovieTheatreRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.MovieTheatreRooms.Update(movieTheatreRoom.ToDb());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTheatreRoomExists(movieTheatreRoom.MovieTheatreRoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieTheatreRoom);
        }

        // GET: MovieTheatreRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheatreRoom = await _context.MovieTheatreRooms
                .FirstOrDefaultAsync(m => m.MovieTheatreRoomId == id);
            if (movieTheatreRoom == null)
            {
                return NotFound();
            }

            return View(movieTheatreRoom);
        }

        // POST: MovieTheatreRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieTheatreRoom = await _context.MovieTheatreRooms.FindAsync(id);
            _context.MovieTheatreRooms.Remove(movieTheatreRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTheatreRoomExists(int id)
        {
            return _context.MovieTheatreRooms.Any(e => e.MovieTheatreRoomId == id);
        }
    }
}

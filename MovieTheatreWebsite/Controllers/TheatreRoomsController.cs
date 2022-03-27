#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using MovieTheatreModels.Dto;

namespace MovieTheatreWebsite.Controllers
{
    public class TheatreRoomsController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public TheatreRoomsController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: TheatreRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheatreRooms.ToListAsync());
        }

        // GET: TheatreRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatreRoom = await _context.TheatreRooms
                .FirstOrDefaultAsync(m => m.TheatreRoomId == id);
            if (theatreRoom == null)
            {
                return NotFound();
            }

            return View(theatreRoom);
        }

        // GET: TheatreRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheatreRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TheatreRoomId,Name,ChairCount")] TheatreRoomDto theatreRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theatreRoom.ToDb());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theatreRoom);
        }

        // GET: TheatreRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatreRoom = await _context.TheatreRooms.FindAsync(id);
            if (theatreRoom == null)
            {
                return NotFound();
            }
            return View(theatreRoom);
        }

        // POST: TheatreRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TheatreRoomId,Name,ChairCount")] TheatreRoomDto theatreRoom)
        {
            if (id != theatreRoom.TheatreRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theatreRoom.ToDb());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheatreRoomExists(theatreRoom.TheatreRoomId))
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
            return View(theatreRoom);
        }

        // GET: TheatreRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatreRoom = await _context.TheatreRooms
                .FirstOrDefaultAsync(m => m.TheatreRoomId == id);
            if (theatreRoom == null)
            {
                return NotFound();
            }

            return View(theatreRoom);
        }

        // POST: TheatreRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theatreRoom = await _context.TheatreRooms.FindAsync(id);
            _context.TheatreRooms.Remove(theatreRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheatreRoomExists(int id)
        {
            return _context.TheatreRooms.Any(e => e.TheatreRoomId == id);
        }
    }
}

#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using MovieTheatreModels.Dto;

namespace MovieTheatreWebsite.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public ReservationsController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var movieTheatreWebsiteContext = _context.Reservations.Include(r => r.MovieTheatreRoom).Include(r => r.User).Include(r => r.Prices);
            return View(await movieTheatreWebsiteContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id, Guid? reservationGuid)
        {
            if (id == null && reservationGuid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var reservation = await _context.Reservations
                .Include(r => r.MovieTheatreRoom)
                .Include(r => r.User)
                .Include(r => r.Prices)
                .FirstOrDefaultAsync(m => m.ReservationId.Equals(id) || m.ReservationGuid.Equals(reservationGuid));

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["MovieTheatreRoomId"] = new SelectList(_context.Set<MovieTheatreRoom>(), "MovieTheatreRoomId", "MovieTheatreRoomId");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Username");
            ViewData["PriceId"] = new SelectList(_context.Set<Price>(), "PriceId", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,UserId,MovieTheatreRoomId,PriceId,ChairNr")] ReservationDto reservation)
        {

            if (ModelState.IsValid)
            {
                _context.Add(reservation.ToDb());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieTheatreRoomId"] = new SelectList(_context.Set<MovieTheatreRoom>(), "MovieTheatreRoomId", "MovieTheatreRoomId", reservation.MovieTheatreRoomId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Username", reservation.UserId);
            ViewData["PriceId"] = new SelectList(_context.Set<Price>(), "PriceId", "Name");
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["MovieTheatreRoomId"] = new SelectList(_context.Set<MovieTheatreRoom>(), "MovieTheatreRoomId", "MovieTheatreRoomId", reservation.MovieTheatreRoomId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Password", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,MovieTheatreRoomId,ChairNr")] ReservationDto reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation.ToDb());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["MovieTheatreRoomId"] = new SelectList(_context.Set<MovieTheatreRoom>(), "MovieTheatreRoomId", "MovieTheatreRoomId", reservation.MovieTheatreRoomId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Password", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.MovieTheatreRoom)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}

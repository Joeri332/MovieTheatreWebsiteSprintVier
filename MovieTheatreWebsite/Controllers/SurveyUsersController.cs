#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;

namespace MovieTheatreWebsite.Controllers
{
    public class SurveyUsersController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public SurveyUsersController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: SurveyUsers
        public async Task<IActionResult> Index()
        {
            var movieTheatreWebsiteContext = _context.SurveyUser.Include(s => s.Survey);
            return View(await movieTheatreWebsiteContext.ToListAsync());
        }

        // GET: SurveyUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUser = await _context.SurveyUser
                .Include(s => s.Survey)
                .FirstOrDefaultAsync(m => m.SurveyUserId == id);
            if (surveyUser == null)
            {
                return NotFound();
            }

            return View(surveyUser);
        }

        // GET: SurveyUsers/Create
        public IActionResult Create()
        {
            ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Description");
            return View();
        }

        // POST: SurveyUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyUserId,SurveyId,Name,Email,CreatedDate")] SurveyUser surveyUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Description", surveyUser.SurveyId);
            return View(surveyUser);
        }

        // GET: SurveyUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUser = await _context.SurveyUser.FindAsync(id);
            if (surveyUser == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Description", surveyUser.SurveyId);
            return View(surveyUser);
        }

        // POST: SurveyUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyUserId,SurveyId,Name,Email,CreatedDate")] SurveyUser surveyUser)
        {
            if (id != surveyUser.SurveyUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyUserExists(surveyUser.SurveyUserId))
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
            ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Description", surveyUser.SurveyId);
            return View(surveyUser);
        }

        // GET: SurveyUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUser = await _context.SurveyUser
                .Include(s => s.Survey)
                .FirstOrDefaultAsync(m => m.SurveyUserId == id);
            if (surveyUser == null)
            {
                return NotFound();
            }

            return View(surveyUser);
        }

        // POST: SurveyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surveyUser = await _context.SurveyUser.FindAsync(id);
            _context.SurveyUser.Remove(surveyUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyUserExists(int id)
        {
            return _context.SurveyUser.Any(e => e.SurveyUserId == id);
        }
    }
}

#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;


namespace MovieTheatreWebsite.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class SurveyUserAnswersController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public SurveyUserAnswersController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: SurveyUserAnswers
        public async Task<IActionResult> Index()
        {
            var movieTheatreWebsiteContext = _context.SurveyUserAnswers.Include(s => s.SurveyQuestion).Include(s => s.SurveyUser);
            return View(await movieTheatreWebsiteContext.ToListAsync());
        }

        // GET: SurveyUserAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUserAnswers = await _context.SurveyUserAnswers
                .Include(s => s.SurveyQuestion)
                .Include(s => s.SurveyUser)
                .FirstOrDefaultAsync(m => m.SurveyUserAnswerId == id);
            if (surveyUserAnswers == null)
            {
                return NotFound();
            }

            return View(surveyUserAnswers);
        }

        // GET: SurveyUserAnswers/Create
        public IActionResult Create()
        {
            ViewData["SurveyQuestionId"] = new SelectList(_context.SurveyQuestion, "SurveyQuestionId", "QuestionType");
            ViewData["SurveyUserId"] = new SelectList(_context.SurveyUser, "SurveyUserId", "Email");
            return View();
        }

        // POST: SurveyUserAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyUserAnswersId,SurveyUserId,SurveyQuestionId,QuestionOptionEnums")] SurveyUserAnswer surveyUserAnswers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyUserAnswers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SurveyQuestionId"] = new SelectList(_context.SurveyQuestion, "SurveyQuestionId", "QuestionType", surveyUserAnswers.SurveyQuestionId);
            ViewData["SurveyUserId"] = new SelectList(_context.SurveyUser, "SurveyUserId", "Email", surveyUserAnswers.SurveyUserId);
            return View(surveyUserAnswers);
        }

        // GET: SurveyUserAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUserAnswers = await _context.SurveyUserAnswers.FindAsync(id);
            if (surveyUserAnswers == null)
            {
                return NotFound();
            }
            ViewData["SurveyQuestionId"] = new SelectList(_context.SurveyQuestion, "SurveyQuestionId", "QuestionType", surveyUserAnswers.SurveyQuestionId);
            ViewData["SurveyUserId"] = new SelectList(_context.SurveyUser, "SurveyUserId", "Email", surveyUserAnswers.SurveyUserId);
            return View(surveyUserAnswers);
        }

        // POST: SurveyUserAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyUserAnswersId,SurveyUserId,SurveyQuestionId,QuestionOptionEnums")] SurveyUserAnswer surveyUserAnswers)
        {
            if (id != surveyUserAnswers.SurveyUserAnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyUserAnswers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyUserAnswersExists(surveyUserAnswers.SurveyUserAnswerId))
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
            ViewData["SurveyQuestionId"] = new SelectList(_context.SurveyQuestion, "SurveyQuestionId", "QuestionType", surveyUserAnswers.SurveyQuestionId);
            ViewData["SurveyUserId"] = new SelectList(_context.SurveyUser, "SurveyUserId", "Email", surveyUserAnswers.SurveyUserId);
            return View(surveyUserAnswers);
        }

        // GET: SurveyUserAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyUserAnswers = await _context.SurveyUserAnswers
                .Include(s => s.SurveyQuestion)
                .Include(s => s.SurveyUser)
                .FirstOrDefaultAsync(m => m.SurveyUserAnswerId == id);
            if (surveyUserAnswers == null)
            {
                return NotFound();
            }

            return View(surveyUserAnswers);
        }

        // POST: SurveyUserAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surveyUserAnswers = await _context.SurveyUserAnswers.FindAsync(id);
            _context.SurveyUserAnswers.Remove(surveyUserAnswers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyUserAnswersExists(int id)
        {
            return _context.SurveyUserAnswers.Any(e => e.SurveyUserAnswerId == id);
        }

    }
}

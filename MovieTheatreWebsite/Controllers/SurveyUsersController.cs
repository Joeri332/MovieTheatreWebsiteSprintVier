#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTheatreDatabase;
using MovieTheatreModels.Enums;

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

        public IActionResult SurveyPageIndex()
        {
            var surveys = _context.Survey.ToList();
            return View(surveys);
        }

        public IActionResult SurveyPageDetails(int? surveyId)
        {
            var surveys = _context.Survey.Include(x => x.SurveyQuestions).ToList();
            var survey = surveys.Find(x => x.SurveyId == surveyId);
            if (survey == null)
            {
                return RedirectToAction(nameof(SurveyPageIndex));
            }
            return View(survey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SurveyPageDetails(int? surveyId, int test)
        {
            //Option 1, fetch everything from request.form
            //Option 2, try to bind everything to SurveyDto(name, email List<SurveyQuestion>)
            var surveys = _context.Survey.Include(x => x.SurveyQuestions).ToList();
            var survey = surveys.Find(x => x.SurveyId == surveyId);
            if (survey == null)
            {
                return RedirectToAction(nameof(SurveyPageIndex));
            }

            var success = Request.Form.TryGetValue("Name", out var stringValueName);
            success = Request.Form.TryGetValue("Email", out var stringValueEmail) && success;

            if (!success)
                return RedirectToAction(nameof(SurveyPageIndex));

            await using var transaction = await _context.Database.BeginTransactionAsync();

            var surveyUser = new SurveyUser
            {
                Name = stringValueName.ToString(),
                Email = stringValueEmail.ToString(),
                SurveyId = survey.SurveyId,
                CreatedDate = DateTime.Now
            };
            surveyUser = _context.SurveyUser.Add(surveyUser).Entity;
            await _context.SaveChangesAsync();

            foreach (var surveyQuestion in survey.SurveyQuestions)
            {
                success = Request.Form.TryGetValue(surveyQuestion.SurveyQuestionId.ToString(), out var stringValue);
                success = int.TryParse(stringValue, out var optionsEnumInt) && success;
                if (!success)
                    return RedirectToAction(nameof(SurveyPageIndex));

                var surveyUserAnswer = new SurveyUserAnswer
                {
                    SurveyQuestionId = surveyQuestion.SurveyQuestionId,
                    SurveyUserId = surveyUser.SurveyUserId,
                    QuestionOptionEnums = (QuestionOptionEnums)optionsEnumInt
                };
                _context.SurveyUserAnswers.Add(surveyUserAnswer);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return View(survey);
        }
    }
}

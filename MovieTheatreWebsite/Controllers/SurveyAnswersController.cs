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
    public class SurveyAnswersController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public SurveyAnswersController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: SurveyAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SurveyAnswers.ToListAsync());
        }

        // GET: SurveyAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyAnswers = await _context.SurveyAnswers
                .FirstOrDefaultAsync(m => m.SurveyAnswersId == id);
            if (surveyAnswers == null)
            {
                return NotFound();
            }

            return View(surveyAnswers);
        }

        // GET: SurveyAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurveyAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyAnswersId,SurveyId,SurveyQuestionId,QuestionOptionEnums,name")] SurveyAnswers surveyAnswers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyAnswers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surveyAnswers);
        }

        // GET: SurveyAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyAnswers = await _context.SurveyAnswers.FindAsync(id);
            if (surveyAnswers == null)
            {
                return NotFound();
            }
            return View(surveyAnswers);
        }

        // POST: SurveyAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyAnswersId,SurveyId,SurveyQuestionId,QuestionOptionEnums,name")] SurveyAnswers surveyAnswers)
        {
            if (id != surveyAnswers.SurveyAnswersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyAnswers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyAnswersExists(surveyAnswers.SurveyAnswersId))
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
            return View(surveyAnswers);
        }

        // GET: SurveyAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyAnswers = await _context.SurveyAnswers
                .FirstOrDefaultAsync(m => m.SurveyAnswersId == id);
            if (surveyAnswers == null)
            {
                return NotFound();
            }

            return View(surveyAnswers);
        }

        // POST: SurveyAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surveyAnswers = await _context.SurveyAnswers.FindAsync(id);
            _context.SurveyAnswers.Remove(surveyAnswers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyAnswersExists(int id)
        {
            return _context.SurveyAnswers.Any(e => e.SurveyAnswersId == id);
        }
    }
}

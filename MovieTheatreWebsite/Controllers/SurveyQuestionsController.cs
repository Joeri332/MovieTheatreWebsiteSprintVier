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
    public class SurveyQuestionsController : Controller
    {
        private readonly MovieTheatreDatabaseContext _context;

        public SurveyQuestionsController(MovieTheatreDatabaseContext context)
        {
            _context = context;
        }

        // GET: SurveyQuestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.SurveyQuestion.ToListAsync());
        }

        // GET: SurveyQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion
                .FirstOrDefaultAsync(m => m.SurveyQuestionId == id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyQuestionId,Text,QuestionType,QuestionOptionEnums")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion.FindAsync(id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyQuestionId,Text,QuestionType,QuestionOptionEnums")] SurveyQuestion surveyQuestion)
        {
            if (id != surveyQuestion.SurveyQuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyQuestionExists(surveyQuestion.SurveyQuestionId))
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
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surveyQuestion = await _context.SurveyQuestion
                .FirstOrDefaultAsync(m => m.SurveyQuestionId == id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surveyQuestion = await _context.SurveyQuestion.FindAsync(id);
            _context.SurveyQuestion.Remove(surveyQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyQuestionExists(int id)
        {
            return _context.SurveyQuestion.Any(e => e.SurveyQuestionId == id);
        }
    }
}

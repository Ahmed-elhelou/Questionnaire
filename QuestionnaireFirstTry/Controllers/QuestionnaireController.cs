using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFirstTry.Data;
using QuestionnaireFirstTry.Models;

namespace QuestionnaireFirstTry.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionnaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questionnaire
        [Authorize(Policy = "View Questions Policy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Question.ToListAsync());
        }


        // GET: Questionnaire/Create
        [Authorize(Policy = "Add Questions Policy")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Questionnaire/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Add Questions Policy")]

        public async Task<IActionResult> Create([Bind("Id,QuestionBody,Option1,Option2,Option3,InsertDate,Active")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.InsertDate = DateTime.Now;
                question.Active = true;

                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questionnaire/Edit/5
        [Authorize(Policy = "Edit Questions Policy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questionnaire/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Edit Questions Policy")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionBody,Option1,Option2,Option3")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldQuestion = await _context.Question.FirstAsync(m => m.Id == id);
                if(oldQuestion == null)
                    return NotFound("This Qestion is not Found!!");
                oldQuestion.QuestionBody = question.QuestionBody;
                oldQuestion.Option1 = question.Option1;
                oldQuestion.Option2 = question.Option2;
                oldQuestion.Option3 = question.Option3;
                _context.Question.Update(oldQuestion);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questionnaire/Delete/5
        [Authorize(Policy = "Remove Questions Policy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questionnaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Remove Questions Policy")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFirstTry.Data;
using System.Threading.Tasks;

namespace QuestionnaireFirstTry.Controllers
{
    public class UserQuestion : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserQuestion(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var questions = await _context.Question.ToListAsync();
            return View(questions);
        }
    }
}

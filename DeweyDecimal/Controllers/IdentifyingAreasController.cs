using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;

namespace DeweyDecimal.Controllers
{
    public class IdentifyingAreasController : Controller
    {
        public readonly Dictionary<string, string> CallNumberCategory = new(){
            {"000-099", "General Works"},
            {"100-199", "Philosophy and Psychology"},
            {"200-299", "Religion"},
            {"300-399", "Social Sciences"},
            {"400-499", "Language"},
            {"500-599", "Natural Sciences and Mathematics"},
            {"600-699", "Technology"},
            {"700-799", "The Arts"},
            {"800-899", "Literature and Rhetoric"},
            {"900-999", "History, Biography, and Geography"},
        };
        public readonly Dictionary<string, string> CategoryCallNumber = new(){
            {"General Works","000-099"},
            {"Philosophy and Psychology", "100-199"},
            {"Religion", "200-299"},
            {"Social Sciences", "300-399"},
            {"Language", "400-499"},
            {"Natural Sciences and Mathematics", "500-599"},
            {"Technology", "600-699"},
            {"The Arts", "700-799"},
            {"Literature and Rhetoric", "800-899"},
            {"History, Biography, and Geography", "900-999"},
        };
        public IActionResult Index()
        {
            Quiz quiz = new();
            return View(quiz);
        }

        public IActionResult CallToCat()
        {
            Quiz quiz = new(true);
            return View(quiz);
        }

        public IActionResult CatToCall()
        {
            Quiz quiz = new(false);
            return View(quiz);
        }

        [HttpPost]
        public ActionResult<bool> CheckIfCorrect([FromBody] Dictionary<string, string> quiz)
        {
            List<string> questions = new(quiz.Keys);
            foreach (string question in questions)
            {
                if (CallNumberCategory.Keys.Contains(question))
                {
                    if (quiz[question] != CallNumberCategory[question])
                    {
                        return Ok (false);
                    }

                }
                else
                {
                    if (quiz[question] != CategoryCallNumber[question])
                    {
                        return Ok(false);
                    }
                }
            }
            return Ok(true);
        }
    }
}

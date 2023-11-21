using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;
using System.Collections.Generic;

namespace DeweyDecimal.Controllers
{
    public class IdentifyingAreasController : Controller
    {
        // Dictionary to map call number ranges to categories
        public readonly Dictionary<string, string> CallNumberCategory = new()
        {
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

        // Dictionary to map categories to call number ranges
        public readonly Dictionary<string, string> CategoryCallNumber = new()
        {
            {"General Works", "000-099"},
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

        // Action method for the default view
        public IActionResult Index()
        {
            // Create a new Quiz object and return the view
            Quiz quiz = new();
            return View(quiz);
        }

        // Action method to display the quiz from call number to category
        public IActionResult CallToCat()
        {
            Quiz quiz = new(true);
            return View(quiz);
        }

        // Action method to display the quiz from category to call number
        public IActionResult CatToCall()
        {
            Quiz quiz = new(false);
            return View(quiz);
        }

        // Action method to check correctness of quiz answers
        [HttpPost]
        public ActionResult<Dictionary<string, bool>> CheckIfCorrect([FromBody] Dictionary<string, string> quiz)
        {
            // Dictionary to store correctness of answers
            Dictionary<string, bool> correctness = new Dictionary<string, bool>();

            foreach (var (question, answer) in quiz)
            {
                // Check if the provided answer matches the correct answer
                if (CallNumberCategory.ContainsKey(question) && CallNumberCategory[question] == answer)
                {
                    correctness[question] = true;
                }
                else if (CategoryCallNumber.ContainsKey(question) && CategoryCallNumber[question] == answer)
                {
                    correctness[question] = true;
                }
                else
                {
                    correctness[question] = false;
                }
            }

            // Return the correctness dictionary as a JSON response
            return Ok(correctness);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DeweyDecimal.Controllers
{
    public class FindingCallNumbersController : Controller
    {
        // GET request handler
        public IActionResult Index()
        {
            FindingCallNumber findingCallNumber = new FindingCallNumber(4);
            return View(findingCallNumber);
        }

        public FindingCallNumber FindingCallNumber { get; set; }    

        // GET request handler
        public IActionResult OnGet()
        {
            FindingCallNumber = new FindingCallNumber(4);
            return View(FindingCallNumber);
        }

        // POST request handler
        public IActionResult OnPost()
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;
using System;
using System.Collections.Generic;

namespace DeweyDecimal.Controllers
{
    // Controller responsible for handling requests related to book replacement scenarios
    public class ReplacingBooksController : Controller
    {
        // Action method for handling requests to the 'Easy' page
        public IActionResult Easy()
        {
            // Generate a list of 5 random books and pass it to the 'Easy' view
            List<Book> books = Book.GenerateBooks(5);
            return View(books);
        }

        // Action method for handling requests to the 'Medium' page
        public IActionResult Medium()
        {
            // Generate a list of 10 random books and pass it to the 'Medium' view
            List<Book> books = Book.GenerateBooks(10);
            return View(books);
        }

        // Action method for handling requests to the 'Hard' page
        public IActionResult Hard()
        {
            // Generate a list of 10 random books and pass it to the 'Hard' view
            List<Book> books = Book.GenerateBooks(10);
            return View(books);
        }

        // HTTP POST action method to check if the order of books matches the sorted order
        [HttpPost]
        public ActionResult<List<Book>> CheckOrder([FromBody] List<Book> books)
        {
            try
            {
                // Use the InsertionSort method to sort the list of books
                List<Book> sortedBooks = InsertionSort(books);

                // Check if the original array 'books' is the same as the sorted array 'sortedBooks'.
                for (int i = 0; i < books.Count; i++)
                {
                    if (!books[i].Equals(sortedBooks[i]))
                    {
                        return Ok(false); // The order doesn't match.
                    }
                }

                return Ok(true); // The order matches.
            }
            catch
            {
                return BadRequest(); // Return a bad request response in case of an exception
            }
        }

        // Private method to perform Insertion Sort on a list of books
        private List<Book> InsertionSort(List<Book> books)
        {
            int n = books.Count;
            List<Book> sortedBooks = new List<Book>(n);

            foreach (var book in books)
            {
                sortedBooks.Add(book);
            }

            for (int i = 1; i < n; i++)
            {
                Book key = sortedBooks[i];
                int j = i - 1;

                // Compare by CallNumber first
                while (j >= 0 && sortedBooks[j].CallNumber > key.CallNumber)
                {
                    sortedBooks[j + 1] = sortedBooks[j];
                    j--;
                }

                // If CallNumbers are the same, compare by Author
                while (j >= 0 && sortedBooks[j].CallNumber == key.CallNumber && string.Compare(sortedBooks[j].Author, key.Author) > 0)
                {
                    sortedBooks[j + 1] = sortedBooks[j];
                    j--;
                }

                sortedBooks[j + 1] = key;
            }

            return sortedBooks;
        }
    }
}

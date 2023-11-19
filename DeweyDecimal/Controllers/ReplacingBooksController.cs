using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;

namespace DeweyDecimal.Controllers
{
    public class ReplacingBooksController : Controller
    {
        public IActionResult Easy()
        {
            List<Book> books = Book.GenerateBooks(5);
            return View(books);
        }

        public IActionResult Medium()
        {
            List<Book> books = Book.GenerateBooks(10);
            return View(books);
        }

        public IActionResult Hard()
        {
            List<Book> books = Book.GenerateBooks(10);
            return View(books);
        }

        [HttpPost]
        public ActionResult<List<Book>> CheckOrder([FromBody] List<Book> books)
        {
            try
            {
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
                return BadRequest();
            }
        }

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

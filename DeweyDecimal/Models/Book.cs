namespace DeweyDecimal.Models
{
    public class Book
    {
        // Properties representing the attributes of a book
        public int Id { get; set; }
        public string Author { get; set; }
        public double CallNumber { get; set; }

        // Parameterless constructor
        public Book()
        {
            // Parameterless constructor
        }

        // Constructor with parameters for initializing a book
        public Book(int id, string author, double callNumber)
        {
            Id = id;
            Author = author;
            CallNumber = callNumber;
        }

        // Constructor for generating a book with a specified ID and random author and call number
        public Book(int id)
        {
            Id = id;
            Author = RandomAuthor();
            CallNumber = RandomCallNumber();
        }

        // Static method to generate a list of books with random authors and call numbers
        public static List<Book> GenerateBooks(int count)
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < count; i++)
            {
                books.Add(new Book(i));
            }
            return books;
        }

        // Private method to generate a random author name (3 uppercase letters)
        private string RandomAuthor()
        {
            Random random = new Random();
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(alphabet, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Private method to generate a random Dewey Decimal call number
        private double RandomCallNumber()
        {
            Random random = new Random();

            // Dewey Decimal call numbers typically have three parts: integer, decimal point, and fraction.
            int integerPart = random.Next(1000); // Generate a random integer part (0 to 999)
            double fractionPart = random.NextDouble(); // Generate a random fraction part between 0 and 1

            // Combine the parts to form the Dewey Decimal call number.
            double deweyDecimalNumber = integerPart + Math.Round(fractionPart, 3);

            return deweyDecimalNumber;
        }
    }
}

namespace DeweyDecimal.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public double CallNumber { get; set; }

        public Book()
        {
            // Parameterless contructor
        }

        public Book(int id, string author, double callNumber)
        {
            Id = id;
            Author = author;
            CallNumber = callNumber;
        }

        public Book(int id)
        {
            Id = id;
            Author = RandomAuthor();
            CallNumber = RandomCallNumber();
        }

        public static List<Book> GenerateBooks(int count)
        {
            List<Book> books = new();
            for (int i = 0; i < count; i++)
            {
                books.Add(new Book(i));
            }
            return books;
        }

        private string RandomAuthor()
        {
            Random random = new();
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(alphabet, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private double RandomCallNumber()
        {
            Random random = new();
            // Dewey Decimal call numbers typically have three parts: integer, decimal point, and fraction.
            int integerPart = random.Next(1000); // Generate a random integer part (0 to 999)
            double fractionPart = random.NextDouble(); // Generate a random fraction part between 0 and 1

            // Combine the parts to form the Dewey Decimal call number.
            double deweyDecimalNumber = integerPart + Math.Round(fractionPart, 3);

            return deweyDecimalNumber;
        }
    }
}

namespace DeweyDecimal.Models
{
    // Represents a quiz with questions and answers related to Dewey Decimal categories
    public class Quiz
    {
        // Dictionary mapping between call numbers and categories
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

        // Dictionary mapping between categories and call numbers
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

        // List to store quiz questions
        public List<string> Questions { get; set; } = new List<string>();

        // List to store quiz answers
        public List<string> Answers { get; set; } = new List<string>();

        // Constructor to create a quiz with randomized questions and answers based on the provided flag
        public Quiz(bool callToCat)
        {
            // Determine the answer dictionary based on the flag
            Dictionary<string, string> answerDict = callToCat ? CallNumberCategory : CategoryCallNumber;

            // Create lists from the keys and values of the answer dictionary
            List<string> keyList = new List<string>(answerDict.Keys);
            List<string> answerList = new List<string>(answerDict.Values);

            Random rand = new Random();

            // Ensure distinct questions
            while (Questions.Count < 4)
            {
                string question = keyList[rand.Next(keyList.Count)];
                if (!Questions.Contains(question))
                {
                    Questions.Add(question);
                    // Add corresponding answers to Answers list
                    Answers.Add(answerDict[question]);
                }
            }

            // Ensure distinct answers
            while (Answers.Count < 7)
            {
                string answer = answerList[rand.Next(answerList.Count)];
                if (!Answers.Contains(answer))
                {
                    Answers.Add(answer);
                }
            }

            // Shuffle the order of questions and answers
            ShuffleQuestions();
            ShuffleAnswers();
        }

        // Parameterless constructor
        public Quiz()
        {
            // Empty constructor
        }

        // Private method to shuffle the order of questions
        private void ShuffleQuestions()
        {
            Random rand = new Random();
            Questions = Questions.OrderBy(a => rand.Next()).ToList();
        }

        // Private method to shuffle the order of answers
        private void ShuffleAnswers()
        {
            Random rand = new Random();
            Answers = Answers.OrderBy(a => rand.Next()).ToList();
        }
    }
}

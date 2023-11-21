namespace DeweyDecimal.Models
{
    public class Quiz
    {
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

        public List<string> Questions { get; set; } = new List<string>();
        public List<string> Answers { get; set; } = new List<string>();

        public Quiz(bool callToCat)
        {
            Dictionary<string, string> answerDict = callToCat ? CallNumberCategory : CategoryCallNumber;
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

            ShuffleQuestions();
            ShuffleAnswers();
        }

        public Quiz()
        {

        }

        private void ShuffleQuestions()
        {
            Random rand = new Random();
            Questions = Questions.OrderBy(a => rand.Next()).ToList();
        }

        private void ShuffleAnswers()
        {
            Random rand = new Random();
            Answers = Answers.OrderBy(a => rand.Next()).ToList();
        }
    }
}

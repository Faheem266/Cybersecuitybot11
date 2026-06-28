using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class QuizQuestion
    {
        public string Question { get; set; } = string.Empty;

        public List<string> Options { get; set; } = new List<string>();

        public int CorrectAnswer { get; set; }

        public string Explanation { get; set; } = string.Empty;
    }
}

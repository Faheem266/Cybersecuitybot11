namespace CybersecurityChatbot
{
    public class SentimentDetector
    {
        public string DetectMood(string input)
        {
            if (input.Contains("worried") || input.Contains("scared"))
            {
                return "💙 I understand your concern. Cybersecurity can feel overwhelming sometimes.";
            }

            if (input.Contains("confused"))
            {
                return "🤝 Don’t worry. I’ll do my best to guide you.";
            }

            if (input.Contains("happy"))
            {
                return "😊 Glad to hear that.";
            }

            return "";
        }

        public System.Threading.Tasks.Task<string> DetectMoodAsync(string input)
        {
            // lightweight async wrapper for potential future async work
            return System.Threading.Tasks.Task.FromResult(DetectMood(input));
        }
    }
}
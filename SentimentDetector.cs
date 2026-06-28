namespace CybersecurityChatbot
{
    public class SentimentDetector
    {
        public string DetectMood(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared"))
            {
                return "💙 I understand your concern. Cybersecurity threats can seem overwhelming, but learning about them is the first step to staying safe.";
            }

            if (input.Contains("confused"))
            {
                return "🤝 Don't worry. I'll explain cybersecurity concepts in a simple way.";
            }

            if (input.Contains("happy"))
            {
                return "😊 Glad to hear that. Let's continue learning about cybersecurity.";
            }

            if (input.Contains("frustrated"))
            {
                return "💡 Cybersecurity can be challenging at first. Take it one step at a time and I'll help guide you.";
            }

            return string.Empty;
        }

        public System.Threading.Tasks.Task<string> DetectMoodAsync(string input)
        {
            return System.Threading.Tasks.Task.FromResult(DetectMood(input));
        }
    }
}
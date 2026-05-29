namespace CybersecurityChatbot
{
    public class KeywordResponder
    {
        public string GetKeywordResponse(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return $"How can I assist you, {userName}?";
            }

            if (input.Contains("phish") || input.Contains("phishing"))
            {
                return $"It looks like you're asking about phishing, {userName}. Don't click suspicious links and verify sender addresses.";
            }

            if (input.Contains("password"))
            {
                return $"Passwords should be unique and stored in a password manager, {userName}. Enable MFA when possible.";
            }

            if (input.Contains("malware") || input.Contains("virus"))
            {
                return $"Keep your software updated and run reputable antivirus scans, {userName}.";
            }

            if (input.Contains("help") || input.Contains("assist") || input.Contains("support"))
            {
                return $"I'm here to help, {userName}. Ask me about phishing, passwords, updates, or safe browsing.";
            }

            if (input.Contains("bye") || input.Contains("exit") || input.Contains("quit"))
            {
                return $"Goodbye, {userName}. Stay safe online.";
            }

            return $"I didn't quite catch that, {userName}. Can you provide more details or ask about phishing, passwords, or malware?";
        }
    }
}

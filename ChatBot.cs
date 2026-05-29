namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private MemoryStore memory;
        private KeywordResponder responder;
        private SentimentDetector sentiment;

        public ChatBot()
        {
            memory = new MemoryStore();
            responder = new KeywordResponder();
            sentiment = new SentimentDetector();
        }

        public string GetWelcomeMessage()
        {
            return
@"███╗   ██╗██╗   ██╗██╗  ██╗███████╗██╗  ██╗██╗███████╗██╗     ██████╗
████╗  ██║╚██╗ ██╔╝╚██╗██╔╝██╔════╝██║  ██║██║██╔════╝██║     ██╔══██╗
██╔██╗ ██║ ╚████╔╝  ╚███╔╝ ███████╗███████║██║█████╗  ██║     ██║  ██║
██║╚██╗██║  ╚██╔╝   ██╔██╗ ╚════██║██╔══██║██║██╔══╝  ██║     ██║  ██║
██║ ╚████║   ██║   ██╔╝ ██╗███████║██║  ██║██║███████╗███████╗██████╔╝
╚═╝  ╚═══╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝╚═════╝

⚡ Initializing NyxShield… Guardian of the digital realm.
🛡️ I exist to protect you from cyber threats.

👤 Please enter your name to begin.";
        }

        public async System.Threading.Tasks.Task<string> GetResponse(string input)
        {
            // sanitize input (trim, remove control chars, limit length)
            input = Sanitize(input);

            // Save name if not set yet
            if (string.IsNullOrWhiteSpace(memory.UserName))
            {
                memory.UserName = input;
                return $"Welcome, {memory.UserName}. Your digital safety is now my priority.";
            }

            // Sentiment detection (async to avoid blocking UI)
            string emotion = await sentiment.DetectMoodAsync(input).ConfigureAwait(false);

            // Keyword response
            string keywordReply = responder.GetKeywordResponse(input, memory.UserName);

            if (!string.IsNullOrEmpty(emotion))
            {
                return emotion + "\n" + keywordReply;
            }

            return keywordReply;
        }

        private string Sanitize(string input)
        {
            if (input is null)
                return string.Empty;

            var s = input.Trim();

            // remove invisible/control characters
            s = System.Text.RegularExpressions.Regex.Replace(s, "\\p{C}+", string.Empty);

            // limit length to prevent huge inputs
            const int maxLen = 500;
            if (s.Length > maxLen)
                s = s.Substring(0, maxLen);

            return s.ToLowerInvariant();
        }
    }
}

using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class MemoryStore
    {
        public string UserName { get; set; } = string.Empty;

        public string FavouriteTopic { get; set; } = string.Empty;

        public string CurrentTopic { get; set; } = string.Empty;

        public bool NameCaptured { get; set; }

        public Dictionary<string, int> TopicCounts { get; set; }
            = new Dictionary<string, int>();

        public List<string> ActivityLog { get; set; }
            = new List<string>();
    }
}

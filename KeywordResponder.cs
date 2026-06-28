using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class KeywordResponder
    {
        private readonly Random random = new Random();

        private readonly Dictionary<string, List<string>> responses =
            new Dictionary<string, List<string>>
        {
            {
                "phishing",
                new List<string>
                {
                    "Phishing attacks often use fake emails to steal information.",
                    "Always verify the sender before clicking any links.",
                    "Be cautious of emails creating urgency or fear."
                }
            },

            {
                "password",
                new List<string>
                {
                    "Use strong passwords with letters, numbers and symbols.",
                    "Avoid reusing passwords across multiple accounts.",
                    "A password manager can help store passwords securely."
                }
            },

            {
                "malware",
                new List<string>
                {
                    "Keep your operating system updated.",
                    "Install reputable antivirus software.",
                    "Avoid downloading files from unknown sources."
                }
            },

            {
                "privacy",
                new List<string>
                {
                    "Review privacy settings on your accounts regularly.",
                    "Limit the personal information you share online.",
                    "Use privacy-focused browser settings where possible."
                }
            },

            {
                "vpn",
                new List<string>
                {
                    "VPNs encrypt your internet traffic.",
                    "VPNs can improve privacy on public Wi-Fi.",
                    "Choose a trusted VPN provider."
                }
            },

            {
                "2fa",
                new List<string>
                {
                    "Two-Factor Authentication adds an extra security layer.",
                    "2FA protects accounts even if passwords are stolen.",
                    "Authenticator apps are safer than SMS codes."
                }
            }
        };

        public string GetKeywordResponse(string input,
                                         string userName,
                                         MemoryStore memory)
        {
            input = input.ToLower();

            // NLP Simulation

            if (input.Contains("add task") ||
                input.Contains("create task") ||
                input.Contains("new task"))
            {
                return "Please enter the cybersecurity task you would like to add.";
            }

            if (input.Contains("remind me"))
            {
                return "I can help create a reminder. Please provide the task details.";
            }

            if (input.Contains("quiz"))
            {
                return "Type 'Start Quiz' to begin the cybersecurity quiz.";
            }

            if (input.Contains("activity log") ||
                input.Contains("what have you done for me"))
            {
                return "Displaying activity log...";
            }

            if (input.Contains("tell me more") ||
                input.Contains("explain more") ||
                input.Contains("another tip"))
            {
                if (!string.IsNullOrWhiteSpace(memory.CurrentTopic))
                {
                    return $"We're still discussing {memory.CurrentTopic}. Ask me something specific about it.";
                }
            }

            if (input.Contains("favourite topic"))
            {
                if (!string.IsNullOrWhiteSpace(memory.FavouriteTopic))
                {
                    return $"Your favourite cybersecurity topic appears to be {memory.FavouriteTopic}.";
                }

                return "I haven't determined your favourite topic yet.";
            }

            foreach (var topic in responses.Keys)
            {
                if (input.Contains(topic))
                {
                    memory.CurrentTopic = topic;

                    if (!memory.TopicCounts.ContainsKey(topic))
                    {
                        memory.TopicCounts[topic] = 0;
                    }

                    memory.TopicCounts[topic]++;

                    string selectedResponse =
                        responses[topic][random.Next(responses[topic].Count)];

                    return $"{selectedResponse}\n\nWould you like another tip about {topic}?";
                }
            }

            if (input.Contains("help"))
            {
                return
                    "I can help with phishing, malware, passwords, privacy, VPNs, 2FA, tasks, reminders and quizzes.";
            }

            if (input.Contains("bye") ||
                input.Contains("exit") ||
                input.Contains("quit"))
            {
                return $"Goodbye {userName}. Stay safe online!";
            }

            string[] fallback =
            {
                "Could you rephrase that?",
                "I'm not sure I understand. Can you explain differently?",
                "Can you provide more details?",
                "Try asking me about phishing, passwords, privacy or malware."
            };

            return fallback[random.Next(fallback.Length)];
        }
    }
}
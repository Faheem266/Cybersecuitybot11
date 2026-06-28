using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbot
{
    public class QuizManager
    {
        public int CurrentQuestionIndex { get; private set; }

        public int Score { get; private set; }

        public bool IsActive { get; private set; }

        public List<QuizQuestion> Questions { get; } = new()
        {
            new QuizQuestion
            {
                Question = "What should you do if you receive an email asking for your password?",
                Options = new List<string>
                {
                    "Reply with your password",
                    "Delete the email",
                    "Report the email as phishing",
                    "Ignore it"
                },
                CorrectAnswer = 2,
                Explanation = "Reporting phishing helps protect you and other users from scams."
            },

            new QuizQuestion
            {
                Question = "Should you reuse passwords across different accounts?",
                Options = new List<string>
                {
                    "Yes",
                    "No"
                },
                CorrectAnswer = 1,
                Explanation = "Unique passwords reduce damage if one account is compromised."
            },

            new QuizQuestion
            {
                Question = "Which option is the strongest password?",
                Options = new List<string>
                {
                    "password123",
                    "MyName2024",
                    "T!de-94-Moon#Vault",
                    "qwerty"
                },
                CorrectAnswer = 2,
                Explanation = "Strong passwords are long, unique and hard to guess."
            },

            new QuizQuestion
            {
                Question = "What does two-factor authentication add?",
                Options = new List<string>
                {
                    "A second proof that it is really you",
                    "A faster internet connection",
                    "A public password",
                    "A weaker login process"
                },
                CorrectAnswer = 0,
                Explanation = "2FA adds another layer of protection beyond the password."
            },

            new QuizQuestion
            {
                Question = "What should you check before clicking a link in an unexpected email?",
                Options = new List<string>
                {
                    "The sender and link destination",
                    "Only the email colour",
                    "How many emojis it uses",
                    "Whether it says urgent"
                },
                CorrectAnswer = 0,
                Explanation = "Checking the sender and URL helps you spot phishing attempts."
            },

            new QuizQuestion
            {
                Question = "Why are software updates important?",
                Options = new List<string>
                {
                    "They only change colours",
                    "They fix security weaknesses",
                    "They delete all malware automatically",
                    "They make passwords optional"
                },
                CorrectAnswer = 1,
                Explanation = "Updates often patch vulnerabilities attackers could exploit."
            },

            new QuizQuestion
            {
                Question = "What is a safe habit on public Wi-Fi?",
                Options = new List<string>
                {
                    "Use a trusted VPN",
                    "Access banking without protection",
                    "Share files with strangers",
                    "Disable all security settings"
                },
                CorrectAnswer = 0,
                Explanation = "A trusted VPN encrypts traffic on untrusted networks."
            },

            new QuizQuestion
            {
                Question = "What should you do with suspicious attachments?",
                Options = new List<string>
                {
                    "Open them to check",
                    "Download and share them",
                    "Avoid opening them and verify the sender",
                    "Rename them"
                },
                CorrectAnswer = 2,
                Explanation = "Suspicious attachments can contain malware."
            },

            new QuizQuestion
            {
                Question = "What does a password manager help with?",
                Options = new List<string>
                {
                    "Creating and storing unique passwords",
                    "Making every password the same",
                    "Posting passwords online",
                    "Removing the need for 2FA"
                },
                CorrectAnswer = 0,
                Explanation = "Password managers make unique passwords easier to use safely."
            },

            new QuizQuestion
            {
                Question = "What is social engineering?",
                Options = new List<string>
                {
                    "Tricking people into giving access or information",
                    "Building social media apps",
                    "Updating antivirus software",
                    "Encrypting a hard drive"
                },
                CorrectAnswer = 0,
                Explanation = "Social engineering targets human trust instead of only technology."
            }
        };

        public bool IsComplete => CurrentQuestionIndex >= Questions.Count;

        public void Start()
        {
            CurrentQuestionIndex = 0;
            Score = 0;
            IsActive = true;
        }

        public QuizQuestion GetCurrentQuestion()
        {
            return Questions[CurrentQuestionIndex];
        }

        public string SubmitAnswer(int answer)
        {
            if (!IsActive)
                return "Start the quiz first.";

            if (IsComplete)
                return GetFinalScore();

            QuizQuestion question = Questions[CurrentQuestionIndex];
            string result;

            if (answer == question.CorrectAnswer)
            {
                Score++;
                result = "Correct! " + question.Explanation;
            }
            else
            {
                result = "Incorrect. " + question.Explanation;
            }

            CurrentQuestionIndex++;

            if (IsComplete)
                return result + "\n\n" + GetFinalScore();

            return result + "\n\n" + FormatCurrentQuestion();
        }

        public string FormatCurrentQuestion()
        {
            if (IsComplete)
                return GetFinalScore();

            QuizQuestion question = GetCurrentQuestion();
            StringBuilder output = new StringBuilder();

            output.AppendLine($"Question {CurrentQuestionIndex + 1} of {Questions.Count}: {question.Question}");

            for (int i = 0; i < question.Options.Count; i++)
            {
                output.AppendLine($"{i + 1}. {question.Options[i]}");
            }

            return output.ToString().TrimEnd();
        }

        public string GetFinalScore()
        {
            IsActive = false;

            string feedback = Score switch
            {
                >= 8 => "Great job. You're a cybersecurity pro!",
                >= 5 => "Good effort. Review the explanations and keep practising.",
                _ => "Keep learning to stay safe online."
            };

            return $"Quiz complete. You scored {Score}/{Questions.Count}.\n{feedback}";
        }
    }
}

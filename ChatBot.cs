using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private readonly MemoryStore memory;
        private readonly KeywordResponder responder;
        private readonly SentimentDetector sentiment;
        private readonly TaskManager taskManager;
        private readonly QuizManager quizManager;

        public ChatBot()
        {
            memory = new MemoryStore();
            responder = new KeywordResponder();
            sentiment = new SentimentDetector();
            taskManager = new TaskManager();
            quizManager = new QuizManager();
        }

        public IReadOnlyList<CyberTask> Tasks => taskManager.GetTasks();

        public IReadOnlyList<string> ActivityLog => memory.ActivityLog;

        public QuizManager Quiz => quizManager;

        public string GetWelcomeMessage()
        {
            return
@"███╗   ██╗██╗   ██╗██╗  ██╗███████╗██╗  ██╗██╗███████╗██╗     ██████╗
████╗  ██║╚██╗ ██╔╝╚██╗██╔╝██╔════╝██║  ██║██║██╔════╝██║     ██╔══██╗
██╔██╗ ██║ ╚████╔╝  ╚███╔╝ ███████╗███████║██║█████╗  ██║     ██║  ██║
██║╚██╗██║  ╚██╔╝   ██╔██╗ ╚════██║██╔══██║██║██╔══╝  ██║     ██║  ██║
██║ ╚████║   ██║   ██╔╝ ██╗███████║██║  ██║██║███████╗███████╗██████╔╝
╚═╝  ╚═══╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝╚═════╝

⚡ Initializing NyxShield...

🛡️ Your Cybersecurity Awareness Assistant

Available Features:
• Cybersecurity Tips
• Task Assistant
• Reminders
• Cybersecurity Quiz
• Activity Log

👤 Please enter your name:";
        }

        public CyberTask AddTaskFromGui(string title, string description, string reminder)
        {
            CyberTask task = taskManager.AddTask(title, description, reminder);
            LogActivity($"Task added: {task.Title}" + FormatReminderForLog(task));
            return task;
        }

        public string CompleteTaskFromGui(int taskId)
        {
            string result = taskManager.CompleteTask(taskId);
            LogActivity(result);
            return result;
        }

        public string DeleteTaskFromGui(int taskId)
        {
            string result = taskManager.DeleteTask(taskId);
            LogActivity(result);
            return result;
        }

        public string SetReminderFromGui(int taskId, string reminder)
        {
            string result = taskManager.SetReminder(taskId, reminder);
            LogActivity(result);
            return result;
        }

        public string StartQuiz()
        {
            quizManager.Start();
            LogActivity("Quiz started");
            return quizManager.FormatCurrentQuestion();
        }

        public string SubmitQuizAnswer(int answer)
        {
            string result = quizManager.SubmitAnswer(answer);

            if (!quizManager.IsActive)
                LogActivity($"Quiz completed with score {quizManager.Score}/{quizManager.Questions.Count}");

            return result;
        }

        public string GetActivitySummary()
        {
            if (memory.ActivityLog.Count == 0)
                return "No activity has been recorded yet.";

            return "Here's a summary of recent actions:\n\n" +
                   string.Join("\n", memory.ActivityLog.Select((entry, index) => $"{index + 1}. {entry}"));
        }

        public string FormatTasks()
        {
            return taskManager.FormatTasks();
        }

        private void LogActivity(string action)
        {
            memory.ActivityLog.Add($"{DateTime.Now:g} - {action}");

            if (memory.ActivityLog.Count > 10)
                memory.ActivityLog.RemoveAt(0);
        }

        public async System.Threading.Tasks.Task<string> GetResponse(string input)
        {
            input = Sanitize(input);

            if (string.IsNullOrWhiteSpace(input))
                return "Please enter a message.";

            if (!memory.NameCaptured)
            {
                memory.UserName = input;
                memory.NameCaptured = true;
                LogActivity($"User identified as {memory.UserName}");

                return $"Welcome, {memory.UserName}. What cybersecurity topic interests you most?";
            }

            string lowerInput = input.ToLower();

            if (quizManager.IsActive && TryParseQuizAnswer(input, out int answer))
                return SubmitQuizAnswer(answer);

            if (IsActivityLogRequest(lowerInput))
                return GetActivitySummary();

            if (IsViewTaskRequest(lowerInput))
                return FormatTasks();

            if (IsStartQuizRequest(lowerInput))
                return StartQuiz();

            if (IsTaskCompletionRequest(lowerInput))
                return CompleteTaskFromChat(input);

            if (IsTaskDeletionRequest(lowerInput))
                return DeleteTaskFromChat(input);

            if (IsReminderRequest(lowerInput))
                return HandleReminderRequest(input);

            if (IsTaskCreationRequest(lowerInput))
                return HandleTaskCreation(input);

            string mood = await sentiment
                .DetectMoodAsync(input)
                .ConfigureAwait(false);

            string response = responder.GetKeywordResponse(input, memory.UserName, memory);

            if (memory.TopicCounts.Count > 0)
            {
                memory.FavouriteTopic = memory.TopicCounts
                    .OrderByDescending(x => x.Value)
                    .First()
                    .Key;
            }

            if (!string.IsNullOrWhiteSpace(mood))
                response = mood + "\n\n" + response;

            LogActivity($"User asked: {input}");

            return response;
        }

        private string HandleTaskCreation(string input)
        {
            string title = ExtractTaskTitle(input);
            string reminder = ExtractReminder(input);

            if (string.IsNullOrWhiteSpace(title))
                title = "New cybersecurity task";

            CyberTask task = taskManager.AddTask(
                title,
                $"Cybersecurity task: {title}",
                reminder);

            LogActivity($"Task added: {task.Title}" + FormatReminderForLog(task));

            if (string.IsNullOrWhiteSpace(reminder))
                return $"Task added: \"{task.Title}\". Would you like to set a reminder for this task?";

            return $"Task added: \"{task.Title}\". Reminder set for {task.ReminderDate}.";
        }

        private string HandleReminderRequest(string input)
        {
            int? taskId = TaskManager.ExtractTaskId(input);
            string reminder = ExtractReminder(input);
            string title = ExtractTaskTitle(input);

            if (string.IsNullOrWhiteSpace(reminder))
                reminder = "specified date";

            CyberTask? task = null;

            if (taskId.HasValue)
                task = taskManager.FindTask(taskId.Value);

            if (task == null && !string.IsNullOrWhiteSpace(title))
                task = taskManager.FindTaskByTitle(title);

            if (task == null)
            {
                task = taskManager.AddTask(
                    string.IsNullOrWhiteSpace(title) ? "Cybersecurity reminder" : title,
                    $"Cybersecurity task: {(string.IsNullOrWhiteSpace(title) ? "Cybersecurity reminder" : title)}",
                    reminder);

                LogActivity($"Reminder set: {task.Title} on {reminder}");
                return $"Reminder set for \"{task.Title}\" on {reminder}.";
            }

            taskManager.SetReminder(task.Id, reminder);
            LogActivity($"Reminder set: {task.Title} on {reminder}");

            return $"Reminder set for \"{task.Title}\" on {reminder}.";
        }

        private string CompleteTaskFromChat(string input)
        {
            int? taskId = TaskManager.ExtractTaskId(input);

            if (!taskId.HasValue)
                return "Please include the task number to mark complete, for example: complete task 1.";

            string result = taskManager.CompleteTask(taskId.Value);
            LogActivity(result);
            return result;
        }

        private string DeleteTaskFromChat(string input)
        {
            int? taskId = TaskManager.ExtractTaskId(input);

            if (!taskId.HasValue)
                return "Please include the task number to delete, for example: delete task 1.";

            string result = taskManager.DeleteTask(taskId.Value);
            LogActivity(result);
            return result;
        }

        private static bool IsActivityLogRequest(string input)
        {
            return input.Contains("show activity log") ||
                   input.Contains("activity log") ||
                   input.Contains("what have you done for me");
        }

        private static bool IsViewTaskRequest(string input)
        {
            return input.Contains("view tasks") ||
                   input.Contains("show tasks") ||
                   input.Contains("list tasks") ||
                   input.Contains("manage tasks");
        }

        private static bool IsStartQuizRequest(string input)
        {
            return input.Contains("start quiz") ||
                   input.Contains("begin quiz") ||
                   input.Contains("mini game") ||
                   input.Contains("cyber quiz");
        }

        private static bool IsTaskCreationRequest(string input)
        {
            return input.Contains("add task") ||
                   input.Contains("create task") ||
                   input.Contains("new task") ||
                   input.Contains("set a task") ||
                   input.Contains("task to");
        }

        private static bool IsReminderRequest(string input)
        {
            return input.Contains("remind me") ||
                   input.Contains("set reminder") ||
                   input.Contains("set a reminder") ||
                   input.Contains("reminder for");
        }

        private static bool IsTaskCompletionRequest(string input)
        {
            return input.Contains("complete task") ||
                   input.Contains("mark task") ||
                   input.Contains("done with task");
        }

        private static bool IsTaskDeletionRequest(string input)
        {
            return input.Contains("delete task") ||
                   input.Contains("remove task");
        }

        private static bool TryParseQuizAnswer(string input, out int answer)
        {
            string normalized = input.Trim().ToLower();
            answer = -1;

            if (int.TryParse(normalized, out int number))
            {
                answer = number - 1;
                return answer >= 0 && answer <= 3;
            }

            if (normalized.Length == 1 && normalized[0] >= 'a' && normalized[0] <= 'd')
            {
                answer = normalized[0] - 'a';
                return true;
            }

            Match match = Regex.Match(normalized, @"\b(answer\s*)?([1-4]|[a-d])\b");

            if (!match.Success)
                return false;

            string value = match.Groups[2].Value;

            if (int.TryParse(value, out number))
            {
                answer = number - 1;
                return true;
            }

            answer = value[0] - 'a';
            return true;
        }

        private static string ExtractTaskTitle(string input)
        {
            string title = input.Trim();

            title = Regex.Replace(title, @"^(please\s+)?(add|create|new|set)\s+(a\s+)?task\s*(to|for|:|-)?\s*",
                string.Empty, RegexOptions.IgnoreCase);
            title = Regex.Replace(title, @"^(please\s+)?remind me\s+to\s+",
                string.Empty, RegexOptions.IgnoreCase);
            title = Regex.Replace(title, @"^(please\s+)?set\s+(a\s+)?reminder\s+(to|for)\s+",
                string.Empty, RegexOptions.IgnoreCase);
            title = Regex.Replace(title, @"\b(in\s+\d+\s+days?|tomorrow|today|next week|on\s+.+|for\s+.+)$",
                string.Empty, RegexOptions.IgnoreCase);

            return title.Trim(' ', '.', '"', '\'');
        }

        private static string ExtractReminder(string input)
        {
            string lowerInput = input.ToLower();

            Match days = Regex.Match(lowerInput, @"\bin\s+(\d+)\s+days?\b");
            if (days.Success)
                return $"in {days.Groups[1].Value} days";

            if (lowerInput.Contains("tomorrow"))
                return "tomorrow";

            if (lowerInput.Contains("today"))
                return "today";

            if (lowerInput.Contains("next week"))
                return "next week";

            Match explicitDate = Regex.Match(input, @"\b(on|for)\s+(.+)$", RegexOptions.IgnoreCase);
            if (explicitDate.Success)
                return explicitDate.Groups[2].Value.Trim(' ', '.', '"', '\'');

            return string.Empty;
        }

        private static string FormatReminderForLog(CyberTask task)
        {
            if (string.IsNullOrWhiteSpace(task.ReminderDate))
                return " (no reminder set)";

            return $" (Reminder set for {task.ReminderDate})";
        }

        private static string Sanitize(string input)
        {
            if (input == null)
                return string.Empty;

            input = input.Trim();

            input = Regex.Replace(input, "\\p{C}+", string.Empty);

            const int maxLength = 500;

            if (input.Length > maxLength)
                input = input.Substring(0, maxLength);

            return input;
        }
    }
}

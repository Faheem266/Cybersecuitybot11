using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot
{
    public class TaskManager
    {
        private readonly string storagePath;
        private readonly List<CyberTask> tasks = new();

        public TaskManager()
        {
            string folder = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                "NyxShield");

            Directory.CreateDirectory(folder);
            storagePath = Path.Combine(folder, "tasks.json");
            LoadTasks();
        }

        public CyberTask AddTask(string title, string description = "", string reminderDate = "")
        {
            CyberTask task = new CyberTask
            {
                Id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1,
                Title = title,
                Description = string.IsNullOrWhiteSpace(description)
                    ? $"Cybersecurity task: {title}"
                    : description,
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            tasks.Add(task);
            SaveTasks();

            return task;
        }

        public string SetReminder(int taskId, string reminderDate)
        {
            CyberTask? task = FindTask(taskId);

            if (task == null)
                return "Task not found.";

            task.ReminderDate = reminderDate;
            SaveTasks();

            return $"Reminder set for {task.Title}: {reminderDate}.";
        }

        public IReadOnlyList<CyberTask> GetTasks()
        {
            return tasks;
        }

        public string CompleteTask(int taskId)
        {
            CyberTask? task = FindTask(taskId);

            if (task == null)
                return "Task not found.";

            task.IsCompleted = true;
            SaveTasks();

            return $"Task {task.Title} marked as complete.";
        }

        public string DeleteTask(int taskId)
        {
            CyberTask? task = FindTask(taskId);

            if (task == null)
                return "Task not found.";

            tasks.Remove(task);
            SaveTasks();

            return $"Task {task.Title} deleted.";
        }

        public CyberTask? FindTask(int taskId)
        {
            return tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public CyberTask? FindTaskByTitle(string title)
        {
            string normalizedTitle = title.ToLower();

            return tasks.FirstOrDefault(t =>
                t.Title.ToLower().Contains(normalizedTitle) ||
                normalizedTitle.Contains(t.Title.ToLower()));
        }

        public string FormatTasks()
        {
            if (tasks.Count == 0)
                return "No tasks currently stored.";

            StringBuilder output = new StringBuilder("Current Tasks:\n\n");

            foreach (CyberTask task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Pending";
                string reminder = string.IsNullOrWhiteSpace(task.ReminderDate)
                    ? "No reminder set"
                    : $"Reminder: {task.ReminderDate}";

                output.AppendLine($"{task.Id}. {task.Title}");
                output.AppendLine($"   Description: {task.Description}");
                output.AppendLine($"   Status: {status} | {reminder}");
            }

            return output.ToString().TrimEnd();
        }

        public static int? ExtractTaskId(string input)
        {
            Match match = Regex.Match(input, @"\btask\s*(\d+)\b|\b#?(\d+)\b",
                RegexOptions.IgnoreCase);

            if (!match.Success)
                return null;

            string value = match.Groups[1].Success
                ? match.Groups[1].Value
                : match.Groups[2].Value;

            if (int.TryParse(value, out int taskId))
                return taskId;

            return null;
        }

        private void LoadTasks()
        {
            if (!File.Exists(storagePath))
                return;

            string json = File.ReadAllText(storagePath);
            List<CyberTask>? storedTasks = JsonSerializer.Deserialize<List<CyberTask>>(json);

            if (storedTasks == null)
                return;

            tasks.Clear();
            tasks.AddRange(storedTasks);
        }

        private void SaveTasks()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(storagePath, json);
        }
    }
}

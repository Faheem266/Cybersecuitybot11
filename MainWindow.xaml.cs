using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot bot;

        public MainWindow()
        {
            InitializeComponent();

            bot = new ChatBot();
            ChatDisplay.Text += bot.GetWelcomeMessage();

            RefreshTasks();
            RefreshActivityLog();
            ClearQuizOptions();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await ProcessMessageAsync().ConfigureAwait(true);
        }

        private async void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await ProcessMessageAsync().ConfigureAwait(true);
            }
        }

        private async System.Threading.Tasks.Task ProcessMessageAsync()
        {
            string userInput = InputBox.Text;

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            ChatDisplay.Text += $"\n\n💬 You: {userInput}";

            string response = await bot.GetResponse(userInput).ConfigureAwait(true);

            ChatDisplay.Text += $"\n🤖 NyxShield: {response}";
            ChatDisplay.ScrollToEnd();

            InputBox.Clear();
            RefreshTasks();
            RefreshActivityLog();
            LoadCurrentQuizQuestion();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                TaskStatusText.Text = "Please enter a task title.";
                return;
            }

            CyberTask task = bot.AddTaskFromGui(
                title,
                TaskDescriptionInput.Text.Trim(),
                TaskReminderInput.Text.Trim());

            TaskStatusText.Text = $"Task added: {task.Title}";
            TaskTitleInput.Clear();
            TaskDescriptionInput.Clear();
            TaskReminderInput.Clear();

            RefreshTasks();
            RefreshActivityLog();
        }

        private void SetReminderButton_Click(object sender, RoutedEventArgs e)
        {
            CyberTask? task = GetSelectedTask();

            if (task == null)
            {
                TaskStatusText.Text = "Select a task before setting a reminder.";
                return;
            }

            string reminder = TaskReminderInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(reminder))
            {
                TaskStatusText.Text = "Enter a reminder such as 'in 3 days' or 'tomorrow'.";
                return;
            }

            TaskStatusText.Text = bot.SetReminderFromGui(task.Id, reminder);
            RefreshTasks();
            RefreshActivityLog();
        }

        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CyberTask? task = GetSelectedTask();

            if (task == null)
            {
                TaskStatusText.Text = "Select a task to mark complete.";
                return;
            }

            TaskStatusText.Text = bot.CompleteTaskFromGui(task.Id);
            RefreshTasks();
            RefreshActivityLog();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CyberTask? task = GetSelectedTask();

            if (task == null)
            {
                TaskStatusText.Text = "Select a task to delete.";
                return;
            }

            TaskStatusText.Text = bot.DeleteTaskFromGui(task.Id);
            RefreshTasks();
            RefreshActivityLog();
        }

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizFeedbackText.Text = bot.StartQuiz();
            LoadCurrentQuizQuestion();
            RefreshActivityLog();
        }

        private void SubmitQuizButton_Click(object sender, RoutedEventArgs e)
        {
            int? selectedAnswer = GetSelectedQuizAnswer();

            if (!selectedAnswer.HasValue)
            {
                QuizFeedbackText.Text = "Choose an answer before submitting.";
                return;
            }

            QuizFeedbackText.Text = bot.SubmitQuizAnswer(selectedAnswer.Value);
            LoadCurrentQuizQuestion();
            RefreshActivityLog();
        }

        private void RefreshLogButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshActivityLog();
        }

        private CyberTask? GetSelectedTask()
        {
            int selectedIndex = TaskList.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= bot.Tasks.Count)
                return null;

            return bot.Tasks[selectedIndex];
        }

        private void RefreshTasks()
        {
            TaskList.ItemsSource = bot.Tasks
                .Select(task =>
                {
                    string status = task.IsCompleted ? "Completed" : "Pending";
                    string reminder = string.IsNullOrWhiteSpace(task.ReminderDate)
                        ? "No reminder set"
                        : $"Reminder: {task.ReminderDate}";

                    return $"{task.Id}. {task.Title}\n   {task.Description}\n   {status} | {reminder}";
                })
                .ToList();
        }

        private void RefreshActivityLog()
        {
            ActivityLogDisplay.Text = bot.GetActivitySummary();
        }

        private void LoadCurrentQuizQuestion()
        {
            if (!bot.Quiz.IsActive || bot.Quiz.IsComplete)
            {
                ClearQuizOptions();
                return;
            }

            QuizQuestion question = bot.Quiz.GetCurrentQuestion();

            QuizQuestionText.Text =
                $"Question {bot.Quiz.CurrentQuestionIndex + 1} of {bot.Quiz.Questions.Count}: {question.Question}";

            RadioButton[] options =
            {
                QuizOption1,
                QuizOption2,
                QuizOption3,
                QuizOption4
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (i < question.Options.Count)
                {
                    options[i].Content = question.Options[i];
                    options[i].Visibility = Visibility.Visible;
                    options[i].IsChecked = false;
                }
                else
                {
                    options[i].Visibility = Visibility.Collapsed;
                    options[i].IsChecked = false;
                }
            }
        }

        private int? GetSelectedQuizAnswer()
        {
            RadioButton[] options =
            {
                QuizOption1,
                QuizOption2,
                QuizOption3,
                QuizOption4
            };

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].IsChecked == true)
                    return i;
            }

            return null;
        }

        private void ClearQuizOptions()
        {
            QuizQuestionText.Text = "Start the quiz to test your cybersecurity knowledge.";

            RadioButton[] options =
            {
                QuizOption1,
                QuizOption2,
                QuizOption3,
                QuizOption4
            };

            foreach (RadioButton option in options)
            {
                option.Content = string.Empty;
                option.IsChecked = false;
                option.Visibility = Visibility.Collapsed;
            }
        }
    }
}

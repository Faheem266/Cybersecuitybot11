using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private ChatBot bot;

        public MainWindow()
        {
            InitializeComponent();

            bot = new ChatBot();

            ChatDisplay.Text += bot.GetWelcomeMessage();
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

            InputBox.Clear();
        }
    }
}
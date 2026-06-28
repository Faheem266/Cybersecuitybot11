# NyxShield Cybersecurity Awareness Chatbot

NyxShield is a C# WPF chatbot designed to educate users about cybersecurity while providing an interactive and personalized experience.

## Features

- Personalized interaction (uses your name)
- GUI-based chatbot with real-time responses
- Cybersecurity education:
  - Password safety
  - Phishing awareness
  - Malware protection
  - VPN usage
  - Two-Factor Authentication (2FA)
  - Safe browsing tips
- Task assistant for cybersecurity-related tasks
- Reminder support for tasks
- Persistent task storage using a local JSON database file
- 10-question cybersecurity mini-game quiz with scoring and feedback
- NLP-style command recognition for task, reminder, quiz, and log requests
- Activity log showing the last 10 significant chatbot actions
- WPF tabs for Chat, Tasks, Quiz, and Activity Log

## Project Structure

The project contains a WPF front-end and supporting chatbot logic:

NyxShield/
- ChatBot.cs (main chatbot logic)
- MemoryStore.cs (user data model)
- KeywordResponder.cs (keyword-based replies)
- SentimentDetector.cs (simple sentiment/responses)
- CyberTask.cs / TaskManager.cs (task and reminder logic)
- QuizQuestion.cs / QuizManager.cs (quiz mini-game logic)
- MainWindow.xaml / MainWindow.xaml.cs (WPF UI)

> Note: Filenames in your workspace may vary slightly; check the project root for actual file names.

## Requirements

- .NET 6/7/10 (project targets .NET 10)
- Windows OS (for optional SoundPlayer audio support in console)

## Running the app

1. Open the project in Visual Studio or VS Code.
2. Restore dependencies: `dotnet restore`.
3. Build and run: `dotnet run` or press F5 in Visual Studio.

## Example commands

- what is phishing
- add task - Review privacy settings
- remind me to update my password tomorrow
- view tasks
- complete task 1
- start quiz
- answer 2
- show activity log
- help
- exit

## Future improvements

- AI-powered responses
- MySQL-backed task storage when a MySQL server and connector package are available
- Export activity logs
- Voice interaction

## License

Free for educational use.

---

## Video presentation

- [NyxShield Cybersecurity Chatbot video presentation part 2](https://youtu.be/J15ueORE-8E)

Created and maintained as part of the NyxShield Cybersecurity Chatbot project.

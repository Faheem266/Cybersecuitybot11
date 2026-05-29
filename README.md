# NyxShield Cybersecurity Chatbot

NyxShield is a C# console-based chatbot designed to educate users about cybersecurity while providing an interactive and personalized experience.

## Features

- Personalized interaction (uses your name)
- Interactive chatbot with real-time responses
- Cybersecurity education:
  - Password safety
  - Phishing awareness
  - Malware protection
  - VPN usage
  - Two-Factor Authentication (2FA)
  - Safe browsing tips
- ASCII art logo display
- Typewriter-style text effect
- Optional audio greeting (Windows only)
- Simple WPF interface included in the repository

## Project Structure

The project contains a WPF front-end and supporting chatbot logic:

NyxShield/
- Program.cs (entry point)
- ChatBot.cs (main chatbot logic)
- MemoryStore.cs (user data model)
- KeywordResponder.cs (keyword-based replies)
- SentimentDetector.cs (simple sentiment/responses)
- MainWindow.xaml / MainWindow.xaml.cs (WPF UI)
- greeting.wav (optional audio file)

> Note: Filenames in your workspace may vary slightly; check the project root for actual file names.

## Requirements

- .NET 6/7/10 (project targets .NET 10)
- Windows OS (for optional SoundPlayer audio support in console)

## Running the app

1. Open the project in Visual Studio or VS Code.
2. Restore dependencies: `dotnet restore`.
3. Build and run: `dotnet run` or press F5 in Visual Studio.

## Example commands

- hello
- what is phishing
- how to create a strong password
- tips
- help
- exit

## Optional audio greeting

To enable the audio greeting on Windows:

1. Add a `greeting.wav` file to the project root.
2. Install the Windows extensions package: `dotnet add package System.Windows.Extensions`.
3. Target a windows TFM if required for audio: `<TargetFramework>net10.0-windows</TargetFramework>` in the project file.

## Future improvements

- AI-powered responses
- Save chat history
- GUI improvements and accessibility
- Voice interaction

## License

Free for educational use.

---

## Video presentation

- [NyxShield Cybersecurity Chatbot video presentation part 2](https://youtu.be/J15ueORE-8E)

Created and maintained as part of the NyxShield Cybersecurity Chatbot project.

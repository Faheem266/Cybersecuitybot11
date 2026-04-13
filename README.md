# 🛡️ NyxShield Cybersecurity Chatbot

NyxShield is a C# console-based chatbot designed to educate users about cybersecurity while providing an interactive and personalized experience.

---

## 🚀 Features

- 👤 Personalized interaction (uses your name)
- 💬 Interactive chatbot with real-time responses
- 🔐 Cybersecurity education:
  - Password safety
  - Phishing awareness
  - Malware protection
  - VPN usage
  - Two-Factor Authentication (2FA)
  - Safe browsing tips
- 🎨 ASCII art logo display
- ⚡ Typewriter-style text effect
- 🔊 Optional audio greeting

---

## 🖼️ Screenshots

### 💻 Chatbot Interface
![Chatbot Screenshot](screenshot1.png)

### ⚠️ Error Fix Example
![Error Screenshot](screenshot2.png)

---

## 📁 Project Structure

NyxShield/
│
├── Program.cs        # Entry point of the application
├── Chatbot.cs        # Main chatbot logic
├── User.cs           # User data model
├── AudioPlayer.cs    # Handles audio playback (optional)
└── greeting.wav      # Audio file (optional)

---

## 🛠️ Requirements

- .NET 6 or higher
- Windows OS (for SoundPlayer audio support)

---

## ▶️ How to Run

1. Open the project in Visual Studio or VS Code  
2. Restore dependencies:
   dotnet restore  
3. Run the program:
   dotnet run  

---

## 💡 Example Commands

- hello  
- what is phishing  
- how to create a strong password  
- tips  
- help  
- exit  

## 🎬 Video Presentation

Watch the code walkthrough and demo on YouTube: [NyxShield Presentation](https://youtu.be/_rbfv1TizoE)

---

## 🔊 Audio Feature (Optional)

To enable audio greeting:

1. Add a greeting.wav file to your project  
2. Install package:
   dotnet add package System.Windows.Extensions  
3. Update project file:
   <TargetFramework>net7.0-windows</TargetFramework>

---

## 🔮 Future Improvements

- AI-powered responses  
- Save chat history  
- GUI version  
- Voice interaction  

---

## 👨‍💻 Author

Cybersecurity Chatbot Project (C#)

---

## 📜 License

Free for educational use.

using System;

partial class Program
{
    static void Main(string[] args)
    {
        Console.Title = "NyxShield Cybersecurity Bot";

        try
        {
            // Play voice greeting
            AudioPlayer.PlayGreeting();
        }
        catch
        {
            Console.WriteLine("⚠️ Audio unavailable, continuing without sound...");
        }

        // Show ASCII art
        Chatbot.DisplayLogo();

        // Start chatbot
        Chatbot bot = new Chatbot();
        bot.StartChat();
    }
}
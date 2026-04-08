using System;
using System.Threading;

public class Chatbot
{
    private User user;

    public static void DisplayLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine(@"
███╗   ██╗██╗   ██╗██╗  ██╗███████╗██╗  ██╗██╗███████╗██╗     ██████╗ 
████╗  ██║╚██╗ ██╔╝╚██╗██╔╝██╔════╝██║  ██║██║██╔════╝██║     ██╔══██╗
██╔██╗ ██║ ╚████╔╝  ╚███╔╝ ███████╗███████║██║█████╗  ██║     ██║  ██║
██║╚██╗██║  ╚██╔╝   ██╔██╗ ╚════██║██╔══██║██║██╔══╝  ██║     ██║  ██║
██║ ╚████║   ██║   ██╔╝ ██╗███████║██║  ██║██║███████╗███████╗██████╔╝
╚═╝  ╚═══╝   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝╚══════╝╚══════╝╚═════╝ 
");

        Console.ResetColor();
    }

    public void StartChat()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        TypeEffect("⚡ Initializing NyxShield… Guardian of the digital realm.");
        TypeEffect("🛡️ I exist to protect you from cyber threats.\n");

        Console.ResetColor();

        // Get user name
        Console.Write("👤 Enter your name: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("❗ Name cannot be empty. Enter again: ");
            name = Console.ReadLine();
        }

        user = new User(name);

        TypeEffect($"\nWelcome, {user.Name}. Your digital safety is now my priority.");
        TypeEffect("💡 Type 'help' to see what I can do.\n");

        // Chat loop
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n💬 You: ");
            Console.ResetColor();

            string input = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                TypeEffect("⚠️ I need something to process. Try asking a question.");
                continue;
            }

            if (input.Contains("exit"))
            {
                TypeEffect($"🔒 Session terminated. Stay secure out there, {user.Name}.");
                break;
            }

            Respond(input);
        }
    }

    private void Respond(string input)
    {
        string name = user.Name;

        if (input.Contains("hello") || input.Contains("hi"))
        {
            TypeEffect($"👋 Hello {name}. How can I protect you today?");
        }
        else if (input.Contains("how are you"))
        {
            TypeEffect($"🤖 Systems optimal, {name}. All defenses are active.");
        }
        else if (input.Contains("purpose"))
        {
            TypeEffect($"🛡️ My mission is to protect you, {name}, and strengthen your cybersecurity awareness.");
        }
        else if (input.Contains("password"))
        {
            TypeEffect($"🔑 {name}, use strong passwords: 12+ characters, mix symbols, and never reuse them.");
        }
        else if (input.Contains("phishing"))
        {
            TypeEffect($"🎣 Stay alert, {name}. Phishing emails often create urgency. Always verify the sender.");
        }
        else if (input.Contains("malware"))
        {
            TypeEffect($"💀 Malware can damage your system, {name}. Keep your software updated and avoid unknown downloads.");
        }
        else if (input.Contains("vpn"))
        {
            TypeEffect($"🌐 A VPN protects your privacy, {name}, especially on public Wi-Fi.");
        }
        else if (input.Contains("wifi"))
        {
            TypeEffect($"📡 Public Wi-Fi is risky, {name}. Avoid logging into sensitive accounts.");
        }
        else if (input.Contains("2fa") || input.Contains("two factor"))
        {
            TypeEffect($"🔐 Enable 2FA, {name}. It adds an extra layer of protection.");
        }
        else if (input.Contains("hacker"))
        {
            TypeEffect($"👾 Hackers exploit weaknesses, {name}. Stay cautious online.");
        }
        else if (input.Contains("safe browsing"))
        {
            TypeEffect($"🌍 Stick to HTTPS websites, {name}, and avoid suspicious downloads.");
        }
        else if (input.Contains("scam"))
        {
            TypeEffect($"⚠️ If it sounds too good to be true, {name}, it probably is.");
        }
        else if (input.Contains("identity theft"))
        {
            TypeEffect($"🕵️ Identity theft is serious, {name}. Protect your personal info.");
        }
        else if (input.Contains("update"))
        {
            TypeEffect($"⬆️ Keep your system updated, {name}. Updates fix vulnerabilities.");
        }
        else if (input.Contains("antivirus"))
        {
            TypeEffect($"🛡️ Antivirus software helps detect threats, {name}.");
        }
        else if (input.Contains("tips"))
        {
            TypeEffect($"📋 Cyber Tips for you, {name}:\n- Strong passwords\n- Enable 2FA\n- Avoid suspicious links\n- Update software");
        }
        else if (input.Contains("help"))
        {
            TypeEffect($"📖 Ask me about:\n- Passwords\n- Phishing\n- Malware\n- VPN\n- 2FA\n- Scams");
        }
        else
        {
            TypeEffect($"❓ I’m not sure about that, {name}. Try asking about cybersecurity topics.");
        }
    }

    private void TypeEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(20);
        }
        Console.WriteLine();
    }
}
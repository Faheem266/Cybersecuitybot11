using System.Media;
using System;

public class AudioPlayer
{
    public static void PlayGreeting()
    {
        try
        {
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.PlaySync();
        }
        catch (Exception)
        {
            Console.WriteLine("🔇 (Audio file missing)");
        }
    }
}

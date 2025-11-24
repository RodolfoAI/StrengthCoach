using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StrengthCoach.Resources
{
    public static class SoundResources
    {
        public static async Task PlayCountdownSoundAsync()
        {
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "short-beep.mp3");
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "StrengthCoach.Resources.Sounds.short-beep.mp3";

                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                        return;

                    using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(tempPath, UriKind.Absolute));
                player.Play();

                await Task.Delay(1000);
                
                player.Close();
            }
            catch { }
        }
    }
}
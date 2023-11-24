using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaHarbor
{
    public class UpdateYtDlp
    {
        public async Task UpdateYtDlpAsync(string updatingPleaseWait, string ytdlpUpdate, string updateError, string updateCompleted, string updateCompletedMessage)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = ".\\yt-dlp.exe";
                process.StartInfo.Arguments = "-U";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();

                // Güncelleme işlemi başarıyla tamamlandıysa bildirimi göster
                if (output.Contains("yt-dlp is up to date"))
                {
                    // ShowNotification($"{noUpdate}", $"{alreadyUpdatedText}");
                }
                else if (output.Contains("Updating to"))
                {
                    MessageBox.Show($"{updatingPleaseWait}", $"{ytdlpUpdate}");
                    ShowNotification($"{ytdlpUpdate}", $"{updatingPleaseWait}");
                }
                else
                {
                    MessageBox.Show($"{updateError}");
                }
                if (output.Contains("Updated yt-dlp to"))
                {
                    ShowNotification($"{updateCompleted}", $"{updateCompletedMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{updateError}: {ex.Message}");
            }
        }
        public Icon Icon { get; set; }
        private void ShowNotification(string title, string content)
        {
            NotifyIcon notifyIcon1 = new NotifyIcon();
            notifyIcon1.Icon = Icon;
            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = content;
            notifyIcon1.ShowBalloonTip(1000); // Bildirimi 1 saniye boyunca göster
        }
    }
    
}

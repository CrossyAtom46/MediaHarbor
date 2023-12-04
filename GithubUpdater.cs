using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaHarbor
{
    public class GitHubUpdater
    {
        private const string Owner = "CrossyAtom46";
        private const string Repo = "MediaHarbor";
        private const string CurrentVersion = "1.1.3";

        public static async Task CheckForUpdates(string currentCulture)
        {
            string updateNotification, newUpdateText, noUpdateMessage, noUpdateText;

            if (currentCulture == "tr-TR")
            {
                updateNotification = "Güncelleme Mevcut";
                newUpdateText = "Yeni Sürüm:";
                noUpdateMessage = "Güncelleme Yok";
                noUpdateText = "Son Sürüm Kullanıyorsunuz.";
            }
            else
            {
                updateNotification = "Update Avilable";
                newUpdateText = "New Version:";
                noUpdateMessage = "No Update";
                noUpdateText = "You're Using Lastest Version.";
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

                try
                {
                    string apiUrl = $"https://api.github.com/repos/{Owner}/{Repo}/releases/latest";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        dynamic releaseInfo = JObject.Parse(json);

                        string latestVersion = releaseInfo.tag_name;

                        if (Version.TryParse(CurrentVersion, out Version current) && Version.TryParse(latestVersion, out Version latest))
                        {
                            if (latest > current)
                            {
                                string newUpdateTextWithVersion = $"{newUpdateText} {latestVersion}";
                                ShowNotificationUpdate(updateNotification, newUpdateTextWithVersion);
                            }
                            else
                            {
                                // ShowNotification(noUpdateMessage, noUpdateText);
                            }
                        }
                    }
                    else
                    {
                        ShowNotification(updateNotification, $"Can't Contact to GitHub API. Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    ShowNotification(updateNotification, $"There was an error while updating: {ex.Message}");
                }
            }
        }

        private static void ShowNotificationUpdate(string title, string message)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Warning;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(5000); // Bildirimi 5 saniye boyunca göster

            // Bildirim simgesine tıklandığında olayı dinle
            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            notifyIcon.Dispose();
        }

        private static void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // Tıklandığında GitHub linkine yönlendir
            if (e.Button == MouseButtons.Left)
            {
                System.Diagnostics.Process.Start("https://github.com/CrossyAtom46/MediaHarbor/releases/latest");
            }
        }

        private static void ShowNotification(string title, string message)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Warning;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(5000); // Bildirimi 5 saniye boyunca göster
            notifyIcon.Dispose();
        }
    }
}

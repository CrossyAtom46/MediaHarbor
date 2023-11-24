using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Resources;
using System.Threading;
using WK.Libraries.BetterFolderBrowserNS;
using System.Security.Principal;
using MediaHarbor.Properties;
using Newtonsoft.Json.Linq;

namespace MediaHarbor
{

    public partial class Form1 : MetroForm
    {
        public string downloadDir;
        public string downloadDir2;
        string userLanguage = CultureInfo.InstalledUICulture.Name;

        public Form1()
        {
            InitializeComponent();
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            // richTextBox1 için renk değişimi
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;

            // richTextBox4 için renk değişimi
            richTextBox4.ForeColor = Color.Black;
            richTextBox4.BackColor = Color.White;
            // richTextBox2 için renk değişimi
            richTextBox2.ForeColor = Color.Black;
            richTextBox2.BackColor = Color.White;

            // richTextBox3 için renk değişimi
            richTextBox3.ForeColor = Color.Black;
            richTextBox3.BackColor = Color.White;


            notifyIcon2.BalloonTipClicked += NotifyIcon2_BalloonTipClicked;
            MetroFramework.Components.MetroStyleManager styleManager = new MetroFramework.Components.MetroStyleManager();
            styleManager.Owner = this;
            styleManager.Style = MetroFramework.MetroColorStyle.Purple;
            styleManager.Theme = MetroFramework.MetroThemeStyle.Default;
            LoadSettings();
            ChangeLanguage(userLanguage);
            disneyAdet.Location = new System.Drawing.Point(metroLabel2.Right, disneyAdet.Top);
            metroTextBox1.Location = new System.Drawing.Point(metroLabel3.Right, metroTextBox1.Top);
            comboBox2.Items.AddRange(new object[] { "Light", "Dark" });
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }
        private string downloadLocationText, downloadText, folderNotFoundText, howManyText, movieText, playlistText, processCompleteText, saveLocationText, seriesText, shutdownPCText, startText, openText, error, selectFolderText, downloads, noUpdate;
        private string alreadyUpdatedText, ytdlpUpdate, updatingPleaseWait, updateError, updateCompleted, updateCompletedMessage, selectDownloadLocationText, inputCorrectint, movieseriesaudioformat, movieseriesvideoformat, movieserieskeyformat;
        private string moviename, movieyear, seriesname, seriesseason, seriesstartep, moviecompleted, episodenumber, downloadcompleteText, downloadCompleteMessage, filenamingText, enterTextMessage, downloadError, shuttingdownText, enteryoutubelinkText, doformatselectionText;
        private string autoUpdateText;
        private bool isAdminMode = false;


        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox'ın seçili öğesine göre tema ayarını değiştir
            string selectedTheme = comboBox2.SelectedItem.ToString();
            SetTheme(selectedTheme);
        }
        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked)
            {
                RequestAdministratorPrivileges();
            }
        }

        private void RequestAdministratorPrivileges()
        {
            if (!IsRunAsAdministrator())
            {
                // Display a UAC prompt to request administrative privileges
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas" // This requests elevation to run as administrator
                };

                try
                {
                    Process.Start(psi);
                    Application.Exit(); // Exit the current instance of the application
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {

            }
        }

        private static bool IsRunAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            GenericDownload();
            if (metroCheckBox1.Checked)
            {
                Process.Start("shutdown.exe", "/s /t 1");
            }
        }

        private string enteratleastone, selectdownloadlocfirst, musicDownloadErrorText, entersongpodcasturl, pleasecheckapp, songURL, song, songkey;
        private string updateNotification, newUpdateText, noUpdateMessage, noUpdateText;
        private void ChangeLanguage(string cultureCode)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (currentCulture == "tr-TR")
            {
                songkey = TurkishResources.SongKey;
                song = TurkishResources.Song;
                songURL = TurkishResources.SongUrl;
                pleasecheckapp = TurkishResources.PleaseCheckApp;
                entersongpodcasturl = TurkishResources.EnterSongPodcastUrl;
                musicDownloadErrorText = TurkishResources.MusicDownloadErrorText;
                selectdownloadlocfirst = TurkishResources.SelectDownloadLocFirst;
                enteratleastone = TurkishResources.EnterAtLeastOne;
                filenamingText = TurkishResources.FileNamingText;
                enterTextMessage = TurkishResources.EnterTextMessage;
                downloadError = TurkishResources.DownloadError;
                openText = TurkishResources.OpenText;
                folderNotFoundText = TurkishResources.FolderNotFoundText;
                downloadLocationText = TurkishResources.DownloadLocationText;
                downloadText = TurkishResources.DownloadText;
                howManyText = TurkishResources.HowManyText;
                movieText = TurkishResources.MovieText;
                playlistText = TurkishResources.PlaylistText;
                processCompleteText = TurkishResources.ProcessCompleteText;
                saveLocationText = TurkishResources.SaveLocationText;
                seriesText = TurkishResources.SeriesText;
                shutdownPCText = TurkishResources.ShutdownPCText;
                startText = TurkishResources.StartText;

                howManyText = TurkishResources.HowManyText;
                movieText = metroRadioButton1.Text;
                seriesText = metroRadioButton2.Text;
                metroLabel1.Text = downloadLocationText;
                metroLabel3.Text = howManyText;
                error = TurkishResources.Error;
                selectFolderText = TurkishResources.SelectFolderText;
                downloads = TurkishResources.Downloads;
                noUpdate = TurkishResources.NoUpdate;
                alreadyUpdatedText = TurkishResources.AlreadyUpdatedText;
                updatingPleaseWait = TurkishResources.UpdatingPleaseWait;
                ytdlpUpdate = TurkishResources.YtdlpUpdate;
                updateError = TurkishResources.UpdateError;
                updateCompleted = TurkishResources.UpdateCompleted;
                updateCompletedMessage = TurkishResources.UpdateCompletedMessage;
                selectDownloadLocationText = TurkishResources.SelectDownloadLocationText;
                inputCorrectint = TurkishResources.InputCorrectInt;
                movieseriesaudioformat = TurkishResources.MovieSeriesAudioFormat;
                movieseriesvideoformat = TurkishResources.MovieSeriesVideoFormat;
                movieserieskeyformat = TurkishResources.MovieSeriesKeyFormat;
                moviename = TurkishResources.MovieName;
                movieyear = TurkishResources.MovieYear;
                seriesname = TurkishResources.SeriesName;
                seriesseason = TurkishResources.SeriesSeason;
                seriesstartep = TurkishResources.SeriesStartEp;
                moviecompleted = TurkishResources.MovieCompleted;
                episodenumber = TurkishResources.EpisodeNumber;
                downloadcompleteText = TurkishResources.DownloadCompleteText;
                downloadCompleteMessage = TurkishResources.DownloadCompleteMessage;
                shuttingdownText = TurkishResources.ShuttingDownText;
                enteryoutubelinkText = TurkishResources.EnterYoutubeLinkText;
                doformatselectionText = TurkishResources.DoFormatSelectionText;

                button1.Text = downloadText;
                metroCheckBox1.Text = shutdownPCText;
                autoUpdateText = TurkishResources.YtdlpAutoUpdateText;
                metroLabel5.Text = autoUpdateText;
                richTextBox1.Text = TurkishResources.RichTextBox1Text;
                metroLabel2.Text = howManyText;
                metroLabel3.Text = howManyText;
                button2.Text = "İndir";
                metroButton3.Text = openText;
                metroButton4.Text = openText;
                metroLabel4.Text = downloadLocationText;
                metroButton2.Text = "İndir";
                metroButton1.Text = startText;
                metroCheckBox2.Text = playlistText;
            }
            else if (currentCulture == "en-US")
            {
                updateNotification = "Update Avilable";
                newUpdateText = "New Version:";
                noUpdateMessage = "No Update";
                noUpdateText = "You're Using Lastest Version.";
                metroTabPage2.Text = "Generic";
                metroTabPage5.Text = "Help";
                metroTabPage4.Text = "Settings";
                songkey = EnglishResources.SongKey;
                song = EnglishResources.Song;
                songURL = EnglishResources.SongUrl;
                pleasecheckapp = EnglishResources.PleaseCheckApp;
                entersongpodcasturl = EnglishResources.EnterSongPodcastUrl;
                musicDownloadErrorText = EnglishResources.MusicDownloadErrorText;
                selectdownloadlocfirst = EnglishResources.SelectDownloadLocFirst;
                enteratleastone = EnglishResources.EnterAtLeastOne;
                filenamingText = EnglishResources.FileNamingText;
                enterTextMessage = EnglishResources.EnterTextMessage;
                downloadError = EnglishResources.DownloadError;
                openText = EnglishResources.OpenText;
                folderNotFoundText = EnglishResources.FolderNotFoundText;
                button1.Text = EnglishResources.DownloadText;
                downloadLocationText = EnglishResources.DownloadLocationText;
                downloadText = EnglishResources.DownloadText;
                howManyText = EnglishResources.HowManyText;
                movieText = EnglishResources.MovieText;
                playlistText = EnglishResources.PlaylistText;
                processCompleteText = EnglishResources.ProcessCompleteText;
                saveLocationText = EnglishResources.SaveLocationText;
                seriesText = EnglishResources.SeriesText;
                shutdownPCText = EnglishResources.ShutdownPCText;
                startText = EnglishResources.StartText;

                metroButton1.Text = startText;
                howManyText = EnglishResources.HowManyText;
                metroRadioButton1.Text = movieText;
                metroRadioButton2.Text = seriesText;
                downloadText = metroButton2.Text;
                metroLabel1.Text = downloadLocationText;
                metroLabel3.Text = howManyText;
                error = EnglishResources.Error;
                selectFolderText = EnglishResources.SelectFolderText;
                downloads = EnglishResources.Downloads;
                noUpdate = EnglishResources.NoUpdate;
                alreadyUpdatedText = EnglishResources.AlreadyUpdatedText;
                updatingPleaseWait = EnglishResources.UpdatingPleaseWait;
                ytdlpUpdate = EnglishResources.YtdlpUpdate;
                updateError = EnglishResources.UpdateError;
                updateCompleted = EnglishResources.UpdateCompleted;
                updateCompletedMessage = EnglishResources.UpdateCompletedMessage;
                selectDownloadLocationText = EnglishResources.SelectDownloadLocationText;
                inputCorrectint = EnglishResources.InputCorrectInt;
                movieseriesaudioformat = EnglishResources.MovieSeriesAudioFormat;
                movieseriesvideoformat = EnglishResources.MovieSeriesVideoFormat;
                movieserieskeyformat = EnglishResources.MovieSeriesKeyFormat;
                moviename = EnglishResources.MovieName;
                movieyear = EnglishResources.MovieYear;
                seriesname = EnglishResources.SeriesName;
                seriesseason = EnglishResources.SeriesSeason;
                seriesstartep = EnglishResources.SeriesStartEp;
                moviecompleted = EnglishResources.MovieCompleted;
                episodenumber = EnglishResources.EpisodeNumber;
                downloadcompleteText = EnglishResources.DownloadCompleteText;
                downloadCompleteMessage = EnglishResources.DownloadCompleteMessage;
                shuttingdownText = EnglishResources.ShuttingDownText;
                enteryoutubelinkText = EnglishResources.EnterYoutubeLinkText;
                doformatselectionText = EnglishResources.DoFormatSelectionText;

                metroCheckBox1.Text = shutdownPCText;
                autoUpdateText = EnglishResources.YtdlpAutoUpdateText;
                metroLabel5.Text = autoUpdateText;
                richTextBox1.Text = EnglishResources.RichTextBox1Text;
                metroLabel2.Text = howManyText;
                metroLabel3.Text = howManyText;
                metroButton2.Text = "Download";
                button2.Text = "Download";
                metroButton3.Text = openText;
                metroButton4.Text = openText;
                metroLabel4.Text = downloadLocationText;
                metroButton2.Text = "Download";
                metroCheckBox2.Text = playlistText;
            }
            else
            {
                metroTabPage2.Text = "Generic";
                metroTabPage5.Text = "Help";
                metroTabPage4.Text = "Settings";
                songkey = EnglishResources.SongKey;
                song = EnglishResources.Song;
                songURL = EnglishResources.SongUrl;
                pleasecheckapp = EnglishResources.PleaseCheckApp;
                entersongpodcasturl = EnglishResources.EnterSongPodcastUrl;
                musicDownloadErrorText = EnglishResources.MusicDownloadErrorText;
                selectdownloadlocfirst = EnglishResources.SelectDownloadLocFirst;
                enteratleastone = EnglishResources.EnterAtLeastOne;
                filenamingText = EnglishResources.FileNamingText;
                enterTextMessage = EnglishResources.EnterTextMessage;
                downloadError = EnglishResources.DownloadError;
                openText = EnglishResources.OpenText;
                folderNotFoundText = EnglishResources.FolderNotFoundText;
                button1.Text = EnglishResources.DownloadText;
                downloadLocationText = EnglishResources.DownloadLocationText;
                downloadText = EnglishResources.DownloadText;
                howManyText = EnglishResources.HowManyText;
                movieText = EnglishResources.MovieText;
                playlistText = EnglishResources.PlaylistText;
                processCompleteText = EnglishResources.ProcessCompleteText;
                saveLocationText = EnglishResources.SaveLocationText;
                seriesText = EnglishResources.SeriesText;
                shutdownPCText = EnglishResources.ShutdownPCText;
                startText = EnglishResources.StartText;

                metroButton1.Text = startText;
                howManyText = EnglishResources.HowManyText;
                metroRadioButton1.Text = "Movie";
                metroRadioButton2.Text = "Series";
                downloadText = metroButton2.Text;
                metroLabel1.Text = downloadLocationText;
                metroLabel3.Text = howManyText;
                error = EnglishResources.Error;
                selectFolderText = EnglishResources.SelectFolderText;
                downloads = EnglishResources.Downloads;
                noUpdate = EnglishResources.NoUpdate;
                alreadyUpdatedText = EnglishResources.AlreadyUpdatedText;
                updatingPleaseWait = EnglishResources.UpdatingPleaseWait;
                ytdlpUpdate = EnglishResources.YtdlpUpdate;
                updateError = EnglishResources.UpdateError;
                updateCompleted = EnglishResources.UpdateCompleted;
                updateCompletedMessage = EnglishResources.UpdateCompletedMessage;
                selectDownloadLocationText = EnglishResources.SelectDownloadLocationText;
                inputCorrectint = EnglishResources.InputCorrectInt;
                movieseriesaudioformat = EnglishResources.MovieSeriesAudioFormat;
                movieseriesvideoformat = EnglishResources.MovieSeriesVideoFormat;
                movieserieskeyformat = EnglishResources.MovieSeriesKeyFormat;
                moviename = EnglishResources.MovieName;
                movieyear = EnglishResources.MovieYear;
                seriesname = EnglishResources.SeriesName;
                seriesseason = EnglishResources.SeriesSeason;
                seriesstartep = EnglishResources.SeriesStartEp;
                moviecompleted = EnglishResources.MovieCompleted;
                episodenumber = EnglishResources.EpisodeNumber;
                downloadcompleteText = EnglishResources.DownloadCompleteText;
                downloadCompleteMessage = EnglishResources.DownloadCompleteMessage;
                shuttingdownText = EnglishResources.ShuttingDownText;
                enteryoutubelinkText = EnglishResources.EnterYoutubeLinkText;
                doformatselectionText = EnglishResources.DoFormatSelectionText;

                metroCheckBox1.Text = shutdownPCText;
                autoUpdateText = EnglishResources.YtdlpAutoUpdateText;
                metroLabel5.Text = autoUpdateText;
                richTextBox1.Text = EnglishResources.RichTextBox1Text;
                metroLabel2.Text = howManyText;
                metroLabel3.Text = howManyText;
                metroButton2.Text = "Download";
                button2.Text = "Download";
                metroButton3.Text = openText;
                metroButton4.Text = openText;
                metroLabel4.Text = downloadLocationText;
                metroButton2.Text = "Download";
                metroCheckBox2.Text = playlistText;
            }


        }

        private void NotifyIcon2_BalloonTipClicked(object sender, EventArgs e)
        {
            // Open the download folder using the default file explorer
            if (Directory.Exists(downloadDir2))
            {
                Process.Start("explorer.exe", downloadDir2);
            }
            else
            {
                MessageBox.Show($"{folderNotFoundText}", error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowNotification(string title, string content)
        {
            notifyIcon1.Icon = Icon;
            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = content;
            notifyIcon1.ShowBalloonTip(1000); // Bildirimi 1 saniye boyunca göster
        }

        private void ProcessNotification(string title2, string content2)
        {
            notifyIcon2.BalloonTipTitle = title2;
            notifyIcon2.BalloonTipText = content2;
            notifyIcon2.ShowBalloonTip(3000);
        }
        private bool isUpdateRequested = false;
        private string ffmpegPath = ""; // ffmpeg.exe konumunu tutacak değişken

        private void directory_button_Click(object sender, EventArgs e)
        {
            var folderBrowser = new BetterFolderBrowser
            {
                Title = "Klasör Seç",
                Multiselect = false
            };
            this.TopMost = false;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowser.SelectedPath;

                saveDirectory.Text = selectedFolderPath;
                downloadDir = selectedFolderPath;
                downloadDir2 = Path.Combine(downloadDir, downloads);

                if (!Directory.Exists(downloadDir2))
                {
                    Directory.CreateDirectory(downloadDir2);
                }
            }
            this.TopMost = true;

        }


        private async Task UpdateYtDlpAsync()
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
        private async void Form1_Load(object sender, EventArgs e)
        {
            metroTabControl1.SelectedTab = metroTabPage6;
            string systemCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
            GitHubUpdater.CheckForUpdates(systemCulture);
            if (IsRunAsAdministrator())
            {
                metroCheckBox1.Checked = true;
                metroCheckBox1.Enabled = false;
            }
            if (metroToggle1.Checked)
            {
                // Form yüklenmişse ve otomatik güncelleme isteği yapılmışsa güncelleme işlemini başlat
                if (IsHandleCreated)
                {
                    await UpdateYtDlpAsync();
                }
                else
                {
                    // Form henüz yüklenmemişse, güncelleme isteği yapılmış durumda olduğunu işaretle
                    isUpdateRequested = true;
                }
            }
            else
            {
                isUpdateRequested = false;
            }

            ffmpegPath = Path.Combine(Application.StartupPath, "ffmpeg.exe");
            comboBox1.Hide();

        }

        private void SetTheme(string theme)
        {
            if (theme == "Light")
            {
                // richTextBox1 için renk değişimi
                richTextBox1.ForeColor = Color.Black;
                richTextBox1.BackColor = Color.White;

                // richTextBox4 için renk değişimi
                richTextBox4.ForeColor = Color.Black;
                richTextBox4.BackColor = Color.White;
                // richTextBox2 için renk değişimi
                richTextBox2.ForeColor = Color.Black;
                richTextBox2.BackColor = Color.White;

                // richTextBox3 için renk değişimi
                richTextBox3.ForeColor = Color.Black;
                richTextBox3.BackColor = Color.White;


                comboBox2.ForeColor = Color.Black;
                comboBox2.BackColor = Color.White;

                comboBox1.ForeColor = Color.Black;
                comboBox1.BackColor = Color.White;

                this.Theme = MetroThemeStyle.Light;
                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple; 
            }
            else if (theme == "Dark")
            {
                richTextBox1.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox1.BackColor = Color.FromArgb(17, 17, 17);

                richTextBox4.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox4.BackColor = Color.FromArgb(17, 17, 17);
                richTextBox3.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox3.BackColor = Color.FromArgb(17, 17, 17);

                richTextBox2.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox2.BackColor = Color.FromArgb(17, 17, 17);

                comboBox2.ForeColor = Color.FromArgb(153, 153, 153);
                comboBox2.BackColor = Color.FromArgb(17, 17, 17);

                comboBox1.ForeColor = Color.FromArgb(153, 153, 153);
                comboBox1.BackColor = Color.FromArgb(17, 17, 17);

                this.Theme = MetroThemeStyle.Dark;
                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple;
            }


        }

        private void SaveSettings()
        {
            // İndirme konumu ve toggle ayarlarını kaydet
            Properties.Settings.Default.DownloadDirectory = saveDirectory.Text;
            Properties.Settings.Default.DownDir = downloadDir;
            Properties.Settings.Default.saveDir = downloadDir2;
            Properties.Settings.Default.AutoUpdateEnabled = metroToggle1.Checked;

            // Ayarları kaydet
            Properties.Settings.Default.Save();
        }

        

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(selectDownloadLocationText, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int numEpisodes;
                if (!int.TryParse(disneyAdet.Text, out numEpisodes))
                {
                    MessageBox.Show(inputCorrectint);
                    return;
                }

                List<string> audioLinks = new List<string>();
                List<string> videoLinks = new List<string>();
                List<string> keys = new List<string>();

                for (int i = 0; i < numEpisodes; i++)
                {
                    string audioLink = Interaction.InputBox($"{i + 1}{movieseriesaudioformat} ", $"Episode {i + 1} - Audio Link", "");
                    string videoLink = Interaction.InputBox($"{i + 1}{movieseriesvideoformat} ", $"Episode {i + 1} - Video Link", "");
                    string key = Interaction.InputBox($"{i + 1}{movieserieskeyformat} {i + 1} - Key", "");

                    audioLinks.Add(audioLink);
                    videoLinks.Add(videoLink);
                    keys.Add(key);
                }

                string outputFormat = "";
                int startEpisodeNumber = 0;
                if (metroRadioButton1.Checked)
                {
                    string filmName = Interaction.InputBox($"{moviename}", "Film Adı", "");
                    string filmYear = Interaction.InputBox($"{movieyear}", "Film Yılı", "");

                    outputFormat = $"{filmName} ({filmYear})";
                }
                else if (metroRadioButton2.Checked)
                {
                    string seriesName = Interaction.InputBox($"{seriesname}", "Dizi Adı", "");
                    string seasonNumber = Interaction.InputBox($"{seriesseason}", "Sezon Numarası", "");
                    string startEpisode = Interaction.InputBox($"{seriesstartep}", "Başlangıç Bölümü", "");

                    if (!int.TryParse(startEpisode, out startEpisodeNumber))
                    {
                        MessageBox.Show(inputCorrectint);
                        return;
                    }

                    outputFormat = $"{seriesName} S{seasonNumber}";
                }

                for (int i = 0; i < numEpisodes; i++)
                {
                    string audioUrl = audioLinks[i];
                    string videoUrl = videoLinks[i];
                    string key = keys[i];

                    Process.Start(".\\m3u8dl.exe", $"--saveName {i}s --workDir \"{downloadDir2}\" \"{audioUrl}\"").WaitForExit();
                    Process.Start(".\\m3u8dl.exe", $"--saveName {i}v --workDir \"{downloadDir2}\" \"{videoUrl}\"").WaitForExit();
                    Process.Start("mp4decrypt.exe", $"--key {key} \"{downloadDir2}\\{i}v.mp4\" \"{downloadDir2}\\out{i}.mp4\"").WaitForExit();

                    string audioFile = Path.Combine(downloadDir2, $"{i}s.m4a");
                    string videoFile = Path.Combine(downloadDir2, $"out{i}.mp4");

                    string outputFile;

                    if (metroRadioButton2.Checked)
                    {
                        outputFile = Path.Combine(downloadDir, $"{outputFormat} E{startEpisodeNumber + i:D2}.mkv");
                    }
                    else
                    {
                        outputFile = Path.Combine(downloadDir, $"{outputFormat}.mkv");

                    }

                    Process.Start("mkvmerge", $"-o \"{outputFile}\" \"{audioFile}\" \"{videoFile}\"").WaitForExit();
                    if (metroRadioButton1.Checked)
                    {
                        richTextBox3.AppendText($"{i + 1}{moviecompleted}" + Environment.NewLine);
                    }
                    else
                    {
                        richTextBox3.AppendText($"{i + 1}.{seriesText} {i + 1}.{episodenumber}" + Environment.NewLine);
                    }
                }

                if (metroCheckBox1.Checked)
                {
                    richTextBox3.AppendText($"{shuttingdownText}" + Environment.NewLine);
                    Process.Start("shutdown", "/s /t 1");
                }
                else
                {
                    ProcessNotification(downloadcompleteText, processCompleteText);
                }
            }
            

        }

        private async Task DecryptAndConvertAsync(string videoPath, string key, string outputPath)
        {
            // mp4decrypt ile decrypt işlemi
            Process.Start("mp4decrypt.exe", $"--key {key} \"{videoPath}\" \"{outputPath}\"").WaitForExit();

            // ffmpeg ile MP3'e dönüştür
            string mp3FilePath = Path.ChangeExtension(outputPath, ".mp3");
            Process.Start("ffmpeg", $"-i \"{outputPath}\" -codec:a libmp3lame -q:a 2 \"{mp3FilePath}\"").WaitForExit();

            // Decrypt işlemi sonrasında artık kullanmadığımız orijinal video dosyasını silelim
            File.Delete(outputPath);
        }

        private async Task DownloadAndDecryptAsync(string songUrl, string key, int songIndex)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(songUrl);

                if (response.IsSuccessStatusCode)
                {
                    // İndirilen dosyayı sabit bir isimle kaydet
                    string downloadPath = Path.Combine(downloadDir2, "inendosya");
                    using (FileStream fs = File.OpenWrite(downloadPath))
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        await stream.CopyToAsync(fs);
                    }

                    // İnen dosyayı isimlendir
                    string userInput = Interaction.InputBox(entersongpodcasturl, filenamingText, "");
                    string fileName = $"{userInput}.mp3";

                    // mp4decrypt ve ffmpeg işlemleri
                    string decryptedFilePath = Path.Combine(downloadDir2, $"{userInput}.mp3");
                    await DecryptAndConvertAsync(downloadPath, key, decryptedFilePath);

                   ProcessNotification(downloadcompleteText, downloadCompleteMessage);
                }
                else
                {
                    ShowNotification(downloadError, pleasecheckapp);
                    MessageBox.Show($"{downloadError} {response.StatusCode}");
                }
            }
        }


        private async void metroButton2_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(selectdownloadlocfirst, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int numOfSong;
                numOfSong = int.TryParse(metroTextBox1.Text, out numOfSong) ? numOfSong : 0;
                if (numOfSong == 0)
                {
                    MessageBox.Show(enteratleastone);
                }
                else
                {
                    // Şarkıları indir, decrypt işlemi yap ve MP3'e dönüştür
                    for (int i = 0; i < numOfSong; i++)
                    {
                        string songUrl = Interaction.InputBox($"{i + 1}.{songURL} ", $"{song} {i + 1} - URL", "");
                        string key = Interaction.InputBox($"{i + 1}.{songkey}: ", $"{song} {i + 1} - Key", "");

                        await DownloadAndDecryptAsync(songUrl, key, i);
                    }
                }
            }
        }



        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
        private string GetFormatCode(string selectedQuality)
        {
            // comboBox1'den seçilen kaliteye göre format kodunu döndür
            // Bu metodun içeriği, comboBox1'deki kalite seçeneklerine göre güncellenmelidir.
            switch (selectedQuality)
            {
                case "144p":
                    return "144";
                case "240p":
                    return "240";
                case "360p":
                    return "360";
                case "480p":
                    return "480";
                case "720p":
                    return "720";
                case "1080p":
                    return "1080";
                case "1440p":
                    return "1440";
                default:
                    return "best";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(selectDownloadLocationText, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (metroCheckBox2.Checked == true)
                {
                    if (metroRadioButton3.Checked)
                    {
                        // mp3 olarak indir
                        DownloadAudioPlaylist();
                    }
                    else if (metroRadioButton4.Checked)
                    {
                        // video ve ses olarak indir
                        DownloadVideoAndAudioPlaylist();
                    }
                    else
                    {
                        MessageBox.Show(doformatselectionText);
                    }
                }
                else
                {
                    if (metroRadioButton3.Checked)
                    {
                        // mp3 olarak indir
                        DownloadAudio();
                    }
                    else if (metroRadioButton4.Checked)
                    {
                        // video ve ses olarak indir
                        DownloadVideoAndAudio();
                    }
                    else
                    {
                        MessageBox.Show(doformatselectionText);
                    }
                }
                
            }
            
        }
        private async void DownloadAudio()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                MessageBox.Show(enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Ses dosyalarını downloadDir2 klasörüne kaydet

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\%(title)s.%(ext)s\" --no-playlist --format bestaudio --extract-audio --audio-format mp3 {youtubeLink} ";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText(e.Data + Environment.NewLine);
                        richTextBox2.SelectionStart = richTextBox2.Text.Length;
                        richTextBox2.ScrollToCaret();
                    }));
                }
            };

            process.Start();
            process.BeginOutputReadLine();

            await process.WaitForExitAsync();

            richTextBox2.Clear();
            richTextBox2.Text = processCompleteText;

            ProcessNotification(downloadcompleteText, processCompleteText);
            if (metroCheckBox1.Checked == true) { Process.Start("shutdown.exe, /s /t 1"); }
        }

        private async void DownloadVideoAndAudio()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                AlwaysOnTopMessageBox.Show(enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Video ve ses dosyalarını downloadDir2 klasörüne kaydet

            string selectedQuality = comboBox1.SelectedItem.ToString();
            string formatCode = GetFormatCode(selectedQuality);

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\%(title)s.%(ext)s\" --no-playlist -S res:{formatCode} {youtubeLink}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText(e.Data + Environment.NewLine);
                        richTextBox2.SelectionStart = richTextBox2.Text.Length;
                        richTextBox2.ScrollToCaret();
                    }));
                }
            };

            process.Start();
            process.BeginOutputReadLine();

            await process.WaitForExitAsync();

            richTextBox2.Clear();
            richTextBox2.Text = processCompleteText;

            ProcessNotification(downloadcompleteText, processCompleteText);
            if (metroCheckBox1.Checked==true) { Process.Start("shutdown.exe, /s /t 1"); }
        }
        


        private async void DownloadAudioPlaylist()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                MessageBox.Show(enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Ses dosyalarını downloadDir2 klasörüne kaydet

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\%(title)s.%(ext)s\" --yes-playlist --format bestaudio --extract-audio --audio-format mp3 {youtubeLink} ";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText(e.Data + Environment.NewLine);
                        richTextBox2.SelectionStart = richTextBox2.Text.Length;
                        richTextBox2.ScrollToCaret();
                    }));
                }
            };

            
            process.Start();
            process.BeginOutputReadLine();

            await process.WaitForExitAsync();

            richTextBox2.Clear();
            richTextBox2.Text = processCompleteText;

            ProcessNotification(downloadcompleteText, processCompleteText);
        }

        private async void DownloadVideoAndAudioPlaylist()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                AlwaysOnTopMessageBox.Show("Lütfen bir Youtube linki girin.");
                return;
            }

            string outputDir = downloadDir2; // Video ve ses dosyalarını downloadDir2 klasörüne kaydet

            string selectedQuality = comboBox1.SelectedItem.ToString();
            string formatCode = GetFormatCode(selectedQuality);

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\%(title)s.%(ext)s\" --yes-playlist --format {formatCode} {youtubeLink}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText(e.Data + Environment.NewLine);
                        richTextBox2.SelectionStart = richTextBox2.Text.Length;
                        richTextBox2.ScrollToCaret();
                    }));
                }
            };

            process.Start();
            process.BeginOutputReadLine();

            await process.WaitForExitAsync();

            richTextBox2.Clear();
            richTextBox2.Text = processCompleteText;

            ProcessNotification(downloadcompleteText, processCompleteText);
            if (metroCheckBox1.Checked == true) { Process.Start("shutdown.exe, /s /t 1"); }
        }

        private async void GenericDownload()
        {
            string genericLink = genericTextBox.Text;

            if (string.IsNullOrWhiteSpace(genericLink))
            {
                AlwaysOnTopMessageBox.Show("Lütfen bir Youtube linki girin.");
                return;
            }

            string outputDir = downloadDir2; // Video ve ses dosyalarını downloadDir2 klasörüne kaydet

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\%(title)s.%(ext)s\" {genericLink}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() =>
                    {
                        richTextBox4.AppendText(e.Data + Environment.NewLine);
                        richTextBox2.SelectionStart = richTextBox2.Text.Length;
                        richTextBox2.ScrollToCaret();
                    }));
                }
            };

            process.Start();
            process.BeginOutputReadLine();

            await process.WaitForExitAsync();

            richTextBox4.Clear();
            richTextBox4.Text = processCompleteText;

            ProcessNotification(downloadcompleteText, processCompleteText);
        }

        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Show();
        }


        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Hide();
        }

        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void OpenDownloadFolder()
        {
            if (Directory.Exists(downloadDir))
            {
                Process.Start("explorer.exe", downloadDir2);
            }
            else
            {
                MessageBox.Show(folderNotFoundText, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            OpenDownloadFolder();
        }
        private void LoadSettings()
        {

            // İndirme konumu ve toggle ayarlarını yükle
            downloadDir = Properties.Settings.Default.DownDir;
            downloadDir2 = Properties.Settings.Default.saveDir;
            saveDirectory.Text = Properties.Settings.Default.DownloadDirectory;
            metroToggle1.Checked = Properties.Settings.Default.AutoUpdateEnabled;
        }

        private void metroTabPage4_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }

}
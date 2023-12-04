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
using System.Linq;
using MediaHarbor.Classes;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static MediaHarbor.Form1;

namespace MediaHarbor
{
    
    public partial class Form1 : MetroForm
    {
        private List<string> songUrls = new List<string>();
        private string ApiKey = "YOUR_API_KEY";
        public string downloadDir;
        public string downloadDir2;
        public string downloadDir3;
        string userLanguage = CultureInfo.InstalledUICulture.Name;
        public string MP3Folder, MP4Folder;
        public string LightTheme = Properties.Resources.lightTheme;
        public string DarkTheme = Properties.Resources.darkTheme;
        public string ThemeOption;
        LanguageManager manager = new LanguageManager();
        ThemeString themeString = new ThemeString();

        public Form1()
        {
            InitializeComponent();
            manager.SetCommonTranslations();
            metroTextBox3.Text = manager.youtubeSearch;
            comboBox2.Items.Add(manager.lightTheme);
            comboBox2.Items.Add(manager.darkTheme);
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
            MP3Folder = $"\\MP3";
            MP4Folder = $"\\MP4";

            MP3Folder = $"\\MP3";
            MP4Folder = $"\\MP4";

            metroLabel9.Text = manager.appTheme;
            notifyIcon2.BalloonTipClicked += NotifyIcon2_BalloonTipClicked;
            MetroFramework.Components.MetroStyleManager styleManager = new MetroFramework.Components.MetroStyleManager();
            styleManager.Owner = this;
            styleManager.Style = MetroFramework.MetroColorStyle.Purple;
            styleManager.Theme = MetroFramework.MetroThemeStyle.Default;
            LoadSettings();
            ChangeLanguage(userLanguage);
            disneyAdet.Location = new System.Drawing.Point(metroLabel2.Right, disneyAdet.Top);
            metroTextBox1.Location = new System.Drawing.Point(metroLabel3.Right, metroTextBox1.Top);
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;


        }


        public class ThemeString
        {
            public string SelectedTheme { get; set; }
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox'ın seçili öğesine göre tema ayarını değiştir
            string selectedTheme = comboBox2.SelectedItem.ToString();
            SetTheme(selectedTheme);
            
        }
        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void searchButton_ClickAsync(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == manager.lightTheme)
            {

                ThemeOption = manager.lightTheme;

            }
            else if (comboBox2.SelectedItem == manager.darkTheme)
            {
                ThemeOption = manager.darkTheme;

            }
            ShowSearchResults();

        }
        private async Task ShowSearchResults()
        {
            string searchQuery = metroTextBox3.Text;
            var videos = await SearchYouTubeVideos(searchQuery);

            // SearchResultsForm'u aç ve arama sonuçlarını göster
            SearchResultForm searchResultForm = new SearchResultForm(videos);
            searchResultForm.ShowDialog();
        }
        int maxResults = 12;
        private async Task<List<YouTubeVideo>> SearchYouTubeVideos(string query)
        {
            List<YouTubeVideo> videos = new List<YouTubeVideo>();
            string apiUrl = $"https://www.googleapis.com/youtube/v3/search?q={Uri.EscapeDataString(query)}&key={ApiKey}&part=snippet&type=video&maxResults={maxResults}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(apiUrl);
                    dynamic result = JsonConvert.DeserializeObject(response);

                    foreach (var item in result.items)
                    {
                        string videoId = item.id.videoId;
                        string videoTitle = item.snippet.title;
                        videos.Add(new YouTubeVideo { VideoId = videoId, Title = videoTitle });
                    }
                }
                catch (HttpRequestException ex)
                {
                    // API isteği sırasında bir hata oluştuğunda
                    throw new HttpRequestException($"API Request Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Diğer genel hata durumları için
                    throw new Exception($"There was an error: {ex.Message}");
                }
            }

            return videos;
        }


        private void resultsListBox_DoubleClick(object sender, EventArgs e)
        {

        }

        private void metroTextBox3_Enter(object sender, EventArgs e)
        {
            if (metroTextBox3.Text == manager.youtubeSearch)
            {
                metroTextBox3.Text = "";
                metroTextBox3.ForeColor = SystemColors.WindowText;
            }
        }

        private void metroTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(metroTextBox3.Text))
            {
                metroTextBox3.Text = manager.youtubeSearch;
                metroTextBox3.ForeColor = SystemColors.GrayText;
            }
        }

        private void metroTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox3.Text != manager.youtubeSearch)
            {
                metroTextBox3.ForeColor = SystemColors.WindowText;
            }
        }

        private void metroTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }




        private void button2_Click(object sender, EventArgs e)
        {
            GenericDownload();

            //Düzeltilecek
            
        }

        private string enteratleastone, selectdownloadlocfirst, musicDownloadErrorText, entersongpodcasturl, pleasecheckapp, songURL, song, songkey;
        private string updateNotification, newUpdateText, noUpdateMessage, noUpdateText;
        private string downloadLocationText, downloadText, folderNotFoundText, howManyText, movieText, playlistText, processCompleteText, saveLocationText, seriesText, shutdownPCText, startText, openText, error, selectFolderText, downloads, noUpdate;
        private string alreadyUpdatedText, ytdlpUpdate, updatingPleaseWait, updateError, updateCompleted, updateCompletedMessage, selectDownloadLocationText, inputCorrectint, movieseriesaudioformat, movieseriesvideoformat, movieserieskeyformat;
        private string moviename, movieyear, seriesname, seriesseason, seriesstartep, moviecompleted, episodenumber, downloadcompleteText, downloadCompleteMessage, filenamingText, enterTextMessage, downloadError, shuttingdownText, enteryoutubelinkText, doformatselectionText;

        private string autoUpdateText;
        private bool isAdminMode = false;

        private void ChangeLanguage(string cultureCode)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            

            if (currentCulture == "tr-TR")
            {
                searchButton.Text = manager.searchText;
                metroTabPage7.Text = manager.searchText;
                metroLabel9.Text = manager.appTheme;
                button1.Text = manager.downloadText;
                metroCheckBox1.Text = manager.shutdownPCText;
                autoUpdateText = Properties.Resources.ytdlpAutoUpdateText;
                metroLabel5.Text = autoUpdateText;
                richTextBox1.Text = Properties.Resources.richTextBox1_Text;
                metroLabel2.Text = manager.howManyText;
                button2.Text = manager.downloadText;
                metroButton3.Text = manager.openText;
                metroButton4.Text = manager.openText;
                metroLabel4.Text = manager.downloadLocationText;
                metroButton2.Text = manager.downloadText;
                metroButton1.Text = manager.startText;
                metroCheckBox2.Text = manager.playlistText;
                movieText = manager.movieText;
                seriesText = manager.seriesText;
                metroLabel1.Text = manager.downloadLocationText;
                metroLabel3.Text = manager.howManyText;

                button2.Text = "İndir";
                metroButton2.Text = "İndir";
                manager.SetCommonTranslations();
            }
            else if (currentCulture == "tr-TR")
            {
                searchButton.Text = manager.searchText;
                metroLabel9.Text = manager.appTheme;
                updateNotification = "Update Avilable";
                newUpdateText = "New Version:";
                noUpdateMessage = "No Update";
                noUpdateText = "You're Using Lastest Version.";
                metroTabPage2.Text = "Generic";
                metroTabPage5.Text = "Help";
                metroTabPage4.Text = "Settings";
                button2.Text = "Download";
                metroButton2.Text = "Download";

                
                button1.Text = manager.downloadText;
                metroCheckBox1.Text = manager.shutdownPCText;
                autoUpdateText = Properties.Resources.ytdlpAutoUpdateText;
                metroLabel5.Text = autoUpdateText;
                richTextBox1.Text = Properties.Resources.richTextBox1_Text;
                metroLabel2.Text = manager.howManyText;
                button2.Text = manager.downloadText;
                metroButton3.Text = manager.openText;
                metroButton4.Text = manager.openText;
                metroLabel4.Text = manager.downloadLocationText;
                metroButton2.Text = manager.downloadText;
                metroButton1.Text = manager.startText;
                metroCheckBox2.Text = manager.playlistText;
                movieText = manager.movieText;
                seriesText = manager.seriesText;
                metroLabel1.Text = manager.downloadLocationText;
                metroLabel3.Text = manager.howManyText;

                manager.SetCommonTranslations();

                

                movieText = metroRadioButton1.Text;
                seriesText = metroRadioButton2.Text;
                metroLabel1.Text = downloadLocationText;
                metroLabel3.Text = howManyText;

                manager.SetCommonTranslations();

                

            }
            else
            {
                
                movieText = metroRadioButton1.Text;
                seriesText = metroRadioButton2.Text;
                metroLabel1.Text = downloadLocationText;
                metroLabel3.Text = howManyText;

                manager.SetCommonTranslations();
            }


        }

        private void NotifyIcon2_BalloonTipClicked(object sender, EventArgs e)
        {

        }

        private void ShowNotification(string title, string content)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = Icon;
            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = content;
            notifyIcon1.ShowBalloonTip(1000); // Bildirimi 1 saniye boyunca göster
            notifyIcon1.Visible = true;
        }

        private void ProcessNotification(string title2, string content2)
        {
            notifyIcon2.Visible = true;
            notifyIcon2.Icon = Icon;
            notifyIcon2.BalloonTipTitle = title2;
            notifyIcon2.BalloonTipText = content2;
            notifyIcon2.ShowBalloonTip(3000);
            notifyIcon2.Visible = false;
        }
        private bool isUpdateRequested = false;
        private string ffmpegPath = ""; // ffmpeg.exe konumunu tutacak değişken

        private void directory_button_Click(object sender, EventArgs e)
        {
            var folderBrowser = new BetterFolderBrowser
            {
                Title = manager.selectFolderText,
                Multiselect = false
            };
            this.TopMost = false;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowser.SelectedPath;

                saveDirectory.Text = selectedFolderPath;
                downloadDir = selectedFolderPath;
                downloadDir2 = Path.Combine(downloadDir, "MediaHarbor");
                downloadDir3 = Path.Combine(downloadDir2, "Spotify");

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
                     ShowNotification($"{manager.noUpdate}", $"{manager.alreadyUpdatedText}");
                }
                else if (output.Contains("Updating to"))
                {
                    MessageBox.Show($"{manager.updatingPleaseWait}", $"{manager.ytdlpUpdate}");
                    ShowNotification($"{manager.ytdlpUpdate}", $"{manager.updatingPleaseWait}");
                }
                else
                {
                    MessageBox.Show($"{manager.updateError}");
                }
                if (output.Contains("Updated yt-dlp to"))
                {
                    ShowNotification($"{manager.updateCompleted}", $"{manager.updateCompletedMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{manager.updateError}: {ex.Message}");
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            
            
            MP3Folder = Path.Combine(downloadDir, "MediaHarbor");

            MP4Folder = Path.Combine(downloadDir, "MediaHarbor");
            metroTabControl1.SelectedTab = metroTabPage6;
            string systemCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
            GitHubUpdater.CheckForUpdates(systemCulture);
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
        

        public void SetTheme(string theme)
        {
            

            if (theme == manager.lightTheme)
            {
                ThemeOption = LightTheme;
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

                richTextBox5.ForeColor = Color.Black;
                richTextBox5.BackColor = Color.White;

                comboBox2.ForeColor = Color.Black;
                comboBox2.BackColor = Color.White;

                comboBox1.ForeColor = Color.Black;
                comboBox1.BackColor = Color.White;

                this.Theme = MetroThemeStyle.Light;
                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple;
            }
            else if (theme == manager.darkTheme)
            {
                ThemeOption = DarkTheme;
                richTextBox1.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox1.BackColor = Color.FromArgb(17, 17, 17);

                richTextBox4.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox4.BackColor = Color.FromArgb(17, 17, 17);
                richTextBox3.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox3.BackColor = Color.FromArgb(17, 17, 17);

                richTextBox2.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox2.BackColor = Color.FromArgb(17, 17, 17);

                richTextBox5.ForeColor = Color.FromArgb(153, 153, 153);
                richTextBox5.BackColor = Color.FromArgb(17, 17, 17);

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

            if (comboBox2.SelectedItem != null)
            {
                Properties.Settings.Default.theme = comboBox2.SelectedItem.ToString();
            }
            // Ayarları kaydet
            Properties.Settings.Default.Save();
        }



        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(manager.selectDownloadLocationText, manager.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int numEpisodes;
                if (!int.TryParse(disneyAdet.Text, out numEpisodes))
                {
                    MessageBox.Show(manager.inputCorrectint);
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
                        richTextBox3.AppendText($"{i + 1}{manager.moviecompleted}" + Environment.NewLine);
                    }
                    else
                    {
                        richTextBox3.AppendText($"{i + 1}.{manager.seriesText} {i + 1}.{manager.episodenumber}" + Environment.NewLine);
                    }
                }

                if (metroCheckBox1.Checked)
                {
                    richTextBox3.AppendText($"{manager.shuttingdownText}" + Environment.NewLine);
                    Process.Start("shutdown", "/s /t 1");
                }
                else
                {
                    ProcessNotification(manager.downloadcompleteText, manager.processCompleteText);
                }
            }


        }

        private void DecryptAndConvert(string videoPath, string key, string outputPath)
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
                    string userInput = Interaction.InputBox(manager.entersongpodcasturl, manager.filenamingText, "");
                    string fileName = $"{userInput}.mp3";

                    // mp4decrypt ve ffmpeg işlemleri
                    string decryptedFilePath = Path.Combine(downloadDir2, $"{userInput}.mp3");
                    DecryptAndConvert(downloadPath, key, decryptedFilePath);

                    ProcessNotification(manager.downloadcompleteText, manager.downloadCompleteMessage);
                }
                else
                {
                    ShowNotification(manager.downloadError, manager.pleasecheckapp);
                    MessageBox.Show($"{manager.downloadError} {response.StatusCode}");
                }
            }
        }


        private async void metroButton2_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(manager.selectdownloadlocfirst, manager.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int numOfSong;
                numOfSong = int.TryParse(metroTextBox1.Text, out numOfSong) ? numOfSong : 0;
                if (numOfSong == 0)
                {
                    MessageBox.Show(manager.enteratleastone);
                }
                else
                {
                    //Using SpotdL
                    for (int i = 0; i < numOfSong; i++)
                    {
                        string songUrl = Interaction.InputBox($"{i + 1}. {song} {i + 1} - URL", $"{song} {i + 1} - URL", "");
                        songUrls.Add(songUrl);
                    }
                    if (songUrls.Count > 0)
                    {
                        foreach (var songUrl in songUrls)
                        {
                            spotDlDownloadAsync(songUrl);
                            // İndirme tamamlandı mesajı ya da başka bir işlem yapabilirsiniz.
                        }
                    }
                    else
                    {
                        MessageBox.Show("En az bir URL eklemelisiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Hata mesajı ya da başka bir işlem yapabilirsiniz.
                    }
                    // Şarkıları indir, decrypt işlemi yap ve MP3'e dönüştür
                    //for (int i = 0; i < numOfSong; i++)
                    //{
                    //                    string songUrl = Interaction.InputBox($"{i + 1}.{songURL} ", $"{song} {i + 1} - URL", "");
                    //                        string key = Interaction.InputBox($"{i + 1}.{songkey}: ", $"{song} {i + 1} - Key", "");

                    //                      await DownloadAndDecryptAsync(songUrl, key, i);
                    //}
                }
            }
        }



        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
        private string GetFormatCode(string selectedQuality)
        {
            try
            {
                if (selectedQuality == null)
                {
                    throw new ArgumentNullException(nameof(selectedQuality), "Kalite seçeneği null olamaz.");
                }

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
            catch (Exception ex)
            {
                // Exception'ı logla, kullanıcıya bildir, vb.
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                return string.Empty; // veya başka bir varsayılan değer
            }

            // comboBox1'den seçilen kaliteye göre format kodunu döndür
            // Bu metodun içeriği, comboBox1'deki kalite seçeneklerine göre güncellenmelidir.


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveDirectory.Text == "")
            {
                MessageBox.Show(manager.selectDownloadLocationText, manager.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!Directory.Exists(MP3Folder))
                {
                    Directory.CreateDirectory(downloadDir2+MP3Folder);
                }
                else if (!Directory.Exists(MP4Folder))
                {
                    Directory.CreateDirectory(downloadDir2+MP4Folder);
                }
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
                        MessageBox.Show(manager.doformatselectionText);
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
                        MessageBox.Show(manager.doformatselectionText);
                    }
                }

            }

        }
        private async void DownloadAudio()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                MessageBox.Show(manager.enteryoutubelinkText);
                return;
            }

            string outputDir = MP3Folder; // Ses dosyalarını downloadDir2 klasörüne kaydet

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\MP3\\%(title)s.%(ext)s\" --no-playlist --format bestaudio --extract-audio --audio-format mp3 {youtubeLink} ";
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
            richTextBox2.Text = manager.processCompleteText;


            if (metroCheckBox1.Checked)
            {
                Process.Start("shutdown", "/s /f /t 0");
            }
            else
            {
                ProcessNotification(manager.downloadcompleteText, manager.processCompleteText);
            }
        }

        private async void DownloadVideoAndAudio()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                AlwaysOnTopMessageBox.Show(manager.enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Video ve ses dosyalarını downloadDir2 klasörüne kaydet


            string selectedQuality = comboBox1.SelectedItem?.ToString();
            if (selectedQuality == null)
            {
                // selectedQuality null ise kullanıcıya bildir
                AlwaysOnTopMessageBox.Show(manager.selectQuality);
                return;
            }

            string formatCode = GetFormatCode(selectedQuality);

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\MP4\\%(title)s.%(ext)s\" --no-playlist -S res:{formatCode} {youtubeLink}";
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
            richTextBox2.Text = manager.processCompleteText;

            // Convert the last downloaded video file to MP4
            ConvertLastDownloadedToMp4(outputDir);

            if (metroCheckBox1.Checked)
            {
                Process.Start("shutdown", "/s /f /t 0");
            }
            else
            {
                ProcessNotification(manager.downloadcompleteText, manager.processCompleteText);
            }
        }

        private void ConvertLastDownloadedToMp4(string outputDir)
        {
            // Get the list of all files in the output directory, ordered by creation time
            var files = new DirectoryInfo(outputDir).GetFiles()
                .OrderByDescending(f => f.CreationTime)
                .ToList();

            // Find the first video file
            var videoFile = files.FirstOrDefault();

            if (videoFile != null && !IsMp4File(videoFile))
            {
                // Construct the output file path with the MP4 extension
                string mp4FilePath = Path.Combine(outputDir, Path.ChangeExtension(videoFile.Name, "mp4"));

                // Run ffmpeg to convert the file to MP4
                Process ffmpegProcess = new Process();
                ffmpegProcess.StartInfo.FileName = "ffmpeg.exe"; // Make sure ffmpeg is in your PATH or provide the full path
                ffmpegProcess.StartInfo.Arguments = $"-i \"{videoFile.FullName}\" -c:v libx264 -c:a aac \"{mp4FilePath}\"";
                ffmpegProcess.StartInfo.UseShellExecute = false;
                ffmpegProcess.StartInfo.RedirectStandardOutput = true;
                ffmpegProcess.StartInfo.CreateNoWindow = true;

                ffmpegProcess.Start();
                ffmpegProcess.WaitForExit();

                // Delete the original file after conversion
                videoFile.Delete();
            }
        }

        private bool IsMp4File(FileInfo file)
        {
            // Check if the file has an MP4 extension
            return string.Equals(file.Extension, ".mp4", StringComparison.OrdinalIgnoreCase);
        }



        private async void DownloadAudioPlaylist()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                MessageBox.Show(manager.enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Ses dosyalarını downloadDir2 klasörüne kaydet

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\MP3\\%(playlist_title)s\\%(title)s.%(ext)s\" --yes-playlist --format bestaudio --extract-audio --audio-format mp3 {youtubeLink} ";
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
            richTextBox2.Text = manager.processCompleteText;

            if (metroCheckBox1.Checked)
            {
                Process.Start("shutdown", "/s /f /t 0");
            }
            else
            {
                ProcessNotification(manager.downloadcompleteText, manager.processCompleteText);
            }
        }

        private async void DownloadVideoAndAudioPlaylist()
        {
            string youtubeLink = metroTextBox2.Text;

            if (string.IsNullOrWhiteSpace(youtubeLink))
            {
                AlwaysOnTopMessageBox.Show(manager.enteryoutubelinkText);
                return;
            }

            string outputDir = downloadDir2; // Video ve ses dosyalarını downloadDir2 klasörüne kaydet

            string selectedQuality = comboBox1.SelectedItem.ToString();
            string formatCode = GetFormatCode(selectedQuality);

            Process process = new Process();
            process.StartInfo.FileName = ".\\yt-dlp.exe";
            process.StartInfo.Arguments = $"--ffmpeg-location \"{ffmpegPath}\" -o \"{outputDir}\\MP4\\%(playlist_title)s\\%(title)s.%(ext)s\" --yes-playlist -S res:{formatCode} {youtubeLink}";
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
            richTextBox2.Text = manager.processCompleteText;

            // Convert the last downloaded playlist videos to MP4
            ConvertAllPlaylistVideosToMp4(outputDir);

            if (metroCheckBox1.Checked)
            {
                Process.Start("shutdown", "/s /f /t 0");
            }
            else
            {
                ProcessNotification(manager.downloadcompleteText, manager.processCompleteText);
            }
        }

        private void ConvertAllPlaylistVideosToMp4(string outputDir)
        {
            // Get the list of all playlists in the output directory
            var playlists = new DirectoryInfo(outputDir).GetDirectories();

            // Iterate through each playlist
            foreach (var playlist in playlists)
            {
                // Get the list of all files in the playlist directory
                var files = playlist.GetFiles();

                // Iterate through each file in the playlist
                foreach (var file in files)
                {
                    // Check if the file is a video file and not already in MP4 format
                    if (IsVideoFile(file) && !IsMp4File(file))
                    {
                        // Construct the output file path with the MP4 extension
                        string mp4FilePath = Path.Combine(playlist.FullName, Path.ChangeExtension(file.Name, "mp4"));

                        // Run ffmpeg to convert the file to MP4
                        Process ffmpegProcess = new Process();
                        ffmpegProcess.StartInfo.FileName = "ffmpeg.exe"; // Make sure ffmpeg is in your PATH or provide the full path
                        ffmpegProcess.StartInfo.Arguments = $"-i \"{file.FullName}\" -c:v libx264 -c:a aac \"{mp4FilePath}\"";
                        ffmpegProcess.StartInfo.UseShellExecute = false;
                        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
                        ffmpegProcess.StartInfo.CreateNoWindow = true;

                        ffmpegProcess.Start();
                        ffmpegProcess.WaitForExit();

                        // Delete the original file after conversion
                        file.Delete();
                    }
                }
            }
        }

        private bool IsVideoFile(FileInfo file)
        {
            // Check if the file has a video extension
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".wmv", ".mov", ".3gp", ".webm" };
            string extension = file.Extension;

            return videoExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
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
            richTextBox4.Text = manager.processCompleteText;

            
            
        }
        private void spotDlDownloadAsync(string songUrl)
        {
            try
            {

                // spotdl komutunu oluştur
                string spotDlCommand = $"\"{songUrl}\" --bitrate 320k --format mp3";

                // Process oluştur ve başlat
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = downloadDir2,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.StartInfo = startInfo;
                process.StartInfo.FileName = ".\\spotdl-4.2.4-win32.exe";
                process.StartInfo.Arguments = spotDlCommand;

                // Progress bar güncelleme olayını ekle
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        this.Invoke(new Action(() =>
                        {
                            richTextBox5.AppendText(e.Data + Environment.NewLine);
                            richTextBox5.SelectionStart = richTextBox5.Text.Length;
                            richTextBox5.ScrollToCaret();

                        }));
                    }
                };

                process.Start();
                process.BeginOutputReadLine();



                // Bekleme işlemi tamamlandıktan sonra kontrolü bırak
                process.WaitForExitAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
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
                MessageBox.Show(manager.folderNotFoundText, manager.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            comboBox2.SelectedItem = Properties.Settings.Default.theme;
        }

        private void metroTabPage4_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }
        
    }



}
using MediaHarbor.Classes;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MediaHarbor.Form1;

namespace MediaHarbor
{
    public partial class SearchResultForm : MetroForm
    {
        LanguageManager manager = new LanguageManager();
        ThemeString themes = new ThemeString();
        Form1 form1Instance = new Form1();
        public SearchResultForm(List<YouTubeVideo> videos)
        {
            string selectedtheme = form1Instance.ThemeOption;
            InitializeComponent();
            ChangeLanguage();
            this.Name = manager.youtubeSearch;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.AutoScroll = true; // Kaydırma özelliğini etkinleştir
            PopulateResults(videos);
            SetTheme(selectedtheme);
        }
        public void SetTheme(string theme)
        {
            if (theme == manager.lightTheme)
            {
                // richTextBox1 için renk değişimi
                this.Theme = MetroThemeStyle.Light;
                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple;
            }
            else if (theme == manager.darkTheme)
            {

                this.Theme = MetroThemeStyle.Dark;
                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroStyleManager1.Style = MetroFramework.MetroColorStyle.Purple;
            }


        }

        private void ChangeLanguage() 
        {
            manager.SetCommonTranslations();
            

        }
        private void PopulateResults(List<YouTubeVideo> videos)
        {
            foreach (var video in videos)
            {
                AddResult(video);
            }
        }
      
        private void AddResult(YouTubeVideo video)
        {
            Form1 form1Instance = new Form1();
            string selectedtheme = form1Instance.ThemeOption;
            Panel resultPanel = new Panel();
            resultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resultPanel.Margin = new Padding(5);
            resultPanel.AutoSize = false;
            resultPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width - 10, 120);

            PictureBox thumbnailBox = new PictureBox();
            thumbnailBox.SizeMode = PictureBoxSizeMode.StretchImage;
            thumbnailBox.Size = new Size(120, 90);
            thumbnailBox.ImageLocation = GetThumbnailUrl(video.VideoId);

            Label titleLabel = new Label();
            titleLabel.Text = video.Title;
            titleLabel.AutoSize = true;
            
            Button copyUrlButton = new Button();
            copyUrlButton.Text = manager.copyURL;
            copyUrlButton.Tag = GetVideoUrl(video.VideoId);
            copyUrlButton.Click += CopyUrlButton_Click;
            copyUrlButton.Dock = DockStyle.Bottom;

            resultPanel.Controls.Add(titleLabel);
            resultPanel.Controls.Add(thumbnailBox);
            resultPanel.Controls.Add(copyUrlButton);

            flowLayoutPanel1.Controls.Add(resultPanel);

            // FlowLayoutPanel'ın FlowDirection'ını ayarla
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            if (selectedtheme == manager.lightTheme)
            {
                titleLabel.ForeColor = Color.Black;
                copyUrlButton.ForeColor = Color.Black;
                copyUrlButton.BackColor = Color.White;
            }
            else if (selectedtheme == manager.darkTheme)
            {
                titleLabel.ForeColor = Color.White;
                copyUrlButton.ForeColor = Color.White;
                copyUrlButton.BackColor = Color.Black;
            }
        }

       



        private string GetThumbnailUrl(string videoId)
        {
            return $"https://img.youtube.com/vi/{videoId}/default.jpg";
        }

        private string GetVideoUrl(string videoId)
        {
            return $"https://www.youtube.com/watch?v={videoId}";
        }

        private void CopyUrlButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string videoUrl = (string)button.Tag;

            // Clipboard'e video URL'sini kopyala
            Clipboard.SetText(videoUrl);
        }

        private void SearchResultForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class YouTubeVideo
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
    }

}
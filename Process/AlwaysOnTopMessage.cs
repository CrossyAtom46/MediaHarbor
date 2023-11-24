using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaHarbor
{
    public class AlwaysOnTopMessageBox : Form
    {
        public static DialogResult Show(string text)
        {
            AlwaysOnTopMessageBox messageBox = new AlwaysOnTopMessageBox(text);
            return messageBox.ShowDialog();
        }

        private AlwaysOnTopMessageBox(string text)
        {
            InitializeComponent(text);
        }

        private void InitializeComponent(string text)
        {
            this.SuspendLayout();
            // Formun özelliklerini ayarla
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Text = "Message";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true; // Always on top olarak ayarla

            // Label ekleyerek mesajı göster
            Label label = new Label();
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(label);

            this.ResumeLayout(false);
        }
    }
}

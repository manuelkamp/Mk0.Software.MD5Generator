using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Mk0.Software.OnlineUpdater;

namespace Mk0.Software.MD5Generator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ButtonSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog.ShowDialog();
            if(res == DialogResult.OK)
            {
                GenerateMD5(openFileDialog.FileName);
            }
        }

        private void GenerateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    textBoxMD5.Text = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxMD5.Text))
            {
                Clipboard.SetText(textBoxMD5.Text);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.Start("https://www.kmpr.at/update/md5generator.xml");
        }
    }
}

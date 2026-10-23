using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private string[]? folderFile = null;
        private int selected = 0;
        private int end = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string[]? part1 = null;
                string[]? part2 = null;
                string[]? part3 = null;
                
                part1 = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.jpg");
                part2 = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.jpeg");
                part3 = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.bmp");

                folderFile = new string[part1.Length + part2.Length + part3.Length];
                Array.Copy(part1, 0, folderFile, 0, part1.Length);
                Array.Copy(part2, 0, folderFile, part1.Length, part2.Length);
                Array.Copy(part3, 0, folderFile, part1.Length + part2.Length, part3.Length);

                selected = 0;
                end = folderFile.Length - 1;

                if (folderFile.Length > 0)
                {
                    showImage(folderFile[selected]);
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnStart.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Khong tim thay file hinh anh trong thu muc nay!");
                }
            }
        }

        private void showImage(string path)
        {
            Image imgtemp = Image.FromFile(path);
            pictureBox1.Width = imgtemp.Width / 2;
            pictureBox1.Height = imgtemp.Height / 2;
            pictureBox1.Image = imgtemp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void prevImage()
        {
            if (folderFile == null || folderFile.Length == 0) return;

            if (selected == 0)
            {
                selected = folderFile.Length - 1;
                showImage(folderFile[selected]);
            }
            else
            {
                selected = selected - 1;
                showImage(folderFile[selected]);
            }
        }

        private void nextImage()
        {
            if (folderFile == null || folderFile.Length == 0) return;

            if (selected == folderFile.Length - 1)
            {
                selected = 0;
                showImage(folderFile[selected]);
            }
            else
            {
                selected = selected + 1;
                showImage(folderFile[selected]);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            prevImage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            nextImage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nextImage();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                btnStart.Text = "<< Bắt đầu Slide Show >>";
            }
            else
            {
                timer1.Enabled = true;
                btnStart.Text = "<< Kết thúc Slide Show >>";
            }
        }
    }
}

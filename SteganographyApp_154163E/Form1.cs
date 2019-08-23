using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace SteganographyApp_154163E
{
    public partial class Form1 : Form
    {
        private Bitmap bmp = null;
        private string extractedText = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void imagePictureBox_Click(object sender, EventArgs e)
        {
            dataTextBox.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                imagePictureBox.Image = Image.FromFile(open_dialog.FileName);
                textBox1.Text = open_dialog.FileName;
            }

        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)imagePictureBox.Image;



            if (bmp == null)
            {
                MessageBox.Show("Image is unavailable", "Error");

                return;
            }

            int h = bmp.Height;
            int w = bmp.Width;
            int pixels = h * w;


            string text = dataTextBox.Text;

            if (pixels * 3 < (text.Length * 8) + 8)
            {

                MessageBox.Show("Image is not sufficient", "Error");

                return;
            }

            if (text.Equals(""))
            {
                MessageBox.Show("You have not entered any secret ", "Error");

                return;
            }

            if (text.Length > 255)
            {
                MessageBox.Show("The Maximum length of secret is 255", "Error");

                return;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < 32 || text[i] > 125)
                {

                    MessageBox.Show("Secret contain invalid character(s)", "Error");
                    return;
                }
            }


            bmp = SteganographyEmbed.embedSecret(text, bmp);

            MessageBox.Show("The hiding of the secret is successful!", "Success");
            dataTextBox.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (bmp == null)
            {
                MessageBox.Show("Image is unavailable", "Error");

                return;
            }

            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Png);
                            MessageBox.Show("Saved Successfully!", "Success");

                        }
                        break;
                    case 1:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                            MessageBox.Show("Saved Successfully!", "Success");
                        }
                        break;
                }

                dataTextBox.Text = "";
                textBox2.Text = save_dialog.FileName;
            }

        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)imagePictureBox.Image;

            if (bmp == null)
            {
                MessageBox.Show("Image is unavailable", "Error");

                return;
            }

            string extractedText = SteganographyExtract.extractSecret(bmp);

            if (extractedText == null || extractedText.Equals(""))
            {
                MessageBox.Show("Do not contain any secret message", "Error");
            }
            else
            {
                dataTextBox.Text = extractedText;
            }


        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

      
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

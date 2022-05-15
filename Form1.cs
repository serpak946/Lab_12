using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_12
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Image image;
        Exception FileNotFoundEx = new Exception("Файл не выбран");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = openFileDialog1;
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(dialog.FileName);
                pictureBox1.Image = image;
                bmp = new Bitmap(image);
            }
            else
                MessageBox.Show("Картинка не выбрана");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (image == null) throw FileNotFoundEx;
                bmp = new Bitmap(image);
                progressBar1.Minimum = 0;
                progressBar1.Maximum = bmp.Width * bmp.Height;
                progressBar1.Step = 1;
                for (int i = 0; i < bmp.Width; i++)
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        Color color = bmp.GetPixel(i, j);
                        bmp.SetPixel(i, j,
                            Color.FromArgb(color.A,
                            checkBoxR.Checked ? color.R : 0,
                            checkBoxG.Checked ? color.G : 0,
                            checkBoxB.Checked ? color.B : 0));
                        progressBar1.PerformStep();
                    }
                progressBar1.Value = 0;
                pictureBox1.Image = bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message == "Файл не выбран")
                {
                    button1_Click(sender, e);
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (bmp == null) throw FileNotFoundEx;
                SaveFileDialog dialog2 = saveFileDialog1;
                dialog2.Filter = "Image files (*.PNG, *.JPG, *.GIF, *.BMP)|*.png;*.jpg;*.gif;*.bmp";
                dialog2.ShowDialog();
                bmp.Save(dialog2.FileName);
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
                if (ex.Message == "Файл не выбран")
                {
                    button1_Click(sender, e);
                }
                 
            }
        }
    }
}

using Alturos.Yolo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YOLO.desk.cs
{
    public partial class Form1 : Form
    {
        List<int> rectPoints = new List<int>();
        public int rowCount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 20;
            var configurationDetector = new ConfigurationDetector();
            var config = configurationDetector.Detect();
            using (var yoloWrapper = new YoloWrapper(config))
            {
                progressBar1.Value = 50;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, ImageFormat.Png);
                    var items = yoloWrapper.Detect(ms.ToArray());
                    yoloItemBindingSource.DataSource = items;
                    progressBar1.Value = 80;
                }
            }
            progressBar1.Value = 100;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            try
            {
                Image img1 = Image.FromFile(imagePathtxb.Text);
                pictureBox1.Image = img1;
            }
            catch
            {
                imagePathtxb.Text = string.Empty;
                MessageBox.Show("Incorrect image path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            var halfX = pictureBox1.ClientRectangle.Width / 2;
            var halfY = pictureBox1.ClientRectangle.Height / 2;

            Random rnd = new Random();
            var offsetX = rnd.Next(-10, 10);
            var offsetY = rnd.Next(-10, 10);

            drawPoint();
        }

        public void drawPoint()  //what da fuck dis do??
        {
            int colCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    
                    string value = dataGridView1.Rows[rowCount].Cells[colCount].Value.ToString(); //makes rectangle points
                    colCount++;
                    bool isIntString = value.All(char.IsDigit);
                    if (isIntString == true)
                    {
                        rectPoints.Add(Int32.Parse(value));
                    }
                }
                catch
                {
                    try
                    {
                        int X = rectPoints[0]; //rect point declerations
                        int Y = rectPoints[1];
                        int Size1 = rectPoints[2];
                        int Size2 = rectPoints[3];
                        string XS = textBox1.Text;
                        string YS = textBox2.Text;
                        string Size1S = textBox3.Text;
                        string Size2S = textBox4.Text;
                        Graphics g = Graphics.FromHwnd(pictureBox1.Handle); //draws boxes
                        SolidBrush brush = new SolidBrush(Color.Blue);
                        using (Pen selPen = new Pen(Color.Blue))
                        {
                            g.DrawRectangle(selPen, X, Y, Size1, Size2);
                        }
                        g.Dispose();
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        rectPoints.Clear();
                        IncreaseRows();
                    }
                    catch
                    {
                        
                    }
                }
            }
            try
            {
                
                int X = rectPoints[0]; //rect point declerations
                int Y = rectPoints[1];
                int Size1 = rectPoints[2];
                int Size2 = rectPoints[3];
                string XS = textBox1.Text;
                string YS = textBox2.Text;
                string Size1S = textBox3.Text;
                string Size2S = textBox4.Text;
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle); //draws boxes
                SolidBrush brush = new SolidBrush(Color.Blue);
                using (Pen selPen = new Pen(Color.Blue))
                {
                    g.DrawRectangle(selPen, X, Y, Size1, Size2);
                }
                g.Dispose();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                rectPoints.Clear();
                IncreaseRows();
            }
            catch
            {
               
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            rowCount++;
        }

        private void IncreaseRows()
        {
            rowCount++;
            drawPoint();
        }

        private void ImagePathtxb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            player.SoundLocation = "C:/Audio/House.wav";
            player.Play();
        }
    }
}
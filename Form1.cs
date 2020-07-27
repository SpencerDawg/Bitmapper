using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitmapper {
    public partial class Form1 : Form {
        Color[][] image = null;
        OpenFileDialog openFile = new OpenFileDialog();
        Button imgB = new Button();
        Bitmap bmp = null;
        public Form1() {
            InitializeComponent();
            AddStuff();
        }
        private void AddStuff() {
            //ofd
            openFile.FileOk += SetButt;
            openFile.Filter = "Bitmaps (*.bmp)|*.bmp|PNG Images (*.png)|*.png";
            //butts
            List<Control> l = new List<Control>();
            Button butt = new Button();
            l.Add(butt);
            {
                butt.Text = "Get Arr";
                butt.Location = new Point(this.Size.Width / 2);
                butt.Click += Butt_Click;
            }

            l.Add(imgB);
            {
                imgB.Location = new Point(butt.Location.X, butt.Location.Y + 50);
                if (image == null)
                    imgB.Text = "Choose your Image!";
                imgB.AutoSize = true;
                imgB.Click += ImgB_Click;
            }
            //now we actually add stuff
            for (int i = 0; i < l.Count; i++) {
                Control[] arr = l.ToArray();
                Controls.Add(arr[i]);
            }
        }

        private void Butt_Click(object sender, EventArgs e) {
            Dictionary<string, Color> colors = new Dictionary<string, Color>();
            foreach (Color c in bmp.Palette.Entries) {
                colors.Add(c.ToString(), c);
            }
            richTextBox1.Text = "{ ";
            if (image != null) {
                for (int i = 0; i < bmp.Height; i++) {
                    for (int j = 0; j < bmp.Width; j++) {
                        if (j == 0)
                            richTextBox1.Text += "{ ";
                        if (image[j][i].Name != "0") {
                            richTextBox1.Text += image[j][i].Name;
                            if (i != bmp.Height)
                                richTextBox1.Text += ", ";
                        }
                    }
                    richTextBox1.Text += "} \n";
                }
            }
            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 5);
            richTextBox1.Text = richTextBox1.Text.Remove(0, 2);
        }

        private void SetButt(object sender, CancelEventArgs e) {
            imgB.Text = openFile.FileName;
            bmp = new Bitmap(openFile.FileName);
            getBitmapColorMatrix();
        }

        private void ImgB_Click(object sender, EventArgs e) {
            openFile.ShowDialog();
        }
        public Color[][] getBitmapColorMatrix() {
            Color[][] matrix;
            int height = bmp.Height;
            int width = bmp.Width;
            if (height > width) {
                matrix = new Color[bmp.Width][];
                for (int i = 0; i <= bmp.Width - 1; i++) {
                    matrix[i] = new Color[bmp.Height];
                    for (int j = 0; j < bmp.Height - 1; j++) {
                        matrix[i][j] = bmp.GetPixel(i, j);
                    }
                }
            }
            else {
                matrix = new Color[bmp.Height][];
                for (int i = 0; i <= bmp.Height - 1; i++) {
                    matrix[i] = new Color[bmp.Width];
                    for (int j = 0; j < bmp.Width - 1; j++) {
                        matrix[i][j] = bmp.GetPixel(i, j);
                    }
                }
            }
            image = matrix;
            return image;
        }
        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}

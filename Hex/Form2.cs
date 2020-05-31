using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;


namespace Hex
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string CurrentFile = "";
        int[] sizes = new int[6];


        bool ConvertToIcon(Bitmap inputBitmap, Stream output)
        {
            if (inputBitmap == null)
                return false;

            int[] sizes = new int[] { 256, 128, 64, 48, 32, 16 };

            int numSelectSizes = 0; //Counter for how many sizes were selected, helps when saving the diff sizes to the .ico                          

            // Generate bitmaps for all the sizes and toss them in streams
            List<MemoryStream> imageStreams = new List<MemoryStream>();
            foreach (int size in sizes)
            {
                if (size == 16)
                {
                    //if (checkBox16.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 16");
                    //}
                    
                }

                if (size == 32)
                {
                    //if (checkBox32.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 32");
                    //}

                }

                if (size == 48)
                {
                    //if (checkBox48.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 48");
                    //}

                }

                if (size == 64)
                {
                    //if (checkBox64.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 64");
                    //}

                }

                if (size == 128)
                {
                    //if (checkBox128.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 128");
                    //}

                }

                if (size == 256)
                {
                    //if (checkBox256.Checked == true)
                    //{
                        Bitmap newBitmap = ResizeImage(inputBitmap, size, size);
                        if (newBitmap == null)
                            return false;
                        MemoryStream memoryStream = new MemoryStream();
                        newBitmap.Save(memoryStream, ImageFormat.Png);
                        imageStreams.Add(memoryStream);
                        numSelectSizes++;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("no 256");
                    //}

                }                

                
            }

            BinaryWriter iconWriter = new BinaryWriter(output);
            if (output == null || iconWriter == null)
                return false;

            int offset = 0;

            // 0-1 reserved, 0
            iconWriter.Write((byte)0);
            iconWriter.Write((byte)0);

            // 2-3 image type, 1 = icon, 2 = cursor
            iconWriter.Write((short)1);

            // 4-5 number of images
            iconWriter.Write((short)sizes.Length);

            offset += 6 + (16 * sizes.Length);

            
            //before had sizes.length instead of numSelectSizes
            for (int i = 0; i < numSelectSizes; i++)
            {
                // image entry 1
                // 0 image width
                iconWriter.Write((byte)sizes[i]);
                // 1 image height
                iconWriter.Write((byte)sizes[i]);

                // 2 number of colors
                iconWriter.Write((byte)0);

                // 3 reserved
                iconWriter.Write((byte)0);

                // 4-5 color planes
                iconWriter.Write((short)0);

                // 6-7 bits per pixel
                iconWriter.Write((short)32);

                // 8-11 size of image data
                //MessageBox.Show(imageStreams[5].Length + "");
                iconWriter.Write((int)imageStreams[i].Length);

                // 12-15 offset of image data
                iconWriter.Write((int)offset);

                offset += (int)imageStreams[i].Length;
            }

            for (int i = 0; i < numSelectSizes; i++)
            {
                // write image data
                // png data must contain the whole png data file
                iconWriter.Write(imageStreams[i].ToArray());
                imageStreams[i].Close();
            }

            iconWriter.Flush();
            //MessageBox.Show("done!");
            lblError.ForeColor = Color.Green;
            lblError.Text = "Icon Converted Successfully!";

            return true;
        }


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        void filePrompt()
        {
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.gif;*.png|PNG Images (*.png)|*.png|JPG Images (*.jpg)|*.jpg|GIF Images (*.gif)|*.gif|BMP Images (*.bmp)|*.bmp";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
                Bitmap b = new Bitmap(CurrentFile);
                //imagePanel1.CanvasSize = b.Size;
                imageFrame.Image = b;
                lblFileName.Text = CurrentFile;
                lblError.Text = "";
            }
        } 
        
        void savePrompt()
        {
            if (textBox1.Text != "")
            {
                saveFileDialog1.FileName = textBox1.Text + ".ico";
            }
            else
            {
                saveFileDialog1.FileName = "favicon.ico";
            }

            saveFileDialog1.Filter = "Icon Files (*.ico)|*.ico";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream IconStream = File.OpenWrite(saveFileDialog1.FileName);
                Bitmap b = new Bitmap(imageFrame.Image);
                if (checkBoxTrans.Checked == true)
                {
                    b = whiteTrans(b);
                }
                b.SetResolution(256, 256);
                ConvertToIcon(b, IconStream);
                IconStream.Close();
            }
        }

        Bitmap whiteTrans(Bitmap input)
        {
            input.MakeTransparent(Color.White);
            return input;
        }
        


        private void lblOpen_MouseEnter(object sender, EventArgs e)
        {
            lblOpen.BackColor = Color.Purple;
            lblOpen.ForeColor = Color.White;
        }

        private void lblOpen_MouseLeave(object sender, EventArgs e)
        {
            lblOpen.BackColor = Color.Black;
            lblOpen.ForeColor = Color.White;
        }

        private void lblOpen_Click(object sender, EventArgs e)
        {
            filePrompt();            
        }

        private void lblConvert_Click(object sender, EventArgs e)
        {
            if (CurrentFile == "")
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Please import an image!";
            }
            else
            {
                lblError.Text = "";
                savePrompt();
            }
        }

        private void lblConvert_MouseEnter(object sender, EventArgs e)
        {
            lblConvert.BackColor = Color.Purple;
            lblConvert.ForeColor = Color.White;
            //ColorWheel.Controls.ColorWheelControl memes = new ColorWheel.Controls.ColorWheelControl();
        }

        private void lblConvert_MouseLeave(object sender, EventArgs e)
        {
            lblConvert.BackColor = Color.Black;
            lblConvert.ForeColor = Color.White;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Iconify V1.5\n09/02/18\nBy Owen Goodwin\nthe404.ml");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It's recommended you use a square image, otherwise the icon will be a bit stretched.\n\nFor more help, go to the404.ml");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CurrentFile == "")
                {
                    lblError.ForeColor = Color.Red;
                    lblError.Text = "Please import an image!";
                }
                else
                {
                    lblError.Text = "";
                    savePrompt();
                    
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string CurrentFile = "";


        void filePrompt()
        {
            /*Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            CurrentFile = openFileDialog1.FileName;
                            myStream.Close();
                            loadFile();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }*/
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurrentFile = openFileDialog1.FileName;
                Bitmap b = new Bitmap(CurrentFile);                
                //imagePanel1.CanvasSize = b.Size;
                frame1.Image = b;
                label1.Text = CurrentFile;
            }
        }


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            filePrompt();
        }

        private void zoomBar_Scroll(object sender, EventArgs e)
        {
            frame1.Zoom = zoomBar.Value * 0.02f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(frame1.Image, 16, 16);
            
            b.Save("test.bmp");
        }
    }
}

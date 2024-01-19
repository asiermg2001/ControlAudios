using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Printing;
using Gma.System.MouseKeyHook;

namespace ControlAudios
{
    public partial class Form1 : Form
    {
        public DialogResult empezando;
        public DialogResult parando;
        public bool choosingStartKeybind;
        public bool choosingStopKeybind;
        public Keys empezar;
        public Keys parar;
        public string Test;
        public bool recording;
        public object test;
        public FileInfo lastFile;
        private IKeyboardMouseEvents m_GlobalHook;
        public Form1()
        {
            InitializeComponent();
            if (Properties.Settings.Default.path != null)
            { 
                folderBrowserDialog1.SelectedPath = Properties.Settings.Default.path;
            }
            if (Properties.Settings.Default.format != null)
            {

                //comboBox1.SelectedItem = (object)Program.Format;
            }
            Suscribe();
            this.CenterToScreen();
        }
        public void Suscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += Form1_KeyDown;
        }
        public void Unsuscribe()
        {
            m_GlobalHook.KeyDown -= Form1_KeyDown;
            m_GlobalHook.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Program.Path = folderBrowserDialog1.SelectedPath;
                button1.Text = folderBrowserDialog1.SelectedPath;
                fileSystemWatcher1.Path = folderBrowserDialog1.SelectedPath;
                Program.folderSelected = true;             
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
        }
        public static bool IsFileInUseGeneric(FileInfo file)
        {
            try
            {
                var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            Program.file = file;
        }
        private void fileSystemWatcher2_Created(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            Program.fbxFile = file;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SendKeys.SendWait("{UP}");
            recording = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (choosingStartKeybind)
            {
                empezar = e.KeyCode;
                button3.Text = empezar.ToString();
                choosingStartKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (choosingStopKeybind)
            {
                parar = e.KeyCode;
                button4.Text = parar.ToString();
                choosingStopKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == parar)
            {
                Thread.Sleep(2000);
                label2.Text = "OBS está: esperando";
                if (recording)
                {
                    if (Program.isOpen == false)
                    {
                        Form form2 = new Form2();
                        form2.Show();
                        Program.isOpen = true;
                        progressBar1.Visible = false;
                        progressBar1.Value = 0;
                        timer1.Stop();
                    }
                    recording = false;
                }
            }
            if (e.KeyCode == empezar)
            {
                if (!Program.folderSelected) 
                {
                    MessageBox.Show("Detén la grabacion, no se ha elegido una de las carpetas pero OBS esta grabando");
                }
                Properties.Settings.Default.path = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
                recording = true;
                label2.Text = "OBS está: grabando";
                progressBar1.Visible = true;
                timer1.Start();
            }
            /*
            if  (e.KeyCode == Keys.R)
            {
                if (recording)
                {
                    if (Program.isOpen == false)
                    {
                        Form form2 = new Form2();
                        form2.Show();
                        Program.isOpen = true;
                    }
                    recording = false;
                    return;
                }
                if (!recording)
                {
                    Properties.Settings.Default.path = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.Save();
                    recording = true;
                    label2.Text = "Grabando...";
                    return;
                }    

            }*/
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            //Program.previousFile = Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + letter + Program.file.Extension;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == progressBar1.Maximum)
                progressBar1.Value = 0;
        }

        private void button2_Click_4(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                Program.fbxPath = folderBrowserDialog2.SelectedPath;
                button2.Text = folderBrowserDialog2.SelectedPath;
                fileSystemWatcher2.Path = folderBrowserDialog2.SelectedPath;
                Program.bothFoldersSelected = true;
            }

        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            choosingStartKeybind = true;
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            choosingStopKeybind = true;
            timer1.Start();
        }
    }
}

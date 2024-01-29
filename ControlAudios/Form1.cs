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
        public bool audioUpKeybind;
        public bool audioDownKeybind;
        public bool audioGoodKeybind;
        public bool audioBadKeybind;
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
            button8.Text = Program.audioBad.ToString();
            button7.Text = Program.audioGood.ToString();
            button6.Text = Program.audioDown.ToString();
            button5.Text = Program.audioUp.ToString();
            button4.Text = Program.stopRecording.ToString();
            button3.Text = Program.startRecording.ToString();
            HideAllBoxes();
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
                Program.startRecording = e.KeyCode;
                button3.Text = Program.startRecording.ToString();
                choosingStartKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (choosingStopKeybind)
            {
                Program.stopRecording = e.KeyCode;
                button4.Text = Program.stopRecording.ToString();
                choosingStopKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (audioGoodKeybind)
            {
                Program.audioGood = e.KeyCode;
                button7.Text = Program.audioGood.ToString();
                audioGoodKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (audioBadKeybind)
            {
                Program.audioBad = e.KeyCode;
                button8.Text = Program.audioBad.ToString();
                audioBadKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (audioDownKeybind)
            {
                Program.audioDown = e.KeyCode;
                button6.Text = Program.audioDown.ToString();
                audioDownKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (audioUpKeybind)
            {
                Program.audioUp = e.KeyCode;
                button5.Text = Program.audioUp.ToString();
                audioUpKeybind = false;
                timer1.Stop();
                progressBar1.Visible = false;
                e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyCode == Program.stopRecording)
            {
                Thread.Sleep(4000);
                label2.Text = "OBS está: esperando";
                if (recording)
                {
                    if (Program.isOpen == false)
                    {
                        Form form2 = new Form2();
                        form2.Location = this.Location;
                        form2.Show();
                        Program.isOpen = true;
                        progressBar1.Visible = false;
                        progressBar1.Value = 0;
                        timer1.Stop();
                    }
                    recording = false;
                }
            }
            if (e.KeyCode == Program.startRecording)
            {
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

        private void button7_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            audioGoodKeybind = true;
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            audioBadKeybind = true;
            timer1.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            audioDownKeybind = true;
            timer1.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            audioUpKeybind = true;
            timer1.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                Program.RootFolderPath = folderBrowserDialog3.SelectedPath;
                string date = DateTime.Now.ToString("yyyy_MM_dd"); // Format the date as a string
                string folderPath = Path.Combine(Program.RootFolderPath, date); // Combine the root folder path with the date
                Program.TodaysFolder = folderPath;
                button1.Text = Program.TodaysFolder;
                Directory.CreateDirectory(folderPath);
                // Array of subfolder names
                string[] subfolders = { "MIE", "EUF", "TRI", "NEU" };

                foreach (string subfolder in subfolders)
                {
                    string subfolderPath = Path.Combine(folderPath, subfolder);
                    Directory.CreateDirectory(subfolderPath); // Create the subfolder

                    // Create the "FBX" and "Videos" subfolders inside each subfolder
                    Directory.CreateDirectory(Path.Combine(subfolderPath, "FBX"));
                    Directory.CreateDirectory(Path.Combine(subfolderPath, "Videos"));
                }
                ShowAllBoxes();
            }
        }
        public void HideAllBoxes()
        {
            TRIbox.Visible = false;
            NEUbox.Visible = false;
            EUFbox.Visible = false;
            MIEbox.Visible = false;
        }
        public void ShowAllBoxes()
        {
            TRIbox.Visible = true;
            NEUbox.Visible = true;
            EUFbox.Visible = true;
            MIEbox.Visible = true;

        }
        public void AllBoxesToFalse()
        {
            TRIbox.Checked = false;
            NEUbox.Checked = false;
            EUFbox.Checked = false;
            MIEbox.Checked = false;
        }

        private void MIEbox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void TRIbox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void NEUbox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void EUFbox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void MIEbox_Click(object sender, EventArgs e)
        {
            AllBoxesToFalse();
            MIEbox.Checked = true;
            Program.SelectedEmotion = "MIE";
            fileSystemWatcher1.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/FBX";
            fileSystemWatcher2.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/Videos";
        }

        private void TRIbox_Click(object sender, EventArgs e)
        {
            AllBoxesToFalse();
            TRIbox.Checked = true;
            Program.SelectedEmotion = "TRI";
            fileSystemWatcher1.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/FBX";
            fileSystemWatcher2.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/Videos";
        }

        private void NEUbox_Click(object sender, EventArgs e)
        {
            AllBoxesToFalse();
            NEUbox.Checked = true;
            Program.SelectedEmotion = "NEU";
            fileSystemWatcher1.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/FBX";
            fileSystemWatcher2.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/Videos";
        }

        private void EUFbox_Click(object sender, EventArgs e)
        {
            AllBoxesToFalse();
            EUFbox.Checked = true;
            Program.SelectedEmotion = "EUF";
            fileSystemWatcher1.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/FBX";
            fileSystemWatcher2.Path = Program.TodaysFolder + "/" + Program.SelectedEmotion + "/Videos";
        }
    }
}

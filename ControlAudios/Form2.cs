using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Gma.System.MouseKeyHook;

namespace ControlAudios
{
    public partial class Form2 : Form
    {
        public string textSave;
        public int numbers;
        public string count;
        public int audioNumber;
        private IKeyboardMouseEvents m_GlobalHook;
        public Form2()
        {
            InitializeComponent();
            if (Program.file != null)
            {
            label1.Text = "Nuevo archivo creado: " + Program.file.Name;
                label2.Text = "Nuevo archivo creado: " + Program.fbxFile.Name;

            }
            //textBox1.Text = Properties.Settings.Default.textBoxField;
            numbers = Properties.Settings.Default.numbers;
            numericUpDown1.Value = Properties.Settings.Default.audioNumber;
            this.KeyPreview = true;
            label3.Text = numbers.ToString();
        }
        public void Suscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += Form2_KeyDown;
        }
        public void Unsuscribe()
        {
            m_GlobalHook.KeyDown -= Form2_KeyDown;
            m_GlobalHook.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void CloseAndSave()
        {
            Properties.Settings.Default.numbers = numbers;
            // Properties.Settings.Default.textBoxField = textSave;
            Properties.Settings.Default.Save();
            Program.isOpen = false;
            label3.Text = numbers.ToString();
            this.Close();
        }

        void Rename(string letter,int number)
        { 
            File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" +  Program.SelectedEmotion + "1" + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3")  + Program.file.Extension);
            File.Move(Program.fbxFile.FullName, Program.fbxFile.DirectoryName + "\\" + Program.SelectedEmotion + "1" + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.fbxFile.Extension);
            Program.previousFile = new FileInfo(Program.file.DirectoryName + "\\" + Program.SelectedEmotion + "1" + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.file.Extension);
            Program.fbxPreviousFile = new FileInfo(Program.fbxFile.DirectoryName + "\\" + Program.SelectedEmotion + "1" + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.fbxFile.Extension);
            //Program.previousFile = Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + numbers.ToString("D3") + letter + Program.file.Extension
        }
        public void ChangeLetter(string letter)
        {
           // int newaudionumber = (int)numericUpDown1.Value;
            //File.Move(Program.previousFile.FullName, Program.previousFile.DirectoryName + "\\" + textBox1.Text + "1" + newaudionumber.ToString("D4") + letter + "_" + numbers.ToString("D3") + Program.previousFile.Extension);
            //File.Move(Program.fbxPreviousFile.FullName, Program.fbxPreviousFile.DirectoryName + "\\" + textBox1.Text + "1" + audioNumber.ToString("D4") + letter + "_" + numbers.ToString("D3") + Program.fbxPreviousFile.Extension);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            numbers++;
            //count = numbers.ToString("D3");
            audioNumber = (int)numericUpDown1.Value;
            Rename("_M", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_m" + Program.file.Extension);
            //Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numbers++;
            count = numbers.ToString("D3");
            audioNumber = (int)numericUpDown1.Value;
            Program.previousFile = Program.file;
            Rename("_B", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_b" + Program.file.Extension);
            //Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numbers++;
            audioNumber = (int)numericUpDown1.Value;
            Rename("_BB", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_bb" + Program.file.Extension);
            //Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.audioNumber = ((int)numericUpDown1.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numbers = 0;
            label3.Text = Properties.Settings.Default.numbers.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeLetter("_m");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeLetter("_b");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeLetter("_bb");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.isOpen = false;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Program.audioUp)
            {
                numericUpDown1.Value++;
                numbers = 0;
                label3.Text = "1";
            }
            if (e.KeyCode == Program.audioDown)
            {
                numericUpDown1.Value--;
                numbers = 0;
                label3.Text = "1";
            }
            if (e.KeyCode == Program.audioGood)
            {
                numbers++;
                count = numbers.ToString("D3");
                audioNumber = (int)numericUpDown1.Value;
                Program.previousFile = Program.file;
                Rename("_B", numbers);
                //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_b" + Program.file.Extension);
                //Properties.Settings.Default.textBoxField = textBox1.Text;
                CloseAndSave();
                

            }
            if (e.KeyCode == Keys.Escape)
            {
                this.ActiveControl = null;
            }
            if (e.KeyCode == Program.audioBad)
            {
                numbers++;
                //count = numbers.ToString("D3");
                audioNumber = (int)numericUpDown1.Value;
                Rename("_M", numbers);
                //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_m" + Program.file.Extension);
               // Properties.Settings.Default.textBoxField = textBox1.Text;
                CloseAndSave();
                
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            label1.Focus();
        }
    }
}

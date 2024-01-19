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
            textBox1.Text = Properties.Settings.Default.textBoxField;
            numbers = Properties.Settings.Default.numbers;
            numericUpDown1.Value = Properties.Settings.Default.audioNumber;
            this.CenterToScreen();
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
            this.Close();
        }

        void Rename(string letter,int number)
        {
            if (!Program.folderSelected)
            {
                MessageBox.Show("No habias elegido carpeta!");
                return;
            }
            File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + textBox1.Text + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3")  + Program.file.Extension);
            File.Move(Program.fbxFile.FullName, Program.fbxFile.DirectoryName + "\\" + textBox1.Text + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.fbxFile.Extension);
            Program.previousFile = new FileInfo(Program.file.DirectoryName + "\\" + textBox1.Text + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.file.Extension);
            Program.fbxPreviousFile = new FileInfo(Program.fbxFile.DirectoryName + "\\" + textBox1.Text + audioNumber.ToString("D4") + letter + "_" + number.ToString("D3") + Program.fbxFile.Extension);
            //Program.previousFile = Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + numbers.ToString("D3") + letter + Program.file.Extension
        }
        public void ChangeLetter(string letter)
        {
            int newaudionumber = (int)numericUpDown1.Value;
            File.Move(Program.previousFile.FullName, Program.previousFile.DirectoryName + "\\" + textBox1.Text + newaudionumber.ToString("D4") + letter + "_" + numbers.ToString("D3") + Program.previousFile.Extension);
            File.Move(Program.fbxPreviousFile.FullName, Program.fbxPreviousFile.DirectoryName + "\\" + textBox1.Text + audioNumber.ToString("D4") + letter + "_" + numbers.ToString("D3") + Program.fbxPreviousFile.Extension);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            numbers++;
            //count = numbers.ToString("D3");
            audioNumber = (int)numericUpDown1.Value;
            Rename("_m", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_m" + Program.file.Extension);
            Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numbers++;
            count = numbers.ToString("D3");
            audioNumber = (int)numericUpDown1.Value;
            Program.previousFile = Program.file;
            Rename("_b", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_b" + Program.file.Extension);
            Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numbers++;
            audioNumber = (int)numericUpDown1.Value;
            Rename("_bb", numbers);
            //File.Move(Program.file.FullName, Program.file.DirectoryName + "\\" + audioNumber + textBox1.Text + count + "_bb" + Program.file.Extension);
            Properties.Settings.Default.textBoxField = textBox1.Text;
            CloseAndSave();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.audioNumber = ((int)numericUpDown1.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numbers = 0;
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
            if (e.KeyCode == Keys.Up)
            {
                numericUpDown1.Value++;
            }
            if (e.KeyCode == Keys.Down)
            {
                numericUpDown1.Value--;
            }
            if (e.KeyCode == Keys.Add)
            {
                numbers++;
                count = numbers.ToString("D3");
                audioNumber = (int)numericUpDown1.Value;
                Program.previousFile = Program.file;
                Rename("_b", numbers);
                Properties.Settings.Default.textBoxField = textBox1.Text;
                CloseAndSave();

            }
            if (e.KeyCode == Keys.Subtract)
            {
                numbers++;
                audioNumber = (int)numericUpDown1.Value;
                Rename("_m", numbers);
                Properties.Settings.Default.textBoxField = textBox1.Text;
                CloseAndSave();
            }
        }
    }
}

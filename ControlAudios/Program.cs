using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using WebSocketSharp;

namespace ControlAudios
{
    internal static class Program
    {
        public static bool isOpen;
        public static string Path;
        public static string fbxPath;
        public static string Format;
        public static FileInfo fbxFile;
        public static FileInfo fbxPreviousFile;
        public static FileInfo file;
        public static FileInfo previousFile;
        public static bool folderSelected;
        public static bool bothFoldersSelected;
        public static Keys audioGood = Keys.Up;
        public static Keys audioBad = Keys.Down;
        public static Keys audioUp = Keys.Right;
        public static Keys audioDown = Keys.Left;
        public static Keys startRecording = Keys.Oemplus;
        public static Keys stopRecording = Keys.OemQuestion;
        public static string RootFolderPath;
        public static string SelectedEmotion;
        public static string TodaysFolder;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}


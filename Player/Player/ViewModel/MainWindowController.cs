using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.PlayerApplication
{
    class MainWindowController
    {
        public static List<string> Paths;
        public static List<string> SelectFiles()
        {
            List<string> files = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Paths = openFileDialog.FileNames.ToList();
                foreach (string filename in openFileDialog.SafeFileNames)
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(filename));
            }
            return files;

        }
    }
}

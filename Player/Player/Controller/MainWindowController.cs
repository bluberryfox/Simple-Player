using Microsoft.Win32;
using Player.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Player.Controller
{
    class MainWindowController 
    {
        private static List<string> paths;
        
        public static List<string> FindAllFiles()
        {
            var files = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                paths = openFileDialog.FileNames.ToList();
                foreach (string filename in openFileDialog.SafeFileNames)
                    files.Add(System.IO.Path.GetFileNameWithoutExtension(filename));
            }
            return files;
        }
        public static string GetPathToFile(int index)
        {
            return paths[index];
        }



        
    }
}

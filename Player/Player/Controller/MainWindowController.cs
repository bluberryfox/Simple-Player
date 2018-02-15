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

namespace Player.VM
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
       
        public static List<string> Paths;
        private ObservableCollection<string> files;
        private int selectedSongIndex;
        public MediaElement Player { get; set; }
        public Song song { get; set; }

        public int SelectedSongIndex
        {
            get
            {
                return selectedSongIndex;
            }
            set
            {
                if (Equals(value, selectedSongIndex)) return;
                selectedSongIndex = value;
                OnPropertyChanged(nameof(SelectedSongIndex));
            }
        }
        public ObservableCollection<string> Files
        {
            get
            {
                return files;
            }
            set
            {
                if (Equals(value, files)) return;
                files = value;
                OnPropertyChanged(nameof(Files));
            }
        }

        public MainWindowViewModel()
        {
            AddFilesCommand = new Command(arg => SelectFiles());
            PlayMusicCommand = new Command(arg => PlayMusic(selectedSongIndex));
            Files = new ObservableCollection<string>();
            song = new Song(Paths[selectedSongIndex], files[selectedSongIndex]);
           
        }

        private string PlayMusic(int selectedSongIndex)
        {
            return Paths[selectedSongIndex];
        }

        public ICommand AddFilesCommand { get; set; }
        public ICommand PlayMusicCommand { get; set; }
       


        public void SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Paths = openFileDialog.FileNames.ToList();
                foreach (string filename in openFileDialog.SafeFileNames)
                    Files.Add(System.IO.Path.GetFileNameWithoutExtension(filename));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

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
using System.Windows.Input;

namespace Player.VM
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Song> _playlist;
        public List<string> Paths;
        public ObservableCollection<string> files;
        public ObservableCollection<Song> Playlist
        {
            get { return _playlist; }
            set
            {
                if (Equals(value, _playlist)) return;
                _playlist = value;
                OnPropertyChanged(nameof(Playlist));
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
            Files = new ObservableCollection<string>();
            //song = new Song("music.mp3", "music");
        }
        public ICommand AddFilesCommand { get; set; }

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

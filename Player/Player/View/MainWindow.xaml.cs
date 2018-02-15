using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Player.Domain;
using Player.VM;

namespace Player.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            this.DataContext = vm;
        }

        private void Play_music_Click(object sender, RoutedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            media.Play();
            currentSongIndex++;
        }

        private void Previos_song_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.SelectedIndex - 1 < 0) return;
            playlist.SelectedIndex = playlist.SelectedIndex - 1;
            media.Play();
        }

        private void Next_song_Click(object sender, RoutedEventArgs e)
        {
            playlist.SelectedIndex = playlist.SelectedIndex + 1;
            media.Play();

        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            media.Source = new Uri(MainWindowViewModel.Paths[currentSongIndex]);
            media.Play();
        }

        private void Pause_button_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }
        
    }
}

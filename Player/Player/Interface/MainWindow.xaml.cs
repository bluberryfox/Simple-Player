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
using Player.PlayerApplication;

namespace Player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play_music_Click(object sender, RoutedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            PlayMedia(currentSongIndex);
            currentSongIndex++;
        }

        private void Previos_song_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.SelectedIndex - 1 < 0) return;
            playlist.SelectedIndex = playlist.SelectedIndex - 1;
            PlayMedia(playlist.SelectedIndex);
        }

        private void Next_song_Click(object sender, RoutedEventArgs e)
        {
            playlist.SelectedIndex = playlist.SelectedIndex + 1;
            PlayMedia(playlist.SelectedIndex);

        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            Song song = new Song(MainWindowController.Paths[currentSongIndex], playlist.SelectedItem.ToString());
            artist_name.Content = song.Singer;
            song_title.Content = song.Title;
            song_lyrics.Text = (song.Lyrics != "") ? song.Lyrics : "К сожалению, текста не найдено, но мы работаем над этим. Или вы просто слушаете русскую музыку)";
            PlayMedia(currentSongIndex);
        }

        private void Pause_button_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<string> files = MainWindowController.SelectFiles();
            foreach (var songname in files)
            {
                playlist.Items.Add(songname);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }
        private void PlayMedia(int index)
        {
            media.Source = new Uri(MainWindowController.Paths[index]);
            media.Play();
        }
    }
}

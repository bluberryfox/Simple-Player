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
using Player.Controller;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace Player.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((media.Source != null) && (media.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                progress.Minimum = 0;
                progress.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
                progress.Value = media.Position.TotalSeconds;
            }
        }
        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            media.Position = TimeSpan.FromSeconds(progress.Value);
        }


        private void Play_music_Click(object sender, RoutedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            string path = MainWindowController.GetPathToFile(currentSongIndex);
            PlayMedia(path);
            currentSongIndex++;
        }

        private void Previos_song_Click(object sender, RoutedEventArgs e)
        {
            if (playlist.SelectedIndex - 1 < 0) return;
            playlist.SelectedIndex = playlist.SelectedIndex - 1;
            string path = MainWindowController.GetPathToFile(playlist.SelectedIndex);
            PlayMedia(path);
        }

        private void Next_song_Click(object sender, RoutedEventArgs e)
        {
            playlist.SelectedIndex = playlist.SelectedIndex + 1;
            string path = MainWindowController.GetPathToFile(playlist.SelectedIndex);
            PlayMedia(path);

        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int currentSongIndex = playlist.SelectedIndex;
            string path = MainWindowController.GetPathToFile(currentSongIndex);
            Song song = new Song(path);
            artist_name.Content = song.Singer;
            song_title.Content = song.Title;
            song_lyrics.Text = (song.Lyrics != "") ? song.Lyrics : "К сожалению, текста не найдено, но мы работаем над этим. Или вы просто слушаете русскую музыку)";
            PlayMedia(path);
        }

        private void Pause_button_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            playlist.ItemsSource = MainWindowController.FindAllFiles();
            
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }
        private void PlayMedia(string path)
        {
            media.Source = new Uri(path);
            media.Play();
        }
    }
}
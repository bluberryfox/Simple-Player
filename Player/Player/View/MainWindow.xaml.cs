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
        private bool userIsDraggingSlider = false;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((media.Source != null) && (media.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                progress.Minimum = 0;
                progress.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
                progress.Value = media.Position.TotalSeconds;
            }
        }
        private void SliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        private void SliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            media.Position = TimeSpan.FromSeconds(progress.Value);
        }


        private void Play_music_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
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
            if (playlist.SelectedIndex - 1 < 0) return;
            playlist.SelectedIndex = playlist.SelectedIndex + 1;
            PlayMedia(MainWindowController.GetPathToFile(playlist.SelectedIndex));

        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = MainWindowController.GetPathToFile(playlist.SelectedIndex);
            var temp = MainWindowController.getSongInfo(path);
            artist_name.Content = temp.Item1;
            song_title.Content = temp.Item2;
            song_lyrics.Text = (temp.Item3 != "") ? temp.Item3 : "К сожалению, текста не найдено, но мы работаем над этим. Или вы просто слушаете русскую музыку)";
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

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            media.Stop();

            if (playlist.SelectedIndex <= playlist.Items.Count)
            {
                playlist.SelectedIndex = playlist.SelectedIndex += 1;
                media.Play();
            }
            else
            {
                media.Stop();
            }
        }
    }
}
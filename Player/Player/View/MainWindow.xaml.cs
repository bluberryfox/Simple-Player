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
        MainWindowController controller;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            controller = new MainWindowController();
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
        private void Progress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        private void Progress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            media.Position = TimeSpan.FromSeconds(progress.Value);
        }


        private void Play_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void PreviosSong_Click(object sender, RoutedEventArgs e)
        {
            ChangeSong(playlist.SelectedIndex, x => x - 1);
        }

        private void NextSong_Click(object sender, RoutedEventArgs e)
        {
            ChangeSong(playlist.SelectedIndex, x=>x+1);
        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = controller.GetPathToFile(playlist.SelectedIndex);
            var temp = controller.GetSongInfo(path);
            artistName.Content = temp.Item1;
            songTitle.Content = temp.Item2;
            songLyrics.Text = temp.Item3 ?? "=^.^= \n Текста нет, но вы держитесь";
            PlayMedia(path);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            playlist.ItemsSource = controller.FindAllFiles();
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
        private void ChangeSong(int currentIndex, Func<int, int> playlistDirection)
        {
            if (currentIndex - 1 < 0) return;
            playlist.SelectedIndex = playlistDirection(currentIndex);
            PlayMedia(controller.GetPathToFile(playlist.SelectedIndex));
        }
    }
}
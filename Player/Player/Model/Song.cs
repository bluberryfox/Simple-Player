using Player.Infrastructure;
using Player.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain
{
    class Song 
    {
        
        private string singer;
        private string title;
        private string lyrics;

        public string Singer
        {
            get
            {
                return singer;
            }
            set
            {
                if (singer != value)
                {
                    singer = value;
                    
                }
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                   
                }
            }
        }
        public string Lyrics
        {
            get
            {
                return lyrics;
            }
            set
            {
                if (lyrics != value)
                {
                    lyrics = value;
                   
                }
            }
        }

        public Song(string path)
        {
            var audioFile = TagLib.File.Create(path);

            string temp_singer = String.Join(", ", audioFile.Tag.Performers);
            string temp_title = audioFile.Tag.Title;

            if (temp_singer == "" || temp_singer == null ||
                temp_title == "" || temp_title == null)
            {
                FixFile.FixMP3(path, audioFile);
            }

            singer = String.Join(", ", audioFile.Tag.Performers);
            title = audioFile.Tag.Title;

            string temp_lyrics = audioFile.Tag.Lyrics;
            if (temp_lyrics == null || temp_lyrics == "")
            {
                FixFile.WriteLyrics(singer, title, audioFile);
            }
            lyrics = audioFile.Tag.Lyrics;
        }
    }
}

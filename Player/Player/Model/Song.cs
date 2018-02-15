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
            if (String.Join(", ", audioFile.Tag.Performers) == "" || String.Join(", ", audioFile.Tag.Performers) == null ||
                audioFile.Tag.Title == "" || audioFile.Tag.Title == null)
            {
                FixFile.FixMP3(path, audioFile);
            }
            singer = String.Join(", ", audioFile.Tag.Performers);
            title = audioFile.Tag.Title;

            if (audioFile.Tag.Lyrics == null || audioFile.Tag.Lyrics == "")
            {
                lyrics = Deserialization.DeserializeLyrics(SearchLyrics.FindLyrics(singer, title));
                audioFile.Save();
            }
            else
            {
                lyrics = audioFile.Tag.Lyrics;
            }
        }
    }
}

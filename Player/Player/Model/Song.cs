using Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Song(string path, string filename)
        {
            var audioFile = TagLib.File.Create(path);
            singer = String.Join(", ", audioFile.Tag.Performers);
            title = audioFile.Tag.Title;

            if (singer == "" || singer == null || title == "" || title == null)
            {
                generateData(filename);
                audioFile.Tag.Performers = new string[] { singer };
                audioFile.Tag.Title = title;
            }

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
        private void generateData(string filename)
        {
            int separatorIndex = filename.LastIndexOf('-');
            if (separatorIndex == -1)
            {
                singer = "Unknown";
                title = "Unknown";
            }
            else
            {
                singer = filename.Substring(0, separatorIndex - 1);
                title = filename.Substring(separatorIndex + 2);
            }
        }
    }
}

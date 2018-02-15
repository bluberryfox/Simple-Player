using Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Services
{
    class FixFile
    {

        public static void FixMP3(string path, TagLib.File audioFile)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            GenerateMP3Data(filename, audioFile);
            string singer = audioFile.Tag.Performers.ToString();
            string song = audioFile.Tag.Title;
            audioFile.Tag.Lyrics = Deserialization.DeserializeLyrics(SearchLyrics.FindLyrics(singer, song));
            audioFile.Save();
        }

        private static void GenerateMP3Data(string filename, TagLib.File audioFile)
        {
            int separatorIndex = filename.LastIndexOf('-');
            if (separatorIndex == -1)
            {
                audioFile.Tag.Performers= new string[] { "Unknown"};
                audioFile.Tag.Title = "Unknown";
            }
            else
            {
                audioFile.Tag.Performers = new string[] { filename.Substring(0, separatorIndex - 1) };
                audioFile.Tag.Title = filename.Substring(separatorIndex + 2);
            }
        }

        public static void WriteLyrics(string singer, string title, TagLib.File audioFile)
        {
            audioFile.Tag.Lyrics = Deserialization.DeserializeLyrics(SearchLyrics.FindLyrics(singer, title));
            audioFile.Save();
        }
    }
}

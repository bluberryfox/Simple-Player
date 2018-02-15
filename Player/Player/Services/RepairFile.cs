using Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Services
{
    class RepairFile
    {
        public static void WriteSongData(string path, TagLib.File audioFile)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            int separatorIndex = filename.LastIndexOf('-');
            if (separatorIndex == -1)
            {
                audioFile.Tag.Performers = new string[] { "Unknown" };
                audioFile.Tag.Title = "Unknown";
            }
            else
            {
                audioFile.Tag.Performers = new string[] { filename.Substring(0, separatorIndex - 1) };
                audioFile.Tag.Title = filename.Substring(separatorIndex + 2);
            }
            audioFile.Save();
        }

        public static void WriteLyrics(string singer, string title, TagLib.File audioFile)
        {
            string result = SearchLyrics.FindLyrics(singer, title);
            if (result.Length <= 0)
                return;
            else
                audioFile.Tag.Lyrics = Deserialization.DeserializeLyrics(result);
            audioFile.Save();
        }
    }
}

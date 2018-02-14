using Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Domain
{
    class Song
    {
        public string Singer { get; private set; }
        public string Title { get; private set; }
        public string Lyrics { get; private set; }
        public Song(string path, string filename)
        {
            //тут должна была бы быть база, но я нашла библиотеку и записываю текст прям в mp3 файл
            var audioFile = TagLib.File.Create(path);
            Singer = String.Join(", ", audioFile.Tag.Performers);
            Title = audioFile.Tag.Title;
            if (Singer == "" || Singer == null || Title == "" || Title == null)
            {
                generateData(filename);
                audioFile.Tag.Performers = new string[] { Singer };
                audioFile.Tag.Title = Title;
            }
            if (audioFile.Tag.Lyrics == null || audioFile.Tag.Lyrics == "")
            {
                Lyrics = Deserialization.DeserializeLyrics(SearchLyrics.FindLyrics(Singer, Title));
                audioFile.Save();
            }
            else
            {
                Lyrics = audioFile.Tag.Lyrics;
            }
        }
        private void generateData(string filename)
        {
            int separatorIndex = filename.LastIndexOf('-');
            if (separatorIndex == -1)
            {
                Singer = "Unknown";
                Title = "Unknown";
            }
            else
            {
                Singer = filename.Substring(0, separatorIndex - 1);
                Title = filename.Substring(separatorIndex + 2);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Player.Infrastructure.Models
{
    [DataContract]
    class SongLyrics
    {
        public string Lyrics
        {
            get
            {
                return lyrics;
            }
            private set
            {
                lyrics = value;
            }
        }
        //на сервере ответы тоже приходят с маленькой буквы, потому и тут с маленькой
        [DataMember]
        private string lyrics;

    }
}

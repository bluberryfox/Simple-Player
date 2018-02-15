using Player.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Player.Infrastructure
{
    class Deserialization
    {
        public static string DeserializeLyrics(string searchResult)
        {
            SongLyrics result;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(searchResult)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(SongLyrics));
                result = (SongLyrics)deserializer.ReadObject(ms);
            }
            return result.Lyrics;
        }
    }
}

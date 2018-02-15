using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Player.Infrastructure
{
    class SearchLyrics
    {
        public static string FindLyrics(string singer, string song)
        {
            //TODO:Add exeption
            string URL = $"http://lyric-api.herokuapp.com/api/find/{singer}/{song}";
            WebRequest request = WebRequest.Create(URL);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string searchResult = streamReader.ReadToEnd();
            streamReader.Close();
            return searchResult;
        }
    }
}

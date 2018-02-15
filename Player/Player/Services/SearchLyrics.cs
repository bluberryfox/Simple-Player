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
            string URL = $"http://lyric-api.herokuapp.com/api/find/{singer}/{song}";
            string result;
            try
            {
                WebRequest request = WebRequest.Create(URL);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                result = "";

            }
            return result;
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Joking
{
    public class JokeEngine
    {
        public string makeMeLaugh()
        {
            string ret = string.Empty;

            var request = HttpWebRequest.Create(Addin.AddinResources.JokingAPIUrl);
            request.Method = "GET";
            request.ContentLength = 0;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        var jss = new JavaScriptSerializer();

                        JokeResponse jokeResponse = jss.Deserialize<JokeResponse>(responseString);

                        ret = jokeResponse.value.joke;
                    }
                }
            }

            return ret;
        }
    }

    class JokeResponse
    {
        public string type { get; set; }
        public Joke value { get; set; }
    }

    class Joke
    {
        public int id { get; set; }
        public string joke { get; set; }
        public List<string> categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Networking
{
    public class Request
    {
        static public string dadJoke()
        {
            Request request = new Request();

            return request.fetch(new Uri(Addin.AddinResources.DadJokeUrl));
        }

        static public string motivationalQuote()
        {
            Request request = new Request();

            return request.fetch(new Uri(Addin.AddinResources.MotivationalQuote));
        }

        protected string fetch(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.AbsoluteUri);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = 
                                                    SecurityProtocolType.Tls |
                                                    SecurityProtocolType.Tls11 |
                                                    SecurityProtocolType.Tls12 |
                                                    SecurityProtocolType.Ssl3;

            string ret = string.Empty;

            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentLength = 0;
            using (var response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        ret = streamReader.ReadToEnd();
                    }
                }
            }

            return ret;
        }
    }
}

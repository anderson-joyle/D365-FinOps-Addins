using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Resourcing
{
    public class OnlineResources
    {
        public static string KnownMethodNames
        {
            get
            {
                OnlineResources resources;
                JObject jObj;

                resources = new OnlineResources();

                jObj = JObject.Parse(resources.getJsonContent());

                return jObj["known_method_names"].ToString();
            }
        }

        public static string KnownMethodNamePrefixes
        {
            get
            {
                OnlineResources resources;
                JObject jObj;

                resources = new OnlineResources();

                jObj = JObject.Parse(resources.getJsonContent());

                return jObj["known_method_name_prefixes"].ToString();
            }
        }

        private string getJsonContent()
        {
            string content = string.Empty;

            var request = HttpWebRequest.Create(Addin.AddinResources.JsonURL);
            request.Method = "GET";
            request.ContentLength = 0;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        content = streamReader.ReadToEnd();
                    }
                }
            }

            return content;
        }
    }
}

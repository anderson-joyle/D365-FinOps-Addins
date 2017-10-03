using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace Resourcing
{
    public class OnlineResources
    {
        #region Variables
        private Dictionary<string, string> knownMethodNames = null;
        private Dictionary<string, string> knownMethodNamesPrefixes = null;
        #endregion

        #region Properties
        protected Dictionary<string, string> KnownMethodNames
        {
            get
            {
                OnlineResources resources;
                XmlDocument xmlDocument;

                if (knownMethodNames == null)
                {
                    knownMethodNames = new Dictionary<string, string>();
                    xmlDocument = new XmlDocument();
                    resources = new OnlineResources();

                    xmlDocument.LoadXml(resources.getXmlContent());

                    foreach (XmlNode node in xmlDocument.SelectNodes("//knownname"))
                    {
                        knownMethodNames.Add(node.Attributes["name"].InnerText, node.Attributes["description"].InnerText);
                    }
                }

                return knownMethodNames;
            }
        }

        protected Dictionary<string, string> KnownMethodNamePrefixes
        {
            get
            {
                OnlineResources resources;
                XmlDocument xmlDocument;

                if (knownMethodNamesPrefixes == null)
                {
                    knownMethodNamesPrefixes = new Dictionary<string, string>();
                    xmlDocument = new XmlDocument();
                    resources = new OnlineResources();

                    xmlDocument.LoadXml(resources.getXmlContent());

                    foreach (XmlNode node in xmlDocument.SelectNodes("//knownprefix"))
                    {
                        knownMethodNamesPrefixes.Add(node.Attributes["prefix"].InnerText, node.Attributes["description"].InnerText);
                    }
                }

                return knownMethodNamesPrefixes;
            }
        }
        #endregion

        private string getXmlContent()
        {
            string content = string.Empty;

            var request = HttpWebRequest.Create(Addin.AddinResources.XmlURL);
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

        public bool hasKnownName(string methodName)
        {
            return this.KnownMethodNames.ContainsKey(methodName);
        }

        public bool hasKnownNamePrefix(string methodName)
        {
            bool ret = false;

            foreach (KeyValuePair<string, string> pair in this.KnownMethodNamePrefixes)
            {
                if (methodName.StartsWith(pair.Key))
                {
                    ret = true;
                    break;
                }
            }
            
            return ret;
        }

        public string getKnownNamePrefixDescription(string methodName)
        {
            string ret = string.Empty;

            foreach (KeyValuePair<string, string> pair in this.KnownMethodNamePrefixes)
            {
                if (methodName.StartsWith(pair.Key))
                {
                    ret = pair.Value;
                    break;
                }
            }

            return ret;
        }

        public string getnownNamePrefix(string methodName)
        {
            string ret = string.Empty;

            foreach (KeyValuePair<string, string> pair in this.KnownMethodNamePrefixes)
            {
                if (methodName.StartsWith(pair.Key))
                {
                    ret = pair.Key;
                    break;
                }
            }

            return ret;
        }

        public string getKnownNameDescription(string methodName)
        {
            return this.KnownMethodNames[methodName];
        }
    }
}

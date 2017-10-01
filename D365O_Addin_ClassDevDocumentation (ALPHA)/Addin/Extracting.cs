using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System.Text.RegularExpressions;
using System.Resources;
using System.Collections;
using Newtonsoft.Json.Linq;
using Resourcing;

namespace Extracting
{
    public interface ITagExtractor
    {
        string getTagValue();
        bool validate();
    }

    public class TagSummary : ITagExtractor
    {
        protected AxMethod method;

        #region Constants
        protected const string tag = "\r/// <summary>\r\n/// {0}\r\n/// </summary>\r\n";
        #endregion

        #region Properties
        private string knownMethodNames;
        private string knownMethodNamePrefixes;
        protected string KnownMethodNames
        {
            get
            {
                if (knownMethodNames == string.Empty)
                {
                    knownMethodNames = OnlineResources.KnownMethodNames;
                }

                return knownMethodNames;
            }
        }

        protected string KnownMethodNameprefixes
        {
            get
            {
                if (knownMethodNamePrefixes == string.Empty)
                {
                    knownMethodNamePrefixes = OnlineResources.KnownMethodNamePrefixes;
                }

                return knownMethodNamePrefixes;
            }
        }
        #endregion

        #region "Has" methods
        protected bool hasCommonName()
        {
            bool ret = false;
            JObject jObject = JObject.Parse(this.KnownMethodNames);

            foreach (var item in jObject)
            {
                if (this.method.Name == item.Key)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        protected bool hasCommonPrefix()
        {
            bool ret = false;
            JObject jObject = JObject.Parse(this.KnownMethodNameprefixes);

            foreach (var item in jObject)
            {
                if (this.method.Name.StartsWith(item.Key))
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
        #endregion

        #region "Get known" methods
        protected string getKnownMethodNameDescription()
        {
            JObject jObject = JObject.Parse(this.KnownMethodNames);

            return jObject[this.method.Name].ToString();
        }

        protected string getKnownMethodNamePrefixDescription()
        {
            JObject jObject = JObject.Parse(this.KnownMethodNameprefixes);
            string description = string.Empty;

            foreach (var item in jObject)
            {
                if (this.method.Name.StartsWith(item.Key))
                {
                    description = item.Value.ToString();
                    break;
                }
            }

            return description;
        }
        #endregion

        public TagSummary(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            return true;
        }

        public string getTagValue()
        {
            string tagValue = string.Empty;
            //string prefix = string.Empty;
            //string typescription = string.Empty;
            //string typeText = string.Empty;

            if (this.hasCommonName())
            {
                tagValue = this.getKnownMethodNameDescription();
            }
            else if (this.hasCommonPrefix())
            {
                tagValue = this.getKnownMethodNameDescription();
            }
            else
            {
                // Todo Manage loggin message when method name or prefix is unknown.
                tagValue = "";
            }

            return string.Format(tag, tagValue);
        }

        
    }

    public class TagParam : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <param name=\"{0}\">{1}</param>\n";

        public TagParam(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            return false;
        }

        public string getTagValue()
        {
            string tagValues = string.Empty;
            string paramDescription = string.Empty;
            string paramTypeText = string.Empty;

            // {0} = Parameter name
            // {1} = Parameter description (class name, resolved label, method name, etc)
            string baseFormat = "{0} {1} {2}";

            foreach (AxMethodParameter param in this.method.Parameters)
            {
                switch (param.Type)
                {
                    case CompilerBaseType.ExtendedDataType:
                        AxEdt axEdt = Utils.MetadataProvider.Edts.Read(this.method.ReturnType.TypeName);

                        paramDescription = Utils.ResolveLabel(axEdt.Label).ToLower();
                        paramTypeText = "value";
                        break;
                    case CompilerBaseType.Record:
                        AxTable axTable = Utils.MetadataProvider.Tables.Read(this.method.ReturnType.TypeName);
                        paramDescription = $"{Utils.ResolveLabel(axTable.Label).ToLower()} (<c>{this.method.ReturnType.TypeName}</c>)";
                        paramTypeText = "record";
                        break;
                    case CompilerBaseType.Class:
                        paramDescription = this.method.ReturnType.TypeName;
                        paramTypeText = "class object";
                        break;
                    default:
                        break;
                }

                tagValues += string.Format(tag, param.Name, string.Format(baseFormat, paramDescription, paramTypeText));
            }

            return tagValues;
        }
    }

    public class TagException : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <exception cref=\"{0}\">{1}</exception>\n";

        public TagException(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            return false;
        }

        public string getTagValue()
        {
            string ret = tag;

            return ret;
        }
    }

    
    public class TagReturns : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <returns>{0}</returns>\n";

        public TagReturns(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            return false;
        }

        public string getTagValue()
        {
            string ret = tag;

            return ret;
        }
    }

    public class TagRemarks : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <remarks>{0}</remarks>\n";

        public TagRemarks(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            return false;
        }

        public string getTagValue()
        {
            string ret = tag;

            return ret;
        }
    }

    class Utils
    {
        static public Microsoft.Dynamics.AX.Metadata.Providers.IMetadataProvider MetadataProvider
        {
            get
            {
                return DesignMetaModelService.Instance.CurrentMetadataProvider;
            }
        }

        static public string ResolveLabel(string label)
        {
            var labelResolver = CoreUtility.ServiceProvider.GetService(typeof(Microsoft.Dynamics.Framework.Tools.Integration.Interfaces.ILabelResolver)) as Microsoft.Dynamics.Framework.Tools.Integration.Interfaces.ILabelResolver;

            if (labelResolver != null)
            {
                return labelResolver.GetLabelText(label);
            }
            return label;
        }

        static public string splitUpperCases(string text)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(text, " ");
        }
    }
}

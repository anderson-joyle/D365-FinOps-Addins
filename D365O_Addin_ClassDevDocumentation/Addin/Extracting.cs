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
using Resourcing;

namespace Extracting
{
    public interface ITagExtractor
    {
        string getTagValue();
        bool validate();
    }

    public class SummaryExtractor: ITagExtractor
    {
        protected AxMethod method;
        protected OnlineResources resource;

        #region Constants
        protected const string tag = "\r/// <summary>\r\n/// {0}\r\n/// </summary>";
        #endregion

        public SummaryExtractor(AxMethod method)
        {
            this.method = method;

            resource = new OnlineResources();
        }

        public bool validate()
        {
            return true;
        }

        public string getTagValue()
        {
            string tagValue = string.Empty;

            if (this.resource.hasKnownName(this.method.Name))
            {
                tagValue = this.resource.getKnownNameDescription(this.method.Name);
            }
            else if (this.resource.hasKnownNamePrefix(this.method.Name))
            {
                tagValue = this.resource.getKnownNamePrefixDescription(this.method.Name);

                tagValue = string.Format(tagValue, Utils.splitUpperCases(this.method.Name.Replace(this.resource.getnownNamePrefix(this.method.Name), "")).ToLower());
            }
            else
            {
                // Todo Manage loggin message when method name or prefix is unknown.
                tagValue = "";
            }

            return string.Format(tag, tagValue);
        }
    }

    public class ParamExtractor : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <param name = \"{0}\">{1}</param>";

        public ParamExtractor(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            bool ret = false;

            if (this.method.Parameters.Count > 0)
            {
                ret = true;
            }

            return ret;
        }

        public string getTagValue()
        {
            string tagValues = string.Empty;
            string paramDescription = string.Empty;
            string paramTypeText = string.Empty;

            foreach (AxMethodParameter param in this.method.Parameters)
            {
                tagValues += string.Format(tag, param.Name, Utils.MessageFromCompilerBaseType(param.Type, param.TypeName));
            }

            return tagValues;
        }
    }

    public class ExceptionExtractor : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <exception cref=\"{0}\">{1}</exception>\n";

        public ExceptionExtractor(AxMethod method)
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

    public class ReturnsExtractor : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <returns>{0}</returns>";

        public ReturnsExtractor(AxMethod method)
        {
            this.method = method;
        }

        public bool validate()
        {
            bool ret = false;

            if (this.method.ReturnType.Type != CompilerBaseType.Void)
            {
                ret = true;
            }

            return ret;
        }

        public string getTagValue()
        {
            string tagValues = string.Empty;

            tagValues = string.Format(tag, Utils.MessageFromCompilerBaseType(this.method.ReturnType.Type, this.method.ReturnType.TypeName));
                
            return tagValues;
        }
    }

    public class RemarksExtractor : ITagExtractor
    {
        protected AxMethod method;
        protected const string tag = "/// <remarks>{0}</remarks>\n";

        public RemarksExtractor(AxMethod method)
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

        static public string MessageFromCompilerBaseType(CompilerBaseType type, string typeName)
        {
            string ret = string.Empty;

            switch (type)
            {
                case CompilerBaseType.ExtendedDataType:
                    AxEdt axEdt = Utils.MetadataProvider.Edts.Read(typeName);
                    ret = $"{Utils.ResolveLabel(axEdt.Label).ToLower()} value";
                    break;
                case CompilerBaseType.Record:
                    AxTable axTable = Utils.MetadataProvider.Tables.Read(typeName);
                    ret = $"A <c>{typeName}</c> table record";
                    break;
                case CompilerBaseType.Class:
                    ret = $"A <c>{typeName}</c> class instance";
                    break;
                default:
                    break;
            }

            return ret;
        }
    }
}

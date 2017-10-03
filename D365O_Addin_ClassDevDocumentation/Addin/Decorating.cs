using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extracting;
using Microsoft.Dynamics.AX.Metadata.MetaModel;

namespace Decorating
{
    public interface IContent
    {
        string getContent();
    }

    public class SummaryTag : IContent
    {
        #region Global variables
        protected IContent content = null;
        protected AxMethod method = null;
        #endregion

        public SummaryTag(AxMethod method)
        {
            this.method = method;
        }

        public string getContent()
        {
            SummaryExtractor extractor = new SummaryExtractor(this.method);

            return extractor.getTagValue();
        }
    }

    public class ParamTag : IContent
    {
        #region Global variables
        protected IContent content = null;
        protected AxMethod method = null;
        #endregion

        public ParamTag(IContent content, AxMethod method)
        {
            this.content = content;
            this.method = method;
        }

        public string getContent()
        {
            ParamExtractor extractor = new ParamExtractor(this.method);
            string ret = string.Empty;

            if (extractor.validate())
            {
                ret = string.Format("{0}\n{1}", this.content.getContent(), extractor.getTagValue());
            }
            else
            {
                ret = this.content.getContent();
            }

            return ret;
        }
    }

    public class ExceptionTag : IContent
    {
        #region Global variables
        protected IContent content = null;
        protected AxMethod method = null;
        #endregion

        public ExceptionTag(IContent content, AxMethod method)
        {
            this.content = content;
            this.method = method;
        }

        public string getContent()
        {
            ExceptionExtractor extractor = new ExceptionExtractor(this.method);
            string ret = string.Empty;

            if (extractor.validate())
            {
                ret = string.Format("{0}\n{1}", this.content.getContent(), extractor.getTagValue());
            }
            else
            {
                ret = this.content.getContent();
            }

            return ret; ;
        }
    }

    public class ReturnsTag : IContent
    {
        #region Global variables
        protected IContent content = null;
        protected AxMethod method = null;
        #endregion

        public ReturnsTag(IContent content, AxMethod method)
        {
            this.content = content;
            this.method = method;
        }

        public string getContent()
        {
            ReturnsExtractor extractor = new ReturnsExtractor(this.method);
            string ret = string.Empty;

            if (extractor.validate())
            {
                ret = string.Format("{0}\n\r{1}", this.content.getContent(), extractor.getTagValue());
            }
            else
            {
                ret = this.content.getContent();
            }

            return ret;
        }
    }

    public class RemarksTag : IContent
    {
        #region Global variables
        protected IContent content = null;
        protected AxMethod method = null;
        #endregion

        public RemarksTag(IContent content, AxMethod method)
        {
            this.content = content;
            this.method = method;
        }

        public string getContent()
        {
            RemarksExtractor extractor = new RemarksExtractor(this.method);
            string ret = string.Empty;

            if (extractor.validate())
            {
                ret = string.Format("{0}\n\r{1}", this.content.getContent(), extractor.getTagValue());
            }
            else
            {
                ret = this.content.getContent();
            }

            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Decorating;
using Elementing;

namespace Building
{
    public class Engine
    {
        #region Variable
        protected List<SingleElement> elements = null;
        protected SolutionElement solutionElement = null;
        #endregion

        #region Properties
        protected string htmlContent = string.Empty;
        protected string HTMLContent
        {
            get
            {
                if (htmlContent == string.Empty)
                {
                    this.htmlContent = this.generateHTMLContent();
                }

                return this.htmlContent;
            }
        }
        #endregion

        public void setElements(List<SingleElement> elements)
        {
            this.elements = new List<SingleElement>();
            this.elements.AddRange(elements);
        }

        public void setSolution(SolutionElement solution)
        {
            this.solutionElement = solution;
        }

        public void show()
        {
            string fileName = string.Empty;

            fileName = this.save();
            this.open(fileName);
        }

        protected string save()
        {
            string fileName = string.Format("{0}.html", Path.GetTempFileName());

            using (var stream = new StreamWriter(fileName))
            {
                stream.Write(this.HTMLContent);
                stream.Close();
            }

            return fileName;
        }

        protected void open(string fileName)
        {
            System.Diagnostics.Process.Start(fileName);
        }

        public string generateHTMLContent()
        {
            IContent content;

            content = new OpenHTMLContent();
            content = new HeaderHTMLContent(content);
            content = new OpenBodyContent(content);
            content = new HeaderContent(content, this.solutionElement, this.elements);
            content = new OpenContainerContent(content);
            //content = new AxEnumContent(content, this.elements);
            //content = new AxEdtContent(content, this.elements);
            content = new AxTableContent(content, this.elements);
            content = new FooterContent(content);
            content = new CloseContainerContent(content);
            content = new CloseBodyContent(content);
            content = new CloseHTMLContent(content);

            return content.getContent();
        }
    }
}

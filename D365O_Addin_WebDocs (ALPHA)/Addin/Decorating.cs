using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elementing;
using Formatting;

namespace Decorating
{
    public interface IContent
    {
        string getContent();
    }

    #region HTML contents
    public class OpenHTMLContent : IContent
    {
        public string getContent()
        {
            return HTMLContent.OpenHTML;
        }
    }

    public class CloseHTMLContent : IContent
    {
        protected IContent content = null;

        public CloseHTMLContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.CloseHTML);
        }
    }

    public class HeaderHTMLContent : IContent
    {
        protected IContent content = null;

        public HeaderHTMLContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.HeaderHTML);
        }
    }

    public class OpenBodyContent : IContent
    {
        protected IContent content = null;

        public OpenBodyContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.OpenBody);
        }
    }

    public class CloseBodyContent : IContent
    {
        protected IContent content = null;

        public CloseBodyContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.CloseBody);
        }
    }

    public class OpenContainerContent : IContent
    {
        protected IContent content = null;

        public OpenContainerContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.OpenContainer);
        }
    }

    public class CloseContainerContent : IContent
    {
        protected IContent content = null;

        public CloseContainerContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.CloseContainer);
        }
    }

    public class FooterContent : IContent
    {
        protected IContent content = null;

        public FooterContent(IContent content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return string.Format("{0} {1}", this.content.getContent(), HTMLContent.Footer);
        }
    }
    #endregion

    #region Extended data types
    public class AxEdtContent : IContent
    {
        protected IContent content = null;
        protected List<SingleElement> selectedElements = null;

        public AxEdtContent(IContent content, List<SingleElement> elements)
        {
            this.content = content;
            this.selectedElements = elements.Where(element => element.Type.StartsWith("AxEdt")).ToList<SingleElement>();
        }

        public string getContent()
        {
            string htmlContent = string.Empty;

            if (this.validate())
            {
                htmlContent = "<h1>AxEdtString CONTENT</h1>\n";

                foreach (SingleElement element in this.selectedElements)
                {
                    htmlContent += string.Format("<h3>{0}</h3> - ", element.Name);
                }
            }

            return string.Format("{0} {1}", this.content.getContent(), htmlContent);
        }

        protected bool validate()
        {
            bool ret = true;

            if (this.selectedElements.Count == 0)
            {
                ret = false;
            }

            return ret;
        }
    }
    #endregion

    public class HeaderContent : IContent
    {
        protected IContent content = null;
        protected SolutionElement solutionElement = null;
        protected List<SingleElement> elements = null;

        public HeaderContent(IContent content, SolutionElement solutionElement, List<SingleElement> elements)
        {
            this.content = content;
            this.solutionElement = solutionElement;
            this.elements = elements;
        }

        public string getContent()
        {
            string htmlContent = string.Empty;
            string projectsContent = string.Empty;

            foreach (var project in this.solutionElement.Projects)
            {
                projectsContent += $"{project.Name}: {project.ProjectItems.Count} items <br>";
            }

            if (this.validate())
            {
                htmlContent = string.Format(HTMLContent.Header, this.solutionElement.Name, projectsContent);
            }

            return string.Format("{0} {1}", this.content.getContent(), htmlContent);
        }

        protected bool validate()
        {
            bool ret = true;

            return ret;
        }
    }

    public class AxTableContent : IContent
    {
        protected IContent content = null;
        protected List<SingleElement> selectedElements = null;

        public AxTableContent(IContent content, List<SingleElement> elements)
        {
            this.content = content;
            this.selectedElements = elements.Where(element => element.Type == "AxTable").ToList<SingleElement>();
        }

        public string getContent()
        {
            string htmlContent = string.Empty;

            if (this.validate())
            {
                foreach (SingleElement element in this.selectedElements)
                {
                    AxTableHelper helper = new AxTableHelper(element.Name);

                    htmlContent += string.Format(HTMLContent.TableTag, helper.Label, element.Name, helper.DevDocument, helper.FormattedFields);
                }
            }

            return string.Format("{0} {1}", this.content.getContent(), htmlContent);
        }

        protected bool validate()
        {
            bool ret = true;

            if (this.selectedElements.Count == 0)
            {
                ret = false;
            }

            return ret;
        }
    }

    public class AxEnumContent : IContent
    {
        protected IContent content = null;
        protected List<SingleElement> selectedElements = null;

        public AxEnumContent(IContent content, List<SingleElement> elements)
        {
            this.content = content;
            this.selectedElements = elements.Where(element => element.Type == "AxEnum").ToList<SingleElement>();
        }

        public string getContent()
        {
            string htmlContent = string.Empty;

            if (this.validate())
            {
                htmlContent = "AxEnum CONTENT\n";

                foreach (SingleElement element in this.selectedElements)
                {
                    htmlContent += string.Format("{0} - ", element.Name);
                }
            }

            return string.Format("{0} {1}", this.content.getContent(), htmlContent);
        }

        protected bool validate()
        {
            bool ret = true;

            if (this.selectedElements.Count == 0)
            {
                ret = false;
            }

            return ret;
        }
    }
}

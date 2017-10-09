using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
using Operating;


namespace Elementing
{
    public interface IElement
    {
        void accept(IOperation operation);
    }

    public class Element
    {
        protected NamedElement element = null;

        public Element(NamedElement element)
        {
            this.element = element;
        }

        static public IElement construct(NamedElement namedElement)
        {
            IElement element = null;

            #region Chose element by type
            switch (namedElement.GetType().Name)
            {
                case "Table":
                    element = new ElementTable(namedElement);
                    break;
                case "TableExtension":
                    element = new ElementTableExtension(namedElement);
                    break;
                case "View":
                    element = new ElementView(namedElement);
                    break;
                case "ClassItem":
                    element = new ElementClassItem(namedElement);
                    break;
                case "SimpleQuery":
                    element = new ElementSimpleQuery(namedElement);
                    break;
                case "CompositeQuery":
                    element = new ElementCompositeQuery(namedElement);
                    break;
                case "Form":
                    element = new ElementForm(namedElement);
                    break;
                case "FormExtension":
                    element = new ElementFormExtension(namedElement);
                    break;
                case "DataEntityView":
                    element = new ElementDataEntityView(namedElement);
                    break;

                default:
                    throw new NotImplementedException($"Not implemented type: {namedElement.GetType().Name}");
            }
            #endregion

            return element;
        }
    }

    public class ElementTable : Element, IElement
    {
        public ElementTable(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitTable(this.element as Table);
        }
    }

    public class ElementTableExtension : Element, IElement
    {
        public ElementTableExtension(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitTableExtension(this.element as TableExtension);
        }
    }

    public class ElementView : Element, IElement
    {
        public ElementView(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitView(this.element as View);
        }
    }

    public class ElementClassItem : Element, IElement
    {
        public ElementClassItem(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitClass(this.element as ClassItem);
        }
    }

    public class ElementSimpleQuery : Element, IElement
    {
        public ElementSimpleQuery(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitSimpleQuery(this.element as SimpleQuery);
        }
    }

    public class ElementCompositeQuery : Element, IElement
    {
        public ElementCompositeQuery(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitCompositeQuery(this.element as CompositeQuery);
        }
    }

    public class ElementForm : Element, IElement
    {
        public ElementForm(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitForm(this.element as Form);
        }
    }

    public class ElementFormExtension : Element, IElement
    {
        public ElementFormExtension(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitFormExtension(this.element as FormExtension);
        }
    }

    public class ElementDataEntityView : Element, IElement
    {
        public ElementDataEntityView(NamedElement element) : base(element)
        {
        }

        public void accept(IOperation operation)
        {
            operation.visitDataEntity(this.element as DataEntityViewBase);
        }
    }
}

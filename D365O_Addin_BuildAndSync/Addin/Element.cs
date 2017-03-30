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

namespace Element
{
    /// <summary>
    /// Element interface
    /// </summary>
    /// <remarks>Visitee class</remarks>
    public interface IElement
    {
        void accept(NamedElement namedElement, Operation.ElementOperation operation);
    }

    public class ElementTable : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            Table table = namedElement as Table;

            operation.visitTable(table, this);
        }
    }

    public class ElementTableExtension : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            TableExtension tableExtension = namedElement as TableExtension;

            operation.visitTableExtension(tableExtension, this);
        }
    }

    public class ElementView : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            View view = namedElement as View;

            operation.visitView(view, this);
        }
    }

    public class ElementClass : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            ClassItem classItem = namedElement as ClassItem;

            operation.visitClass(classItem, this);
        }
    }

    public class ElementSimpleQuery : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            SimpleQuery simpleQuery = namedElement as SimpleQuery;

            operation.visitSimpleQuery(simpleQuery, this);
        }
    }

    public class ElementCompositeQuery : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            CompositeQuery compositeQuery = namedElement as CompositeQuery;

            operation.visitCompositeQuery(compositeQuery, this);
        }
    }

    public class ElementForm : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            Form form = namedElement as Form;

            operation.visitForm(form, this);
        }
    }

    public class ElementFormExtension : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            FormExtension formExtension = namedElement as FormExtension;

            operation.visitFormExtension(formExtension, this);
        }
    }

    public class ElementDataEntity : IElement
    {
        public void accept(NamedElement namedElement, Operation.ElementOperation operation)
        {
            DataEntityViewBase dataEntity = namedElement as DataEntityViewBase;

            operation.visitDataEntity(dataEntity, this);
        }
    }
}

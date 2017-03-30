namespace Addin
{
    using System;
    using System.Linq;
    using System.ComponentModel.Composition;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Classes;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
    using Element;

    using Metadata = Microsoft.Dynamics.AX.Metadata;

    /// <summary>
    /// Build and sync a single element. This add-in implements Visitor design pattern.
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITableExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IView))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ISimpleQuery))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ICompositeQuery))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IDataEntity))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IClassItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IFormExtension))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "Sync single element with database";

        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.DesignerAddinCaption;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return DesignerContextMenuAddIn.addinName;
            }
        }

        /// <summary>
        /// Gets a singleton instance of the metadata provider that can access the metadata repository.
        /// Any metadata, irrespective of whether it is part of what is being edited by VS, is available
        /// through this provider.
        /// </summary>
        public Metadata.Providers.IMetadataProvider MetadataProvider
        {
            get
            {
                if (this.metadataProvider == null)
                {
                    this.metadataProvider = DesignMetaModelService.Instance.CurrentMetadataProvider;
                }

                return this.metadataProvider;
            }
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinDesignerEventArgs e)
        {
            try
            {
                NamedElement namedElement = e.SelectedElement as NamedElement;

                Operation.BuildElementOperation build;
                Operation.SyncElementOperation sync;

                build = new Operation.BuildElementOperation();
                sync = new Operation.SyncElementOperation();

                #region Chose element by type
                switch (namedElement.GetType().Name)
                {
                    // Build and sync
                    case "Table":
                        ElementTable table = new ElementTable();
                        
                        table.accept(namedElement, build);
                        table.accept(namedElement, sync);
                        break;

                    case "TableExtension":
                        ElementTableExtension tableExtension = new ElementTableExtension();

                        tableExtension.accept(namedElement, build);
                        tableExtension.accept(namedElement, sync);
                        break;

                    // Build and sync
                    case "View":
                        ElementView view = new ElementView();

                        view.accept(namedElement, build);
                        view.accept(namedElement, sync);
                        break;

                    // Build
                    case "ClassItem":
                        ElementClass classItem = new ElementClass();

                        classItem.accept(namedElement, build);
                        break;

                    // Build
                    case "SimpleQuery":
                        ElementSimpleQuery simpleQuery = new ElementSimpleQuery();

                        simpleQuery.accept(namedElement, build);
                        break;

                    // Build
                    case "CompositeQuery":
                        ElementCompositeQuery compositeQuery = new ElementCompositeQuery();

                        compositeQuery.accept(namedElement, build);
                        break;

                    // Build
                    case "Form":
                        ElementForm form = new ElementForm();

                        form.accept(namedElement, build);
                        break;

                    // Build
                    case "FormExtension":
                        ElementFormExtension formExtension = new ElementFormExtension();

                        formExtension.accept(namedElement, build);
                        break;

                    // Build and sync
                    case "DataEntityView":
                        ElementDataEntity dataEntity = new Element.ElementDataEntity();

                        dataEntity.accept(namedElement, build);
                        dataEntity.accept(namedElement, sync);
                        break;

                    default:
                        throw new NotImplementedException($"Not implemented type: {namedElement.GetType().Name}");
                }
                #endregion
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}

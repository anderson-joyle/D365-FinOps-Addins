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
    //using Metadata = Microsoft.Dynamics.AX.Metadata;

    using Elementing;
    using Operating;

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
                IElement element = Element.construct(e.SelectedElement as NamedElement);

                BPCheck bpCheck = new BPCheck();
                Build build = new Build();
                Sync sync = new Sync();

                element.accept(bpCheck);
                element.accept(build);
                element.accept(sync);
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}

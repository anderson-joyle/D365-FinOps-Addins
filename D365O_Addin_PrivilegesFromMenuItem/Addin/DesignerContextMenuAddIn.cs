namespace Addin
{
    using System;
    using System.Linq;
    using System.ComponentModel.Composition;
    using Microsoft.Dynamics.AX.Metadata.Core;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Forms;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

    /// <summary>
    /// Creates privileges from current menu item
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IMenuItem))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "D365O_Addin_PrivilegesFromMenuItem";
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
                MenuItem menuItem = e.SelectedElement as MenuItem;

                if (menuItem != null)
                {
                    Building.PrivilegeEngine engine = new Building.PrivilegeEngine(menuItem);
                    engine.run();
                }
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion
    }
}

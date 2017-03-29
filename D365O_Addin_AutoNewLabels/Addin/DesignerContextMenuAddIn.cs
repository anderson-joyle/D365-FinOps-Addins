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
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Views;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.BaseTypes;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.LabelFiles;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Workflows;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Security;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

    /// <summary>
    /// Created new labels automaticlly when prefixing them as @@@
    /// </summary>
    [Export(typeof(IDesignerMenu))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ILabelFile))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ITable))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IEdtBase))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnum))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IBaseEnumExtension))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IMenuItem))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IView))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IForm))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(ISecurityPrivilege))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowHierarchyAssignmentProvider))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowApproval))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTask))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowCategory))]
    [DesignerMenuExportMetadata(AutomationNodeType = typeof(IWorkflowTemplate))]
    public class DesignerContextMenuAddIn : DesignerMenuBase
    {
        #region Member variables
        private const string addinName = "DesignerAddin";
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return "Create new labels";
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
                string logging = string.Empty;

                foreach (NamedElement element in e.SelectedElements)
                {
                    Building.CreateLabels labels = Building.CreateLabels.construct(element);

                    labels.run();

                    logging += labels.getLoggingMessage();
                    logging += "\n";
                }

                CoreUtility.DisplayInfo(logging);
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        } 
        #endregion
    }
}

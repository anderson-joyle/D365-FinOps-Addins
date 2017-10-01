namespace Addin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using Microsoft.Dynamics.Framework.Tools.Extensibility;
    using Microsoft.Dynamics.Framework.Tools.MetaModel;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
    using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
    using Microsoft.Dynamics.Framework.Tools.ProjectSupport;
    using ProjectSystem = Microsoft.Dynamics.Framework.Tools.ProjectSystem;
    using ProjectSupport = Microsoft.Dynamics.Framework.Tools.ProjectSupport;

    using Elementing;
    using System.IO;
    using System.Diagnostics;


    /// <summary>
    /// Extract technical documentation blocks in order to help during documentation process
    /// </summary>
    [Export(typeof(IMainMenu))]
    public class MainMenuAddIn : MainMenuBase
    {
        #region Member variables
        private const string addinName = "Addin";
        #endregion

        #region Properties
        /// <summary>
        /// Caption for the menu item. This is what users would see in the menu.
        /// </summary>
        public override string Caption
        {
            get
            {
                return AddinResources.MainMenuAddInCaption;
            }
        }

        /// <summary>
        /// Unique name of the add-in
        /// </summary>
        public override string Name
        {
            get
            {
                return MainMenuAddIn.addinName;
            }
        }

        #endregion

        #region Callbacks
        /// <summary>
        /// Called when user clicks on the add-in menu
        /// </summary>
        /// <param name="e">The context of the VS tools and metadata</param>
        public override void OnClick(AddinEventArgs e)
        {
            try
            {
                List<EnvDTE.Project> list = CoreUtility.GetDynamicsProjectsInSolution().ToList<EnvDTE.Project>();
                List<ProjectSystem.VSProjectFileNode> nodes;
                List<SingleElement> elements = new List<SingleElement>();

                SolutionElement solutionElement = new SolutionElement();

                if (list.Count == 0)
                {
                    throw new Exception("Make sure you have a active solution and it has projects on it.");
                }
                EnvDTE.DTE dte = CoreUtility.ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                
                solutionElement.Name = Path.GetFileName(dte.Solution.FullName);

                nodes = new List<ProjectSystem.VSProjectFileNode>();

                foreach (ProjectSystem.OAVSProject project in list.Where(item => Guid.Parse(item.Kind).ToString() == Guid.Parse(CoreConstants.GuidVSProjectString).ToString()))
                {
                    solutionElement.Projects.Add(project);
                    this.findElements(project.ProjectItems, nodes);
                }
                
                foreach (ProjectSystem.VSProjectFileNode item in nodes)
                {
                    SingleElement singleElement = new SingleElement();

                    singleElement.Name = item.MetadataReference.Name;
                    singleElement.Type = item.MetadataReference.MetadataType.Name;
                    singleElement.MetadataReference = item.MetadataReference;

                    elements.Add(singleElement);
                }

                Building.Engine engine = new Building.Engine();

                engine.setSolution(solutionElement);
                engine.setElements(elements);
                engine.generateHTMLContent();
                engine.show();
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
        #endregion

        private void findElements(EnvDTE.ProjectItems items, List<ProjectSystem.VSProjectFileNode> nodes)
        {
            foreach (EnvDTE.ProjectItem item in items)
            {
                if (item.ProjectItems.Count > 0)
                {
                    this.findElements(item.ProjectItems, nodes);
                }

                if (item.Object is ProjectSystem.VSProjectFileNode)
                {
                    nodes.Add(item.Object as ProjectSystem.VSProjectFileNode);
                }
            }

        }
    }
}

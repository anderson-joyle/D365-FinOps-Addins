using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Menus;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace Building
{
    public class PrivilegeEngine
    {
        protected MenuItem menuItem;
        public PrivilegeEngine(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }

        /// <summary>
        /// Execute routine
        /// </summary>
        public void run()
        {
            UserInterface userInterface = new UserInterface();

            userInterface.ShowDialog();

            if (!userInterface.closeOk)
            {
                return;
            }

            foreach (var item in userInterface.checkedItems())
            {
                AccessGrant grant;
                string newName = string.Empty;
                string sufix = string.Empty;

                switch (item.ToString().ToUpper())
                {
                    case "UNSET":
                        sufix = "Unset";
                        grant = AccessGrant.ConstructGrantAll();
                        break;
                    case "NO ACCESS":
                        sufix = "NoAccess";
                        grant = AccessGrant.ConstructDenyAll();
                        break;
                    case "READ":
                        sufix = "View";
                        grant = AccessGrant.ConstructGrantRead();
                        break;
                    case "UPDATE":
                        sufix = "Update";
                        grant = AccessGrant.ConstructGrantUpdate();
                        break;
                    case "CREATE":
                        sufix = "Create";
                        grant = AccessGrant.ConstructGrantCreate();
                        break;
                    case "CORRECT":
                        sufix = "Correct";
                        grant = AccessGrant.ConstructGrantCorrect();
                        break;
                    case "DELETE":
                        sufix = "Maintain";
                        grant = AccessGrant.ConstructGrantDelete();
                        break;
                    default:
                        throw new NotImplementedException($"Menu item object type {this.menuItem.ObjectType} is not implemented.");
                }

                newName = $"{this.menuItem.Name}{sufix}";

                this.create(newName, grant);
            }

        }

        /// <summary>
        /// Create privilege in AOT
        /// </summary>
        /// <param name="name">Privilege's name</param>
        /// <param name="grant">User chosen privilege access level</param>
        /// <remarks>This method could be improved. Most probably are better ways to achieve this goal.</remarks>
        protected void create(string name, AccessGrant grant)
        {
            AxSecurityPrivilege privilege = new AxSecurityPrivilege();
            AxSecurityEntryPointReference entryPoint = new AxSecurityEntryPointReference();
            ModelInfo modelInfo;
            ModelSaveInfo modelSaveInfo = new ModelSaveInfo();
            VSProjectNode project = Utils.LocalUtils.GetActiveProject();

            #region Create entry point 
            entryPoint.Name = this.menuItem.Name;
            entryPoint.Grant = grant;
            entryPoint.ObjectName = this.menuItem.Name;
            switch (this.menuItem.ObjectType)
            {
                case MenuItemObjectType.Form:
                    entryPoint.ObjectType = EntryPointType.MenuItemDisplay;
                    break;
                case MenuItemObjectType.Class:
                    entryPoint.ObjectType = EntryPointType.MenuItemAction;
                    break;
                case MenuItemObjectType.SSRSReport:
                    entryPoint.ObjectType = EntryPointType.MenuItemOutput;
                     break;
                default:
                    throw new NotImplementedException($"Menuitem object type {this.menuItem.ObjectType} is not implemented.");
            }
            
            #endregion

            #region Create privilege
            privilege.Name = name;
            privilege.EntryPoints.Add(entryPoint);
            #endregion

            // Most probably there is a better way to do this part.
            #region Add to AOT
            modelInfo = project.GetProjectsModelInfo();

            modelSaveInfo.Id = modelInfo.Id;
            modelSaveInfo.Layer = modelInfo.Layer;

            var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            var metaModelService = metaModelProviders.CurrentMetaModelService;

            metaModelService.CreateSecurityPrivilege(privilege, modelSaveInfo);
            #endregion

            this.appendToProject(privilege);
        }

        /// <summary>
        /// Append created privilege to active project
        /// </summary>
        /// <param name="privilege">Recently created privilege</param>
        /// <remarks>This method could be improved. Most probably are better ways to achieve this goal.</remarks>
        protected void appendToProject(AxSecurityPrivilege privilege)
        {
            var projectService = ServiceLocator.GetService(typeof(IDynamicsProjectService)) as IDynamicsProjectService;
            projectService.AddElementToActiveProject(privilege);
        }
    }
}

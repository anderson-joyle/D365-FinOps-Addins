using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

using Metadata = Microsoft.Dynamics.AX.Metadata;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using EnvDTE;
using System.Globalization;

namespace Building
{ 
    public class NewFieldEngine
    {
        #region Member variables
        protected Addin.Controlling controller;
        protected NamedElement namedElement;
        protected bool edtExist;

        protected FieldType fieldType;
        protected string fieldName;
        protected string edtText;
        protected string extendsText;

        protected string labelText;
        protected string helpTextText;
        #endregion

        #region Properties
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;
        private Metadata.Service.IMetaModelService metaModelService = null;
        private Metadata.MetaModel.ModelSaveInfo modelSaveInfo = null;

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

        public Metadata.Service.IMetaModelService MetaModelService
        {
            get
            {
                if (this.metaModelService == null)
                {
                    this.metaModelService = DesignMetaModelService.Instance.CurrentMetaModelService;
                }
                return this.metaModelService;
            }
        }

        public Metadata.MetaModel.ModelSaveInfo ModelSaveInfo
        {
            get
            {
                Metadata.MetaModel.ModelInfo modelInfo;

                if (this.modelSaveInfo == null)
                {
                    if (this.namedElement is Table)
                    {
                        modelInfo = this.MetaModelService.GetTableModelInfo(this.namedElement.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();
                    }
                    else
                    {
                        modelInfo = this.MetaModelService.GetTableExtensionModelInfo(this.namedElement.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();
                    }

                    modelSaveInfo = new Metadata.MetaModel.ModelSaveInfo(modelInfo);
                }

                return modelSaveInfo;
            }
        }
        #endregion

        public NewFieldEngine(Addin.Controlling controller, NamedElement namedElement)
        {
            this.controller = controller;
            this.namedElement = namedElement;

            Enum.TryParse<FieldType>(this.controller.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

            this.fieldName    = this.controller.textBoxFieldName.Text;
            this.edtText      = this.controller.comboBoxEDTName.Text;
            this.extendsText  = this.controller.comboBoxExtends.Text;
            this.labelText    = this.controller.textBoxLabel.Text;
            this.helpTextText = this.controller.textBoxHelpText.Text;
        }

        public void run()
        {
            AxTableField axTableField;
            AxEdt axEdt;

            axTableField = this.buildField();
            axEdt = this.buildEdt();

            if (!edtExist && axEdt != null)
            {
                this.addGeneralPropertiesToEdt(axEdt);
                this.addSpecificPropertiesToEdt(axEdt);

                this.MetaModelService.CreateExtendedDataType(axEdt, this.ModelSaveInfo);
                this.appendToProject(axEdt);
            }

            this.addGeneralPropertiesToField(axTableField, axEdt);
            this.addSpecificPropertiesToField(axTableField);
            this.addField(axTableField);
            
        }

        public AxTableField buildField()
        {
            AxTableField axTableField;

            switch (this.fieldType)
            {
                case FieldType.String:
                    axTableField = new Metadata.MetaModel.AxTableFieldString();
                    break;
                case FieldType.Integer:
                    axTableField = new Metadata.MetaModel.AxTableFieldInt();
                    break;
                case FieldType.Real:
                    axTableField = new Metadata.MetaModel.AxTableFieldReal();
                    break;
                case FieldType.DateTime:
                    axTableField = new Metadata.MetaModel.AxTableFieldUtcDateTime();
                    break;
                case FieldType.Guid:
                    axTableField = new Metadata.MetaModel.AxTableFieldGuid();
                    break;
                case FieldType.Int64:
                    axTableField = new Metadata.MetaModel.AxTableFieldInt64();
                    break;
                case FieldType.Enum:
                    axTableField = new Metadata.MetaModel.AxTableFieldEnum();
                    break;
                case FieldType.Time:
                    axTableField = new Metadata.MetaModel.AxTableFieldTime();
                    break;
                case FieldType.Container:
                    axTableField = new Metadata.MetaModel.AxTableFieldContainer();
                    break;
                case FieldType.Memo:
                    axTableField = new Metadata.MetaModel.AxTableFieldString();
                    break;
                case FieldType.Date:
                    axTableField = new Metadata.MetaModel.AxTableFieldDate();
                    break;
                default:
                    throw new NotImplementedException($"Field type {this.controller.comboBoxFieldType.ToString()} is not supported");
            }

            return axTableField;
        }

        public Metadata.MetaModel.AxEdt buildEdt()
        {
            Metadata.MetaModel.AxEdt edt;

            edt = this.MetadataProvider.Edts.Read(this.edtText);

            if (edt != null)
            {
                edtExist = true;
            }
            else
            {
                switch (this.fieldType)
                {
                    case FieldType.String:
                        edt = new Metadata.MetaModel.AxEdtString();
                        break;
                    case FieldType.Integer:
                        edt = new Metadata.MetaModel.AxEdtInt();
                        break;
                    case FieldType.Real:
                        edt = new Metadata.MetaModel.AxEdtReal();
                        break;
                    case FieldType.DateTime:
                        edt = new Metadata.MetaModel.AxEdtUtcDateTime();
                        break;
                    case FieldType.Guid:
                        edt = new Metadata.MetaModel.AxEdtGuid();
                        break;
                    case FieldType.Int64:
                        edt = new Metadata.MetaModel.AxEdtInt64();
                        break;
                    case FieldType.Enum:
                        edt = new Metadata.MetaModel.AxEdtEnum();
                        break;
                    case FieldType.Time:
                        edt = new Metadata.MetaModel.AxEdtTime();
                        break;
                    case FieldType.Container:
                        edt = new Metadata.MetaModel.AxEdtContainer();
                        break;
                    case FieldType.Memo:
                        edt = new Metadata.MetaModel.AxEdtString();
                        break;
                    case FieldType.Date:
                        edt = new Metadata.MetaModel.AxEdtDate();
                        break;
                    default:
                        throw new NotImplementedException($"Field type {this.controller.comboBoxFieldType.ToString()} is not supported");
                }

                edt.Name = this.edtText;
            }

            return edt;
        }

        protected void addField(Metadata.MetaModel.AxTableField field)
        {
            if (this.namedElement is Table)
            {
                AxTable axTable = this.MetadataProvider.Tables.Read(this.namedElement.Name);
                axTable.Fields.Add(field);

                this.MetadataProvider.Tables.Update(axTable, this.ModelSaveInfo);
            }
            else
            {
                var extensionName = this.namedElement.Name.Split('.');

                AxTableExtension axTableExtension = this.MetadataProvider.TableExtensions.Read(this.namedElement.Name);
                
                axTableExtension.Fields.Add(field);

                this.MetadataProvider.TableExtensions.Update(axTableExtension, this.ModelSaveInfo);
            }
        }

        protected void addGeneralPropertiesToEdt(AxEdt edt)
        {
            if (!edtExist)
            {
                if (this.extendsText != string.Empty)
                {
                    AxEdt edtLocal = this.MetadataProvider.Edts.Read(this.extendsText);

                    if (edtLocal != null)
                    {
                        edt.Extends = edtLocal.Name;
                    }
                }
                if (this.labelText != String.Empty)
                {
                    edt.Label = this.labelText;
                }
                if (this.helpTextText != String.Empty)
                {
                    edt.HelpText = this.helpTextText;
                }
            }
        }

        protected void addSpecificPropertiesToEdt(AxEdt edt)
        {
            FieldType fieldType;

            if (!edtExist)
            {
                Enum.TryParse<FieldType>(this.controller.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

                switch (fieldType)
                {
                    case FieldType.String:
                        Metadata.MetaModel.AxEdtString edtString = edt as Metadata.MetaModel.AxEdtString;
                        break;
                    case FieldType.Enum:
                        Metadata.MetaModel.AxEdtEnum edtEnum = edt as Metadata.MetaModel.AxEdtEnum;
                        edtEnum.EnumType = this.controller.comboBoxFieldType.Text;
                        break;
                    case FieldType.Memo:
                        edt.Extends = "FreeTxt";
                        break;
                    default:
                        break;
                }
            }
        }

        protected void addGeneralPropertiesToField(AxTableField axTableField, AxEdt edt)
        {
            axTableField.Name = this.controller.textBoxFieldName.Text;

            if (edt != null)
            {
                axTableField.ExtendedDataType = edt.Name;
            }

            if (edtExist)
            {
                if (this.labelText != String.Empty)
                {
                    axTableField.Label = this.labelText;
                }
                if (this.helpTextText != String.Empty)
                {
                    axTableField.HelpText = this.helpTextText;
                }
            }
        }

        protected void addSpecificPropertiesToField(Metadata.MetaModel.AxTableField axTableField)
        {
            return;
        }
        static VSProjectNode GetActiveProjectNode(DTE dte)
        {
            Array array = dte.ActiveSolutionProjects as Array;
            if (array != null && array.Length > 0)
            {
                Project project = array.GetValue(0) as Project;
                if (project != null)
                {
                    return project.Object as VSProjectNode;
                }
            }
            return null;
        }

        /// <summary>
        /// Append createds privilege to active project
        /// </summary>
        /// <param name="privilege">Recently created privilege</param>
        /// <remarks>This method could be improved. Most probably are better ways to achieve this goal.</remarks>
        protected void appendToProject(AxEdt edt)
        {
            DTE dte = CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE;
            if (dte == null)
            {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "No service for DTE found. The DTE must be registered as a service for using this API.", new object[0]));
            }
            VSProjectNode activeProjectNode = NewFieldEngine.GetActiveProjectNode(dte);

            activeProjectNode.AddModelElementsToProject(new List<MetadataReference>
                    {
                        new MetadataReference(edt.Name, edt.GetType())
                    });

            //var projectService = ServiceLocator.GetService(typeof(IDynamicsProjectService)) as IDynamicsProjectService;
            //projectService.AddElementToActiveProject(edt);
        }
    }
}

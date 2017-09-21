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

namespace Building
{ 
    public class NewFieldEngine
    {
        #region Member variables
        protected Addin.FormController controller;
        protected NamedElement namedElement;
        protected bool edtExist;
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

        public NewFieldEngine(Addin.FormController controller, NamedElement namedElement)
        {
            this.controller = controller;
            this.namedElement = namedElement;
        }

        public void run()
        {
            Metadata.MetaModel.AxTableField axTableField;
            Metadata.MetaModel.AxEdt axEdt;

            axTableField = this.buildField();
            axEdt = this.buildEdt();

            if (!edtExist)
            {
                this.addGeneralPropertiesToEdt(axEdt);
                this.addSpecificPropertiesToEdt(axEdt);

                this.MetaModelService.CreateExtendedDataType(axEdt, this.ModelSaveInfo);
            }

            this.addGeneralPropertiesToField(axTableField);
            this.addSpecificPropertiesToField(axTableField);
            this.addField(axTableField);
        }

        public Metadata.MetaModel.AxTableField buildField()
        {
            Metadata.MetaModel.AxTableField axTableField;
            FieldType fieldType;

            Enum.TryParse<FieldType>(this.controller.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

            switch (fieldType)
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
            FieldType fieldType;

            edt = this.MetadataProvider.Edts.Read(this.controller.textBoxEDTName.Text);

            if (edt != null)
            {
                edtExist = true;
            }
            else
            {
                Enum.TryParse<FieldType>(this.controller.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

                switch (fieldType)
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
            }

            return edt;
        }

        protected void addField(Metadata.MetaModel.AxTableField field)
        {
            if (this.namedElement is Table)
            {
                Metadata.MetaModel.AxTable axTable = this.MetadataProvider.Tables.Read(this.namedElement.Name);
                axTable.Fields.Add(field);

                this.MetaModelService.UpdateTable(axTable, this.ModelSaveInfo);
            }
            else
            {
                var extensionName = this.namedElement.Name.Split('.');

                Metadata.MetaModel.AxTableExtension axTableExtension = this.MetadataProvider.TableExtensions.Read(this.namedElement.Name);

                axTableExtension.Fields.Add(field);
            }
        }

        protected void addGeneralPropertiesToEdt(Metadata.MetaModel.AxEdt edt)
        {
            if (!edtExist)
            {
                edt.Name = this.controller.textBoxEDTName.Text;
                edt.Label = this.controller.textBoxLabel.Text;
                edt.HelpText = this.controller.textBoxHelpText.Text;
            }
        }

        protected void addSpecificPropertiesToEdt(Metadata.MetaModel.AxEdt edt)
        {
            FieldType fieldType;

            if (!edtExist)
            {
                Enum.TryParse<FieldType>(this.controller.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

                switch (fieldType)
                {
                    case FieldType.String:
                        Metadata.MetaModel.AxEdtString edtString = edt as Metadata.MetaModel.AxEdtString;
                        edtString.StringSize = (int)this.controller.numericUpDownStringSize.Value;
                        break;
                    case FieldType.Enum:
                        Metadata.MetaModel.AxEdtEnum edtEnum = edt as Metadata.MetaModel.AxEdtEnum;
                        edtEnum.EnumType = this.controller.comboBoxFieldType.Text;
                        break;
                    case FieldType.Memo:
                        edt.Extends = "FreTxt";
                        break;
                    default:
                        break;
                }
            }
        }

        protected void addGeneralPropertiesToField(Metadata.MetaModel.AxTableField axTableField)
        {
            axTableField.Name = this.controller.textBoxFieldName.Text;
            axTableField.ExtendedDataType = this.controller.textBoxEDTName.Text;

            if (edtExist)
            {
                axTableField.Label = this.controller.textBoxLabel.Text;
                axTableField.HelpText = this.controller.textBoxHelpText.Text;
            }
        }

        protected void addSpecificPropertiesToField(Metadata.MetaModel.AxTableField axTableField)
        {
            return;
        }
    }
}

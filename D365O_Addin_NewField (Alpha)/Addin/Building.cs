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
    public class NewField
    {
        public FieldType FieldType { set; get; }
        public string FieldName { set; get; }
        public string EdtName { set; get; }
    }

    public class NewFieldEngine
    {
        #region Member variables
        protected NewField newField;
        protected NamedElement namedElement;
        #endregion

        #region Properties
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Metadata.Providers.IMetadataProvider metadataProvider = null;
        private Metadata.Service.IMetaModelService metaModelService = null;

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
        #endregion

        public NewFieldEngine(NewField newField, NamedElement namedElement)
        {
            this.newField = newField;
            this.namedElement = namedElement;
        }

        public void run()
        {
            //Metadata.MetaModel.AxTable axTable = this.MetadataProvider.Tables.Read(this.table.Name);
            Metadata.MetaModel.AxTableField axTableField = null;
            Metadata.MetaModel.AxEdt axEdt = null;

            axEdt = this.MetadataProvider.Edts.Read(this.newField.EdtName);

            switch (this.newField.FieldType)
            {
                case FieldType.String:
                    axTableField = new Metadata.MetaModel.AxTableFieldString();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtString();
                    }
                    break;
                case FieldType.Integer:
                    axTableField = new Metadata.MetaModel.AxTableFieldInt();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtInt();
                    }
                    break;
                case FieldType.Real:
                    axTableField = new Metadata.MetaModel.AxTableFieldReal();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtReal();
                    }
                    break;
                case FieldType.DateTime:
                    axTableField = new Metadata.MetaModel.AxTableFieldUtcDateTime();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtUtcDateTime();
                    }
                    break;
                case FieldType.Guid:
                    axTableField = new Metadata.MetaModel.AxTableFieldGuid();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtGuid();
                    }
                    break;
                case FieldType.Int64:
                    axTableField = new Metadata.MetaModel.AxTableFieldInt64();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtInt64();
                    }
                    break;
                case FieldType.Enum:
                    axTableField = new Metadata.MetaModel.AxTableFieldEnum();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtEnum();
                    }
                    break;
                case FieldType.Time:
                    axTableField = new Metadata.MetaModel.AxTableFieldTime();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtString();
                    }
                    break;
                case FieldType.Container:
                    axTableField = new Metadata.MetaModel.AxTableFieldContainer();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtContainer();
                    }
                    break;
                case FieldType.Memo:
                    axTableField = new Metadata.MetaModel.AxTableFieldString();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtString();
                    }
                    break;
                case FieldType.Date:
                    axTableField = new Metadata.MetaModel.AxTableFieldDate();
                    if (axEdt == null)
                    {
                        axEdt = new Metadata.MetaModel.AxEdtDate();
                    }
                    break;
                default:
                    throw new NotImplementedException($"Field type {this.newField.FieldType} is not supported");
            }

            if (axEdt.Name == string.Empty)
            {
                axEdt.Name = this.newField.EdtName;
                this.MetaModelService.CreateExtendedDataType(axEdt, this.getModelSaveInfo());
            }

            axTableField.Name = this.newField.FieldName;
            axTableField.ExtendedDataType = axEdt.Name;

            if (this.namedElement is Table)
            {
                Metadata.MetaModel.AxTable axTable = this.MetadataProvider.Tables.Read(this.namedElement.Name);
                axTable.Fields.Add(axTableField);

                this.MetaModelService.UpdateTable(axTable, this.getModelSaveInfo());
            }
            else
            {
                Metadata.MetaModel.AxTableExtension axTableExtension = this.MetadataProvider.TableExtensions.Read(this.namedElement.Name);
                axTableExtension.Fields.Add(axTableField);
            }
        }

        /// <summary>
        /// Gets the Model for the current project
        /// </summary>
        /// <returns>Model save info for saving objects against the model</returns>
        /// <remarks>Copied and adapted from https://github.com/XppDevs/vs-addin/blob/master/XppDevs-Addins/AddinsDemo/AddinsDemo/LocalUtils.cs</remarks>
        protected Metadata.MetaModel.ModelSaveInfo getModelSaveInfo()
        {
            Metadata.MetaModel.ModelSaveInfo saveInfo = new Metadata.MetaModel.ModelSaveInfo();
            Metadata.MetaModel.ModelInfo modelInfo;

            if (this.namedElement is Table)
            {
                modelInfo = this.MetaModelService.GetTableModelInfo(this.namedElement.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();
            }
            else
            {
                modelInfo = this.MetaModelService.GetTableExtensionModelInfo(this.namedElement.Name).FirstOrDefault<Metadata.MetaModel.ModelInfo>();
            }

            saveInfo.Id = modelInfo.Id;
            saveInfo.Layer = modelInfo.Layer;

            return saveInfo;
        }
    }
}

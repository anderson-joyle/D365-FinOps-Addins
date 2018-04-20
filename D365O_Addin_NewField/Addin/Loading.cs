using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using System.Windows.Forms;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace Addin
{
    public class Loading
    {
        public ProgressBar progressBarExtends;

        public ComboBox comboBoxExtends;
        public ComboBox comboBoxEDTName;

        #region Properties
        /// <summary>
        /// Backing field for the metadata provider that is useful for retrieving
        /// metadata that is not loaded in the VS instance.
        /// </summary>
        private Microsoft.Dynamics.AX.Metadata.Providers.IMetadataProvider metadataProvider = null;
        private Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService metaModelService = null;

        /// <summary>
        /// Gets a singleton instance of the metadata provider that can access the metadata repository.
        /// Any metadata, irrespective of whether it is part of what is being edited by VS, is available
        /// through this provider.
        /// </summary>
        public Microsoft.Dynamics.AX.Metadata.Providers.IMetadataProvider MetadataProvider
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

        public Microsoft.Dynamics.AX.Metadata.Service.IMetaModelService MetaModelService
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

        public void loadComboboxEDTs(string selected)
        {
            FieldType fieldType;
            string edtModelName = string.Empty;

            Enum.TryParse<FieldType>(selected, out fieldType);

            this.comboBoxExtends.Items.Clear();
            this.comboBoxEDTName.Items.Clear();

            switch (fieldType)
            {
                case FieldType.String:
                case FieldType.Memo:
                    edtModelName = "Name";
                    break;
                case FieldType.Integer:
                    edtModelName = "Integer";
                    break;
                case FieldType.Real:
                    edtModelName = "RealBase";
                    break;
                case FieldType.DateTime:
                    edtModelName = "TransDateTime";
                    break;
                case FieldType.Guid:
                    edtModelName = "SysGuid";
                    break;
                case FieldType.Int64:
                    edtModelName = "RecId";
                    break;
                case FieldType.Enum:
                    edtModelName = "NoYesId";
                    break;
                case FieldType.Time:
                    edtModelName = "ToTime";
                    break;
                case FieldType.Container:
                    edtModelName = "CryptoBlob";
                    break;
                case FieldType.Date:
                    edtModelName = "TransDate";
                    break;
                default:
                    throw new Exception($"Field type {fieldType} not supported.");
            }

            var namesList = this.MetaModelService.GetExtendedDataTypeNames(this.MetadataProvider.Edts.Read(edtModelName).GetType());

            this.restartProgressBarExtends(namesList.Count);

            // Add empty item as index 0
            this.comboBoxExtends.Items.Add("");
            this.comboBoxEDTName.Items.Add("");

            foreach (string edtName in namesList)
            {
                this.comboBoxExtends.Items.Add(edtName);
                this.comboBoxEDTName.Items.Add(edtName);
                this.progressBarExtends.PerformStep();
            }

            this.comboBoxExtends.SelectedIndex = 0;
            this.comboBoxEDTName.SelectedIndex = 0;
        }

        private void restartProgressBarExtends(int max)
        {
            this.progressBarExtends.Value = 0;
            this.progressBarExtends.Step = 1;
            this.progressBarExtends.Maximum = max;
        }
    }
}

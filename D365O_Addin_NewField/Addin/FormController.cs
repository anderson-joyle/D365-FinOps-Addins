using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Metadata = Microsoft.Dynamics.AX.Metadata;

namespace Addin
{
    public class FormController
    {
        #region Form controls
        public Form form { set; get; }

        public ComboBox comboBoxFieldType { set; get; }

        public TextBox textBoxFieldName { set; get; }
        public TextBox textBoxEDTName { set; get; }
        public TextBox textBoxExtends { set; get; }
        public TextBox textBoxLabel { set; get; }
        public TextBox textBoxHelpText { set; get; }
        public TextBox textBoxEnumType { set; get; }
        public NumericUpDown numericUpDownStringSize { set; get; }

        public ToolStripStatusLabel toolStripStatusLabel { set; get; }

        public ToolTip toolTipEdtName { set; get; }

        public GroupBox groupBoxStringComplements { set; get; }
        public GroupBox groupBoxEnumComplements { set; get; }
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

        public void init()
        {
            this.textBoxEnumType.Enabled = false;

            this.numericUpDownStringSize.Enabled = true;
            this.numericUpDownStringSize.Value = 10;

            this.toolStripStatusLabel.Text = string.Empty;
        }

        public bool prompt()
        {
            FormMain formMain = new FormMain(this);

            formMain.ShowDialog();

            return formMain.closeOk;
        }

        public void fieldNameChanged(object sender, EventArgs e)
        {
            this.textBoxEDTName.Text = this.textBoxFieldName.Text;
        }

        public void edtNameChanged(object sender, EventArgs e)
        {
            FieldType fieldType;

            Enum.TryParse<FieldType>(this.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

            if (this.textBoxEDTName.Text != string.Empty)
            {
                Metadata.MetaModel.AxEdt edt = this.MetadataProvider.Edts.Read(this.textBoxEDTName.Text);

                if (edt == null)
                {
                    this.textBoxHelpText.Text = string.Empty;
                    this.textBoxLabel.Text = string.Empty;

                    this.textBoxExtends.Enabled = true;
                }
                else
                {
                    this.textBoxExtends.Enabled = false;
                    this.textBoxExtends.Text = string.Empty;
                    this.numericUpDownStringSize.Enabled = false;
                    this.numericUpDownStringSize.Value = 0;

                    this.toolTipEdtName.SetToolTip(this.textBoxEDTName, "This EDT already exists. Label and help text will be set to new field.");
                }
            }
        }

        public void fieldTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            FieldType fieldType;

            Metadata.MetaModel.AxEdt edt = null;

            bool enumTypeEnabled = false;
            bool stringSizeEnabled = false;

            Enum.TryParse<FieldType>(this.comboBoxFieldType.SelectedValue.ToString(), out fieldType);

            if (this.textBoxEDTName.Text != string.Empty)
            {
                edt = this.MetadataProvider.Edts.Read(this.textBoxEDTName.Text);
            }

            switch (fieldType)
            {
                case FieldType.String:
                    if (this.textBoxEDTName.Text == string.Empty)
                    {
                        stringSizeEnabled = true;
                    }
                    else
                    {
                        if (edt == null)
                        {
                            stringSizeEnabled = true;
                        }
                        else
                        {
                            stringSizeEnabled = false;
                        }
                    }
                    break;
                case FieldType.Enum:
                    if (this.textBoxEDTName.Text == string.Empty)
                    {
                        enumTypeEnabled = true;
                    }
                    else
                    {
                        if (edt == null)
                        {
                            enumTypeEnabled = true;
                        }
                        else
                        {
                            enumTypeEnabled = false;
                        }
                    }
                    break;
                default:
                    stringSizeEnabled = false;
                    enumTypeEnabled = false;
                    break;
            }

            numericUpDownStringSize.Enabled = stringSizeEnabled;
            textBoxEnumType.Enabled = enumTypeEnabled;
        }

        public bool canClose()
        {
            bool ret = true;

            if (this.textBoxFieldName.Text == string.Empty)
            {
                ret = false;
                this.toolStripStatusLabel.Text = "Field name cannot be empty.";
            }

            if (this.textBoxEDTName.Text == string.Empty)
            {
                ret = false;
                this.toolStripStatusLabel.Text = "EDT name cannot be empty.";
            }

            return ret;
        }
    }
}

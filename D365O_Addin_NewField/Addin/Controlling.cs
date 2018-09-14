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
    public class Controlling
    {
        protected Loading loading;

        #region Form controls
        public Form form { set; get; }

        public ComboBox comboBoxFieldType { set; get; }
        public TextBox textBoxFieldName { set; get; }
        public ComboBox comboBoxEDTName { set; get; }
        public ComboBox comboBoxExtends { set; get; }
        public ProgressBar progressBarExtends { set; get; }
        public ToolTip toolTipEdtName { set; get; }
        public GroupBox groupBoxStringComplements { set; get; }
        public GroupBox groupBoxEnumComplements { set; get; }
        public ToolStripStatusLabel toolStripStatusLabelVerbose { set; get; }
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

        public Controlling()
        {
            loading = new Loading();
        }

        public void init()
        {
            
            this.comboBoxExtends.Leave += this.extendsLeave;
            this.comboBoxEDTName.Leave += this.edtLeave;

            // Verbose for debugging purposes
            //this.comboBoxFieldType.SelectedIndexChanged += this.verbose;
            //this.comboBoxFieldType.SelectedIndexChanged += this.verbose;
            //this.comboBoxFieldType.SelectedIndexChanged += this.verbose;
            //this.comboBoxEnumType.SelectedIndexChanged += this.verbose;
            //this.comboBoxExtends.Leave += this.verbose;
            //this.comboBoxEDTName.Leave += this.verbose;
            
            this.loading.comboBoxExtends = comboBoxExtends;
            this.loading.comboBoxEDTName = comboBoxEDTName;
            this.loading.progressBarExtends = progressBarExtends;
            this.comboBoxFieldType.DataSource = Enum.GetValues(typeof(FieldType));

            this.loading.loadComboboxEDTsAsync("String");
            //this.loading.loadComboboxEDTs("String");

            //this.comboBoxEDTName.SelectedIndex = 0;
            //this.comboBoxExtends.SelectedIndex = 0;

            this.comboBoxFieldType.SelectedIndexChanged += this.fieldTypeChanged;
            this.comboBoxFieldType.SelectedIndexChanged += this.loadEDTNames;
        }

        public bool prompt()
        {
            FormMain formMain = new FormMain(this);

            formMain.ShowDialog();

            return formMain.closeOk;
        }

        public void enumTypeChanged(object sender, EventArgs e)
        {
            ComboBox curCombobox = sender as ComboBox;

            if (string.IsNullOrEmpty(curCombobox.Text) || (curCombobox.SelectedIndex == -1))
            {
                this.comboBoxExtends.Enabled = true;
            }
            else
            {
                this.comboBoxExtends.SelectedItem = null;
                this.comboBoxExtends.Enabled = false;
            }
        }
        public void verbose(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox curComboBox = sender as ComboBox;

                this.toolStripStatusLabelVerbose.Text = $"(Index:{curComboBox.SelectedIndex} - Text:{curComboBox.Text})";
            }
            else if (sender is TextBox)
            {
                TextBox curTextBox = sender as TextBox;

                this.toolStripStatusLabelVerbose.Text = $"(Text:{curTextBox.Text})";
            }
            else
            {
                ComboBox curComboBox = sender as ComboBox;

                this.toolStripStatusLabelVerbose.Text = "Control not supported.";
            }
        }

        public void edtLeave(object sender, EventArgs e)
        {
            ComboBox curCombobox = sender as ComboBox;

            if (curCombobox.SelectedIndex == -1)
            {
                if (curCombobox.Text != string.Empty)
                {
                    int index = curCombobox.Items.IndexOf(curCombobox.Text);

                    if (index > 0)
                    {
                        curCombobox.SelectedIndex = index;
                    }
                }
                else
                {
                    curCombobox.SelectedIndex = 0;
                }
            }
        }

        public void extendsLeave(object sender, EventArgs e)
        {
            ComboBox curCombobox = sender as ComboBox;

            if (curCombobox.SelectedIndex == -1)
            {
                if (curCombobox.Text != string.Empty)
                {
                    int index = curCombobox.Items.IndexOf(curCombobox.Text);

                    if (index > 0)
                    {
                        curCombobox.SelectedIndex = index;
                    }
                }
                else
                {
                    curCombobox.SelectedIndex = 0;
                }
            }
        }

        public void loadEDTNames(object sender, EventArgs e)
        {
            ComboBox curCombobox = sender as ComboBox;

            this.loading.loadComboboxEDTs(curCombobox.SelectedValue.ToString());
        }

        public void fieldTypeChanged(object sender, EventArgs e)
        {
            ComboBox curCombobox = sender as ComboBox;
            FieldType fieldType;

            Metadata.MetaModel.AxEdt edt = null;
            
            this.comboBoxExtends.SelectedIndex = -1;

            Enum.TryParse<FieldType>(curCombobox.SelectedItem.ToString(), out fieldType);

            if (this.comboBoxEDTName.SelectedIndex == -1)
            {
                if (this.comboBoxEDTName.Text != string.Empty)
                {
                    edt = this.MetadataProvider.Edts.Read(this.comboBoxEDTName.Text);
                }
            }
            else
            {
                edt = this.MetadataProvider.Edts.Read(this.comboBoxEDTName.SelectedItem.ToString());
            }
        }

        public bool canClose()
        {
            bool ret = true;

            if (this.textBoxFieldName.Text == string.Empty)
            {
                ret = false;
                MessageBox.Show("Field name cannot be empty.", "Mandatory field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return ret;
        }
    }
}

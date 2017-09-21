using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Dynamics.AX.Metadata.Core.MetaModel;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Automation.Tables;

namespace Addin
{
    public partial class FormMain : FormBase
    {
        public bool closeOk { set; get; }
        protected FormController controller;

        public FormMain(FormController controller)
        {
            InitializeComponent();

            this.controller = controller;

            this.controller.form = this;

            this.controller.textBoxEDTName = textBoxEDTName;
            this.controller.textBoxEnumType = textBoxEnumType;
            this.controller.textBoxExtends = textBoxExtends;
            this.controller.textBoxFieldName = textBoxFieldName;
            this.controller.textBoxHelpText = textBoxHelpText;
            this.controller.textBoxLabel = textBoxLabel;
            this.controller.comboBoxFieldType = comboBoxFieldType;
            this.controller.numericUpDownStringSize = numericUpDownStringSize;
            this.controller.toolStripStatusLabel = toolStripStatusLabelNotes;
            this.controller.toolTipEdtName = toolTipEdtName;

            this.controller.init();

            this.closeOk = false;

            comboBoxFieldType.DataSource = Enum.GetValues(typeof(FieldType));
            
            this.comboBoxFieldType.SelectedIndexChanged += this.controller.fieldTypeSelectedIndexChanged;
            this.textBoxEDTName.Leave += this.controller.edtNameChanged;
        }

        private void TextBoxFieldName_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.controller.canClose())
            {
                this.closeOk = true;
                this.Close();
            }
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();

            aboutBox.ShowDialog();
        }
    }
}

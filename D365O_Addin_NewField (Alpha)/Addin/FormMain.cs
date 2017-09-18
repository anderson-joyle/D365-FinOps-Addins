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
        protected Building.NewField newField;

        public FormMain(string elementName)
        {
            InitializeComponent();

            this.closeOk = false;
            this.Text = $"{elementName}";

            newField = new Building.NewField();

            comboBoxFieldType.DataSource = Enum.GetValues(typeof(FieldType));
        }

        private void textBoxFieldName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEDTName.Text == string.Empty)
            {
                textBoxEDTName.Text = textBoxFieldName.Text;
            }
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            FieldType fieldType;

            if (textBoxFieldName.Text == string.Empty ||
                textBoxEDTName.Text == string.Empty)
            {
                MessageBox.Show("Values cannt be blank.", "Blank values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.closeOk = true;

            Enum.TryParse<FieldType>(comboBoxFieldType.SelectedValue.ToString(), out fieldType);

            newField.FieldType = fieldType;
            newField.FieldName = textBoxFieldName.Text;
            newField.EdtName = textBoxEDTName.Text;

            this.Close();
        }

        public Building.NewField getNewField()
        {
            return this.newField;
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

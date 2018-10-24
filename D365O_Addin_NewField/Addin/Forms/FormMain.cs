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
        protected Controlling controlling;

        public FormMain(Controlling controlling)
        {
            InitializeComponent();

            this.controlling = controlling;
        }

        private void TextBoxFieldName_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.controlling.canClose())
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.controlling.form = this;

            this.controlling.comboBoxEDTName = comboBoxEDTName;
            this.controlling.comboBoxExtends = comboBoxExtends;
            this.controlling.textBoxFieldName = textBoxFieldName;
            this.controlling.textBoxLabel     = textBoxLabel;
            this.controlling.textBoxHelpText  = textBoxHelpText;
            this.controlling.comboBoxFieldType = comboBoxFieldType;
            this.controlling.progressBarExtends = progressBarExtends;
            this.controlling.toolStripStatusLabelVerbose = toolStripStatusLabelVerbose;

            this.controlling.init();

            this.closeOk = false;
        }
    }
}

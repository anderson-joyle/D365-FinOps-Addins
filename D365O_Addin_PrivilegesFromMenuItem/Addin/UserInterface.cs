using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building
{
    public partial class UserInterface : Form
    {
        public bool closeOk { set; get; }

        public UserInterface()
        {
            InitializeComponent();

            this.closeOk = false;

            // Init list box with pre check items
            checkedListBox.Items.Add("Unset", false);
            checkedListBox.Items.Add("No access", false);
            checkedListBox.Items.Add("Read", true);
            checkedListBox.Items.Add("Update", false);
            checkedListBox.Items.Add("Create", false);
            checkedListBox.Items.Add("Correct", false);
            checkedListBox.Items.Add("Delete", true);
        }

        public CheckedListBox.CheckedItemCollection checkedItems()
        {
            return checkedListBox.CheckedItems;
        }

        private void toolStripButtonCreate_Click(object sender, EventArgs e)
        {
            this.closeOk = true;

            this.Close();
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
    }
}

namespace Addin
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.textBoxEDTName = new System.Windows.Forms.TextBox();
            this.comboBoxFieldType = new System.Windows.Forms.ComboBox();
            this.labelFieldType = new System.Windows.Forms.Label();
            this.labelFieldName = new System.Windows.Forms.Label();
            this.labelEDTName = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreate,
            this.toolStripSeparator1,
            this.toolStripButtonCancel,
            this.toolStripButtonAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(431, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonCreate
            // 
            this.toolStripButtonCreate.Image = global::Addin.AddinResources.accept_button;
            this.toolStripButtonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreate.Name = "toolStripButtonCreate";
            this.toolStripButtonCreate.Size = new System.Drawing.Size(61, 22);
            this.toolStripButtonCreate.Text = "Create";
            this.toolStripButtonCreate.Click += new System.EventHandler(this.toolStripButtonCreate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.Image = global::Addin.AddinResources.cancel;
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(63, 22);
            this.toolStripButtonCancel.Text = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = global::Addin.AddinResources.information;
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 137);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(431, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // textBoxFieldName
            // 
            this.textBoxFieldName.Location = new System.Drawing.Point(89, 58);
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.Size = new System.Drawing.Size(304, 22);
            this.textBoxFieldName.TabIndex = 2;
            this.textBoxFieldName.TextChanged += new System.EventHandler(this.textBoxFieldName_TextChanged);
            // 
            // textBoxEDTName
            // 
            this.textBoxEDTName.Location = new System.Drawing.Point(89, 89);
            this.textBoxEDTName.Name = "textBoxEDTName";
            this.textBoxEDTName.Size = new System.Drawing.Size(304, 22);
            this.textBoxEDTName.TabIndex = 3;
            // 
            // comboBoxFieldType
            // 
            this.comboBoxFieldType.FormattingEnabled = true;
            this.comboBoxFieldType.Location = new System.Drawing.Point(89, 28);
            this.comboBoxFieldType.Name = "comboBoxFieldType";
            this.comboBoxFieldType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxFieldType.TabIndex = 4;
            // 
            // labelFieldType
            // 
            this.labelFieldType.AutoSize = true;
            this.labelFieldType.Location = new System.Drawing.Point(13, 31);
            this.labelFieldType.Name = "labelFieldType";
            this.labelFieldType.Size = new System.Drawing.Size(70, 16);
            this.labelFieldType.TabIndex = 5;
            this.labelFieldType.Text = "Field type:";
            // 
            // labelFieldName
            // 
            this.labelFieldName.AutoSize = true;
            this.labelFieldName.Location = new System.Drawing.Point(5, 61);
            this.labelFieldName.Name = "labelFieldName";
            this.labelFieldName.Size = new System.Drawing.Size(78, 16);
            this.labelFieldName.TabIndex = 6;
            this.labelFieldName.Text = "Field name:";
            // 
            // labelEDTName
            // 
            this.labelEDTName.AutoSize = true;
            this.labelEDTName.Location = new System.Drawing.Point(7, 92);
            this.labelEDTName.Name = "labelEDTName";
            this.labelEDTName.Size = new System.Drawing.Size(76, 16);
            this.labelEDTName.TabIndex = 7;
            this.labelEDTName.Text = "EDT name:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 159);
            this.Controls.Add(this.labelEDTName);
            this.Controls.Add(this.labelFieldName);
            this.Controls.Add(this.labelFieldType);
            this.Controls.Add(this.comboBoxFieldType);
            this.Controls.Add(this.textBoxEDTName);
            this.Controls.Add(this.textBoxFieldName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.TextBox textBoxEDTName;
        private System.Windows.Forms.ComboBox comboBoxFieldType;
        private System.Windows.Forms.Label labelFieldType;
        private System.Windows.Forms.Label labelFieldName;
        private System.Windows.Forms.Label labelEDTName;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
    }
}
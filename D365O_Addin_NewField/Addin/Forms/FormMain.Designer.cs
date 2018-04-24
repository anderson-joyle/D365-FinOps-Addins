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
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelVerboseTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVerbose = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.toolTipFieldType = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxFieldType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.comboBoxEDTName = new System.Windows.Forms.ComboBox();
            this.progressBarExtends = new System.Windows.Forms.ProgressBar();
            this.comboBoxExtends = new System.Windows.Forms.ComboBox();
            this.labelExtends = new System.Windows.Forms.Label();
            this.labelEDTName = new System.Windows.Forms.Label();
            this.labelFieldName = new System.Windows.Forms.Label();
            this.labelFieldType = new System.Windows.Forms.Label();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.toolTipEnum = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFieldName = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipStringSize = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipEdtName = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipExtends = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelVerboseTitle,
            this.toolStripStatusLabelVerbose});
            this.statusStrip.Location = new System.Drawing.Point(0, 199);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(510, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // toolStripStatusLabelVerboseTitle
            // 
            this.toolStripStatusLabelVerboseTitle.Name = "toolStripStatusLabelVerboseTitle";
            this.toolStripStatusLabelVerboseTitle.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabelVerboseTitle.Text = "Verbose: ";
            // 
            // toolStripStatusLabelVerbose
            // 
            this.toolStripStatusLabelVerbose.Name = "toolStripStatusLabelVerbose";
            this.toolStripStatusLabelVerbose.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreate,
            this.toolStripSeparator,
            this.toolStripButtonCancel,
            this.toolStripButtonAbout});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(510, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
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
            // toolTipFieldType
            // 
            this.toolTipFieldType.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipFieldType.ToolTipTitle = "Field type";
            // 
            // comboBoxFieldType
            // 
            this.comboBoxFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFieldType.FormattingEnabled = true;
            this.comboBoxFieldType.Location = new System.Drawing.Point(95, 6);
            this.comboBoxFieldType.Name = "comboBoxFieldType";
            this.comboBoxFieldType.Size = new System.Drawing.Size(215, 26);
            this.comboBoxFieldType.TabIndex = 1;
            this.toolTipFieldType.SetToolTip(this.comboBoxFieldType, "All D365 primitive field types.");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 174);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.comboBoxEDTName);
            this.tabPageGeneral.Controls.Add(this.progressBarExtends);
            this.tabPageGeneral.Controls.Add(this.comboBoxExtends);
            this.tabPageGeneral.Controls.Add(this.labelExtends);
            this.tabPageGeneral.Controls.Add(this.labelEDTName);
            this.tabPageGeneral.Controls.Add(this.labelFieldName);
            this.tabPageGeneral.Controls.Add(this.labelFieldType);
            this.tabPageGeneral.Controls.Add(this.comboBoxFieldType);
            this.tabPageGeneral.Controls.Add(this.textBoxFieldName);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 27);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(502, 143);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // comboBoxEDTName
            // 
            this.comboBoxEDTName.FormattingEnabled = true;
            this.comboBoxEDTName.Location = new System.Drawing.Point(95, 68);
            this.comboBoxEDTName.Name = "comboBoxEDTName";
            this.comboBoxEDTName.Size = new System.Drawing.Size(398, 26);
            this.comboBoxEDTName.TabIndex = 4;
            // 
            // progressBarExtends
            // 
            this.progressBarExtends.Location = new System.Drawing.Point(95, 126);
            this.progressBarExtends.Name = "progressBarExtends";
            this.progressBarExtends.Size = new System.Drawing.Size(398, 10);
            this.progressBarExtends.Step = 1;
            this.progressBarExtends.TabIndex = 52;
            // 
            // comboBoxExtends
            // 
            this.comboBoxExtends.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxExtends.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExtends.FormattingEnabled = true;
            this.comboBoxExtends.Location = new System.Drawing.Point(95, 98);
            this.comboBoxExtends.Name = "comboBoxExtends";
            this.comboBoxExtends.Size = new System.Drawing.Size(398, 26);
            this.comboBoxExtends.Sorted = true;
            this.comboBoxExtends.TabIndex = 5;
            this.toolTipExtends.SetToolTip(this.comboBoxExtends, "If a new extended data type is given, it\'s possible to set a edt to extend.");
            // 
            // labelExtends
            // 
            this.labelExtends.AutoSize = true;
            this.labelExtends.Location = new System.Drawing.Point(26, 101);
            this.labelExtends.Name = "labelExtends";
            this.labelExtends.Size = new System.Drawing.Size(65, 18);
            this.labelExtends.TabIndex = 47;
            this.labelExtends.Text = "Extends:";
            // 
            // labelEDTName
            // 
            this.labelEDTName.AutoSize = true;
            this.labelEDTName.Location = new System.Drawing.Point(8, 71);
            this.labelEDTName.Name = "labelEDTName";
            this.labelEDTName.Size = new System.Drawing.Size(83, 18);
            this.labelEDTName.TabIndex = 46;
            this.labelEDTName.Text = "EDT name:";
            // 
            // labelFieldName
            // 
            this.labelFieldName.AutoSize = true;
            this.labelFieldName.Location = new System.Drawing.Point(7, 40);
            this.labelFieldName.Name = "labelFieldName";
            this.labelFieldName.Size = new System.Drawing.Size(84, 18);
            this.labelFieldName.TabIndex = 45;
            this.labelFieldName.Text = "Field name:";
            // 
            // labelFieldType
            // 
            this.labelFieldType.AutoSize = true;
            this.labelFieldType.Location = new System.Drawing.Point(17, 9);
            this.labelFieldType.Name = "labelFieldType";
            this.labelFieldType.Size = new System.Drawing.Size(74, 18);
            this.labelFieldType.TabIndex = 44;
            this.labelFieldType.Text = "Field type:";
            // 
            // textBoxFieldName
            // 
            this.textBoxFieldName.Location = new System.Drawing.Point(95, 38);
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.Size = new System.Drawing.Size(398, 24);
            this.textBoxFieldName.TabIndex = 3;
            this.toolTipFieldName.SetToolTip(this.textBoxFieldName, "Newly created field name.");
            // 
            // toolTipEnum
            // 
            this.toolTipEnum.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipEnum.ToolTipTitle = "Enum";
            // 
            // toolTipFieldName
            // 
            this.toolTipFieldName.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipFieldName.ToolTipTitle = "Field name";
            // 
            // toolTipStringSize
            // 
            this.toolTipStringSize.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipStringSize.ToolTipTitle = "String size";
            // 
            // toolTipEdtName
            // 
            this.toolTipEdtName.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipEdtName.ToolTipTitle = "Extended data type name";
            // 
            // toolTipExtends
            // 
            this.toolTipExtends.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipExtends.ToolTipTitle = "Extends";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 221);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Add field + edt";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolTip toolTipFieldType;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.ComboBox comboBoxExtends;
        private System.Windows.Forms.Label labelExtends;
        private System.Windows.Forms.Label labelEDTName;
        private System.Windows.Forms.Label labelFieldName;
        private System.Windows.Forms.Label labelFieldType;
        private System.Windows.Forms.ComboBox comboBoxFieldType;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.ProgressBar progressBarExtends;
        private System.Windows.Forms.ToolTip toolTipEnum;
        private System.Windows.Forms.ToolTip toolTipFieldName;
        private System.Windows.Forms.ToolTip toolTipStringSize;
        private System.Windows.Forms.ToolTip toolTipEdtName;
        private System.Windows.Forms.ToolTip toolTipExtends;
        private System.Windows.Forms.ComboBox comboBoxEDTName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVerboseTitle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVerbose;
    }
}
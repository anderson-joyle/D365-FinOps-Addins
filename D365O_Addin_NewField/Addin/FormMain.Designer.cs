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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelNotes = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHelpText = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelExtends = new System.Windows.Forms.Label();
            this.textBoxExtends = new System.Windows.Forms.TextBox();
            this.labelEDTName = new System.Windows.Forms.Label();
            this.labelFieldName = new System.Windows.Forms.Label();
            this.labelFieldType = new System.Windows.Forms.Label();
            this.comboBoxFieldType = new System.Windows.Forms.ComboBox();
            this.textBoxEDTName = new System.Windows.Forms.TextBox();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.groupBoxSpecific = new System.Windows.Forms.GroupBox();
            this.numericUpDownStringSize = new System.Windows.Forms.NumericUpDown();
            this.labelStringSize = new System.Windows.Forms.Label();
            this.labelEnumType = new System.Windows.Forms.Label();
            this.textBoxEnumType = new System.Windows.Forms.TextBox();
            this.toolTipEdtName = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            this.groupBoxSpecific.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStringSize)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(812, 25);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelNotes});
            this.statusStrip1.Location = new System.Drawing.Point(0, 282);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(812, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelNotes
            // 
            this.toolStripStatusLabelNotes.Name = "toolStripStatusLabelNotes";
            this.toolStripStatusLabelNotes.Size = new System.Drawing.Size(143, 17);
            this.toolStripStatusLabelNotes.Text = "toolStripStatusLabelNotes";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBoxGeneral);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.groupBoxSpecific);
            this.splitContainer.Size = new System.Drawing.Size(812, 257);
            this.splitContainer.SplitterDistance = 469;
            this.splitContainer.TabIndex = 2;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.label2);
            this.groupBoxGeneral.Controls.Add(this.textBoxHelpText);
            this.groupBoxGeneral.Controls.Add(this.labelLabel);
            this.groupBoxGeneral.Controls.Add(this.textBoxLabel);
            this.groupBoxGeneral.Controls.Add(this.labelExtends);
            this.groupBoxGeneral.Controls.Add(this.textBoxExtends);
            this.groupBoxGeneral.Controls.Add(this.labelEDTName);
            this.groupBoxGeneral.Controls.Add(this.labelFieldName);
            this.groupBoxGeneral.Controls.Add(this.labelFieldType);
            this.groupBoxGeneral.Controls.Add(this.comboBoxFieldType);
            this.groupBoxGeneral.Controls.Add(this.textBoxEDTName);
            this.groupBoxGeneral.Controls.Add(this.textBoxFieldName);
            this.groupBoxGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGeneral.Location = new System.Drawing.Point(0, 0);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(469, 257);
            this.groupBoxGeneral.TabIndex = 1;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "General";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Help text:";
            // 
            // textBoxHelpText
            // 
            this.textBoxHelpText.Location = new System.Drawing.Point(90, 216);
            this.textBoxHelpText.Name = "textBoxHelpText";
            this.textBoxHelpText.Size = new System.Drawing.Size(374, 22);
            this.textBoxHelpText.TabIndex = 6;
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(39, 191);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(45, 16);
            this.labelLabel.TabIndex = 38;
            this.labelLabel.Text = "Label:";
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(90, 188);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(374, 22);
            this.textBoxLabel.TabIndex = 5;
            // 
            // labelExtends
            // 
            this.labelExtends.AutoSize = true;
            this.labelExtends.Location = new System.Drawing.Point(25, 147);
            this.labelExtends.Name = "labelExtends";
            this.labelExtends.Size = new System.Drawing.Size(59, 16);
            this.labelExtends.TabIndex = 37;
            this.labelExtends.Text = "Extends:";
            // 
            // textBoxExtends
            // 
            this.textBoxExtends.Location = new System.Drawing.Point(90, 144);
            this.textBoxExtends.Name = "textBoxExtends";
            this.textBoxExtends.Size = new System.Drawing.Size(283, 22);
            this.textBoxExtends.TabIndex = 4;
            // 
            // labelEDTName
            // 
            this.labelEDTName.AutoSize = true;
            this.labelEDTName.Location = new System.Drawing.Point(8, 119);
            this.labelEDTName.Name = "labelEDTName";
            this.labelEDTName.Size = new System.Drawing.Size(76, 16);
            this.labelEDTName.TabIndex = 36;
            this.labelEDTName.Text = "EDT name:";
            // 
            // labelFieldName
            // 
            this.labelFieldName.AutoSize = true;
            this.labelFieldName.Location = new System.Drawing.Point(6, 73);
            this.labelFieldName.Name = "labelFieldName";
            this.labelFieldName.Size = new System.Drawing.Size(78, 16);
            this.labelFieldName.TabIndex = 35;
            this.labelFieldName.Text = "Field name:";
            // 
            // labelFieldType
            // 
            this.labelFieldType.AutoSize = true;
            this.labelFieldType.Location = new System.Drawing.Point(14, 24);
            this.labelFieldType.Name = "labelFieldType";
            this.labelFieldType.Size = new System.Drawing.Size(70, 16);
            this.labelFieldType.TabIndex = 34;
            this.labelFieldType.Text = "Field type:";
            // 
            // comboBoxFieldType
            // 
            this.comboBoxFieldType.FormattingEnabled = true;
            this.comboBoxFieldType.Location = new System.Drawing.Point(90, 21);
            this.comboBoxFieldType.Name = "comboBoxFieldType";
            this.comboBoxFieldType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxFieldType.TabIndex = 1;
            // 
            // textBoxEDTName
            // 
            this.textBoxEDTName.Location = new System.Drawing.Point(90, 116);
            this.textBoxEDTName.Name = "textBoxEDTName";
            this.textBoxEDTName.Size = new System.Drawing.Size(283, 22);
            this.textBoxEDTName.TabIndex = 3;
            // 
            // textBoxFieldName
            // 
            this.textBoxFieldName.Location = new System.Drawing.Point(90, 70);
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.Size = new System.Drawing.Size(283, 22);
            this.textBoxFieldName.TabIndex = 2;
            // 
            // groupBoxSpecific
            // 
            this.groupBoxSpecific.Controls.Add(this.numericUpDownStringSize);
            this.groupBoxSpecific.Controls.Add(this.labelStringSize);
            this.groupBoxSpecific.Controls.Add(this.labelEnumType);
            this.groupBoxSpecific.Controls.Add(this.textBoxEnumType);
            this.groupBoxSpecific.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSpecific.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSpecific.Name = "groupBoxSpecific";
            this.groupBoxSpecific.Size = new System.Drawing.Size(339, 257);
            this.groupBoxSpecific.TabIndex = 0;
            this.groupBoxSpecific.TabStop = false;
            this.groupBoxSpecific.Text = "Specific";
            // 
            // numericUpDownStringSize
            // 
            this.numericUpDownStringSize.Location = new System.Drawing.Point(86, 22);
            this.numericUpDownStringSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStringSize.Name = "numericUpDownStringSize";
            this.numericUpDownStringSize.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownStringSize.TabIndex = 7;
            // 
            // labelStringSize
            // 
            this.labelStringSize.AutoSize = true;
            this.labelStringSize.Location = new System.Drawing.Point(8, 24);
            this.labelStringSize.Name = "labelStringSize";
            this.labelStringSize.Size = new System.Drawing.Size(72, 16);
            this.labelStringSize.TabIndex = 38;
            this.labelStringSize.Text = "String size:";
            // 
            // labelEnumType
            // 
            this.labelEnumType.AutoSize = true;
            this.labelEnumType.Location = new System.Drawing.Point(6, 53);
            this.labelEnumType.Name = "labelEnumType";
            this.labelEnumType.Size = new System.Drawing.Size(74, 16);
            this.labelEnumType.TabIndex = 37;
            this.labelEnumType.Text = "Enum type:";
            // 
            // textBoxEnumType
            // 
            this.textBoxEnumType.Location = new System.Drawing.Point(86, 50);
            this.textBoxEnumType.Name = "textBoxEnumType";
            this.textBoxEnumType.Size = new System.Drawing.Size(241, 22);
            this.textBoxEnumType.TabIndex = 8;
            // 
            // toolTipEdtName
            // 
            this.toolTipEdtName.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipEdtName.ToolTipTitle = "Extended data type";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 304);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Create new field";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.groupBoxSpecific.ResumeLayout(false);
            this.groupBoxSpecific.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStringSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHelpText;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label labelExtends;
        private System.Windows.Forms.TextBox textBoxExtends;
        private System.Windows.Forms.Label labelEDTName;
        private System.Windows.Forms.Label labelFieldName;
        private System.Windows.Forms.Label labelFieldType;
        private System.Windows.Forms.ComboBox comboBoxFieldType;
        private System.Windows.Forms.TextBox textBoxEDTName;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.GroupBox groupBoxSpecific;
        private System.Windows.Forms.Label labelEnumType;
        private System.Windows.Forms.TextBox textBoxEnumType;
        private System.Windows.Forms.NumericUpDown numericUpDownStringSize;
        private System.Windows.Forms.Label labelStringSize;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelNotes;
        private System.Windows.Forms.ToolTip toolTipEdtName;
    }
}
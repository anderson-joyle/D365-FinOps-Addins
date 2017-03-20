namespace Building
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAbout,
            this.toolStripButtonCreate,
            this.toolStripSeparator1,
            this.toolStripButtonCancel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(284, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripAbout
            // 
            this.toolStripAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripAbout.Image = global::Addin.AddinResources.information;
            this.toolStripAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAbout.Name = "toolStripAbout";
            this.toolStripAbout.Size = new System.Drawing.Size(60, 22);
            this.toolStripAbout.Text = "About";
            this.toolStripAbout.Click += new System.EventHandler(this.toolStripAbout_Click);
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
            // checkedListBox
            // 
            this.checkedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(0, 25);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(284, 120);
            this.checkedListBox.TabIndex = 1;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 145);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInterface";
            this.Text = "Permissions";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripAbout;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
    }
}
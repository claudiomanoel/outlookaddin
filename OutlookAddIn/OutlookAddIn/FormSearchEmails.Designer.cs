namespace OutlookAddIn
{
    partial class FormSearchEmails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchEmails));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkedListBoxLabels = new System.Windows.Forms.CheckedListBox();
            this.dataGridViewSearchMails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearchMails)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSearch.Location = new System.Drawing.Point(725, 0);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 450);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // checkedListBoxLabels
            // 
            this.checkedListBoxLabels.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBoxLabels.FormattingEnabled = true;
            this.checkedListBoxLabels.Items.AddRange(new object[] {
            "Purely Personal",
            "Forwarded email(s) including replies",
            "Government / academic report(s)",
            "talking points",
            "secrecy / confidentiality"});
            this.checkedListBoxLabels.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxLabels.Name = "checkedListBoxLabels";
            this.checkedListBoxLabels.Size = new System.Drawing.Size(725, 89);
            this.checkedListBoxLabels.TabIndex = 1;
            // 
            // dataGridViewSearchMails
            // 
            this.dataGridViewSearchMails.AllowUserToAddRows = false;
            this.dataGridViewSearchMails.AllowUserToDeleteRows = false;
            this.dataGridViewSearchMails.AllowUserToOrderColumns = true;
            this.dataGridViewSearchMails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearchMails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSearchMails.Location = new System.Drawing.Point(0, 89);
            this.dataGridViewSearchMails.Name = "dataGridViewSearchMails";
            this.dataGridViewSearchMails.ReadOnly = true;
            this.dataGridViewSearchMails.RowTemplate.Height = 24;
            this.dataGridViewSearchMails.Size = new System.Drawing.Size(725, 361);
            this.dataGridViewSearchMails.TabIndex = 2;
            this.dataGridViewSearchMails.Enter += new System.EventHandler(this.dataGridViewSearchMails_Enter);
            // 
            // FormSearchEmails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewSearchMails);
            this.Controls.Add(this.checkedListBoxLabels);
            this.Controls.Add(this.buttonSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSearchEmails";
            this.Text = "Search Emails";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearchMails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.CheckedListBox checkedListBoxLabels;
        private System.Windows.Forms.DataGridView dataGridViewSearchMails;
    }
}
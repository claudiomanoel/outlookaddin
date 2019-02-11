﻿namespace OutlookAddIn
{
    partial class RibbonMail : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonMail()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonMail));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonShowMetrics = this.Factory.CreateRibbonButton();
            this.buttonSearchMails = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonShowMetrics);
            this.group1.Items.Add(this.buttonSearchMails);
            this.group1.Label = "Machine Learning Analyse";
            this.group1.Name = "group1";
            // 
            // buttonShowMetrics
            // 
            this.buttonShowMetrics.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonShowMetrics.Image = ((System.Drawing.Image)(resources.GetObject("buttonShowMetrics.Image")));
            this.buttonShowMetrics.Label = "Show Metrics";
            this.buttonShowMetrics.Name = "buttonShowMetrics";
            this.buttonShowMetrics.ShowImage = true;
            this.buttonShowMetrics.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonShowMetrics_Click);
            // 
            // buttonSearchMails
            // 
            this.buttonSearchMails.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSearchMails.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearchMails.Image")));
            this.buttonSearchMails.Label = "Search Mails";
            this.buttonSearchMails.Name = "buttonSearchMails";
            this.buttonSearchMails.ShowImage = true;
            this.buttonSearchMails.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSearchMails_Click);
            // 
            // RibbonMail
            // 
            this.Name = "RibbonMail";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonMail_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonShowMetrics;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSearchMails;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonMail DemoRibbon
        {
            get { return this.GetRibbon<RibbonMail>(); }
        }
    }
}

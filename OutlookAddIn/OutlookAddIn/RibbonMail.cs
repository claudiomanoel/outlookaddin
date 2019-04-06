using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace OutlookAddIn
{
    public partial class RibbonMail
    {
        #region Events

        private void RibbonMail_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void buttonShowMetrics_Click(object sender, RibbonControlEventArgs e)
        {
            openShowMetrics();
        }

        private void buttonSearchMails_Click(object sender, RibbonControlEventArgs e)
        {
            openSearchMails();
        }

        #endregion

        #region Methods

        private void openShowMetrics()
        {
            // Get Application application
            Outlook.Application application = Globals.ThisAddIn.Application;

            // Get the current item for this Inspecto object and check if is type
            // of MailItem
            Outlook.Inspector inspector = application.ActiveInspector();
            Outlook.MailItem mailItem = inspector.CurrentItem as Outlook.MailItem;
            if (mailItem != null)
            {
                FormShowMetrics v_FormMetrics = new FormShowMetrics(mailItem);
                v_FormMetrics.Show();
            }
        }

        private void openSearchMails()
        {
            // Get Application application
            Outlook.Application application = Globals.ThisAddIn.Application;
            FormSearchEmails v_FormSearchEmails = new FormSearchEmails(application.ActiveExplorer().Session.
       GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox));
            v_FormSearchEmails.Show();
        }

        #endregion
    }
}

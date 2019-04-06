using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using Flurl;
using Flurl.Http;
using Microsoft.Office.Interop.Outlook;
using System.Threading.Tasks;
using OutlookAddIn.Data;

namespace OutlookAddIn
{
    public partial class RibbonMail
    {
        #region Members

        private Microsoft.Office.Interop.Outlook.MAPIFolder m_Inbox;

        #endregion

        #region Events

        private void RibbonMail_Load(object sender, RibbonUIEventArgs e)
        {
            loadMembers();
            loadMails();
        }

        private void loadMembers()
        {
            m_Inbox = Globals.ThisAddIn.Application.ActiveExplorer().Session.
       GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
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

        private async void loadMails()
        {
            Microsoft.Office.Interop.Outlook.Items items = m_Inbox.Items;
            List<Task> tasks = new List<Task>();
            List<JsonReceiveEmailClassification> taskResponse = new List<JsonReceiveEmailClassification>();


            var emailClassificationPath = "http://localhost:8080/email_classification";
            foreach (var item in items)
            {
                try
                {
                    var email = (MailItem)item;
                    var id = email.EntryID;
                    var subject = email.Subject;
                    var body = email.Body;
                    string emailClassification = new StringBuilder(email.Subject).ToString();
                    var task = emailClassificationPath.PostJsonAsync(new { id = id, email = emailClassification });
                    tasks.Add(task);
                    var taskJsonResponse = await task.ReceiveJson<JsonReceiveEmailClassification>();
                    taskResponse.Add(taskJsonResponse);
                    
                    //var json = await task.ReceiveJson<JsonReceiveEmailClassification>();
                }
                catch (System.Exception exception)
                {

                }

            }
        }

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
            FormSearchEmails v_FormSearchEmails = new FormSearchEmails(m_Inbox);
            v_FormSearchEmails.Show();
        }

        #endregion
    }
}

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
using OutlookAddIn.Utils;

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
            List<JsonReceiveEmailClassification> v_ListTaskResponse = await getListTaskResponse();
            processListTaskResponse(v_ListTaskResponse);
        }

        private async Task<List<JsonReceiveEmailClassification>> getListTaskResponse()
        {
            Microsoft.Office.Interop.Outlook.Items items = m_Inbox.Items;

            List<JsonReceiveEmailClassification> v_ListTaskResponse = new List<JsonReceiveEmailClassification>();

            var v_EmailClassification = "http://localhost:8080/email_classification";
            var v_HtmlUtils = new HtmlUtils();
            foreach (var item in items)
            {
                try
                {
                    var v_Email = (MailItem)item;
                    var v_Id = v_Email.EntryID;
                    var v_Subject = v_Email.Subject;
                    var v_Body = v_Email.Body;
                    StringBuilder emailClassification = new StringBuilder(v_Email.Subject).Append(". ");
                    if (v_Email.BodyFormat == OlBodyFormat.olFormatHTML)
                    {
                        emailClassification.Append(v_HtmlUtils.HtmlToText(v_Body));
                    }
                    else
                    {
                        emailClassification.Append(v_Body);
                    }

                    var v_EmailToSend = emailClassification.ToString().Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");
                    var v_PostJsonTask = v_EmailClassification.PostJsonAsync(new { id = v_Id, email = v_EmailToSend });
                    var v_TaskJsonResponse = await v_PostJsonTask.ReceiveJson<JsonReceiveEmailClassification>();
                    v_ListTaskResponse.Add(v_TaskJsonResponse);
                }
                catch
                {

                }

            }

            return v_ListTaskResponse;
        }

        private void processListTaskResponse(List<JsonReceiveEmailClassification> p_ListTaskResponse)
        {
            p_ListTaskResponse.Add()
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

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
        private Dictionary<string, MailItem> m_ListIndexMailItem = new Dictionary<string, MailItem>();
        private Dictionary<string, List<string>> m_HashMail = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> m_HashCategory = new Dictionary<string, List<string>>();

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
            makeListMailIndex();
            List<JsonReceiveEmailClassification> v_ListTaskResponse = await getListTaskResponse();
            processListTaskResponse(v_ListTaskResponse);
        }

        private void makeListMailIndex()
        {
            Microsoft.Office.Interop.Outlook.Items v_EmailItems = m_Inbox.Items;
            foreach (var i_Item in v_EmailItems)
            {
                var v_Email = (MailItem)i_Item;
                var v_Id = v_Email.EntryID;
                m_ListIndexMailItem.Add(v_Id, v_Email);
            }
        }

        private async Task<List<JsonReceiveEmailClassification>> getListTaskResponse()
        {
            Microsoft.Office.Interop.Outlook.Items v_EmailItems = m_Inbox.Items;

            List<JsonReceiveEmailClassification> v_ListTaskResponse = new List<JsonReceiveEmailClassification>();

            var v_EmailClassification = "http://localhost:8080/email_classification";
            var v_HtmlUtils = new HtmlUtils();
            foreach (var i_Item in v_EmailItems)
            {
                try
                {
                    var v_Email = (MailItem)i_Item;
                    var v_Id = v_Email.EntryID;
                    var v_Subject = v_Email.Subject;
                    var v_Body = v_Email.Body;
                    StringBuilder v_EmailClasssification = new StringBuilder(v_Email.Subject).Append(". ");
                    if (v_Email.BodyFormat == OlBodyFormat.olFormatHTML)
                    {
                        v_EmailClasssification.Append(v_HtmlUtils.HtmlToText(v_Body));
                    }
                    else
                    {
                        v_EmailClasssification.Append(v_Body);
                    }

                    var v_EmailToSend = v_EmailClasssification.ToString().Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");
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
            if (p_ListTaskResponse == null)
            {
                return;
            }

            foreach(var v_TaskResponse in p_ListTaskResponse)
            {
                var v_EmailId = v_TaskResponse.id;
                var v_ListCategory = v_TaskResponse.res;

                m_HashMail.Add(v_EmailId, v_ListCategory);

                if (v_ListCategory == null)
                {
                    continue;
                }
                foreach(var v_Category in v_ListCategory)
                {
                    if (!m_HashCategory.ContainsKey(v_Category))
                    {
                        m_HashCategory.Add(v_Category, new List<string>());
                    }

                    m_HashCategory[v_Category].Add(v_EmailId);
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
            FormSearchEmails v_FormSearchEmails = new FormSearchEmails(m_Inbox, m_HashCategory, m_ListIndexMailItem);
            v_FormSearchEmails.Show();
        }

        #endregion
    }
}

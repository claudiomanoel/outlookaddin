using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookAddIn
{
    public partial class FormSearchEmails : Form
    {
        private Microsoft.Office.Interop.Outlook.MAPIFolder m_Inbox;
        public FormSearchEmails(Microsoft.Office.Interop.Outlook.MAPIFolder p_Inbox)
        {
            InitializeComponent();
            m_Inbox = p_Inbox;
        }

        private class Email
        {
            public string Subject { get; set; }
            public string Sender { get; set; }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            BindingSource v_Source = new BindingSource();
            List<Email> v_List = new List<Email> { new Email { Subject = "Test", Sender = "Camila Santos (camila@ig.com)"}, new Email { Subject = "Main", Sender = "João Saldanha (joao@ig.com)" } };
            v_Source.DataSource = v_List;
            dataGridViewSearchMails.DataSource = v_Source;
        }

        private void dataGridViewSearchMails_Enter(object sender, EventArgs e)
        { 
            Microsoft.Office.Interop.Outlook.Items items = m_Inbox.Items;

            if (items.Count > 0)
            {
                items.GetFirst().display();
            }


        }
    }
}

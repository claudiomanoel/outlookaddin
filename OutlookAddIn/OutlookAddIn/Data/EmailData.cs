using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookAddIn.Data
{
    public class EmailData
    {
        public EmailData(string p_Subject, string p_Sender)
        {
            Subject = p_Subject;
            Sender = p_Sender;
        }

        public string Subject { get; private set; }
        public string Sender { get; private set; }
    }
}

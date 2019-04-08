using Microsoft.Office.Interop.Outlook;
using OutlookAddIn.Constants;
using OutlookAddIn.Data;
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
        #region Members

        private Microsoft.Office.Interop.Outlook.MAPIFolder m_Inbox;
        private Dictionary<string, MailItem> m_IndexMails = new Dictionary<string, MailItem>();
        private List<string> m_ListCategoryKeys = new List<string>();
        private Dictionary<string, List<string>> m_HashCategory = new Dictionary<string, List<string>>();
        private List<MailItem> m_ListMailItem = new List<MailItem>();

        #endregion

        #region Constructors

        public FormSearchEmails(Microsoft.Office.Interop.Outlook.MAPIFolder p_Inbox, Dictionary<string, List<string>> p_HashCategory, 
            Dictionary<string, MailItem> p_IndexMails)
        {
            InitializeComponent();
            m_Inbox = p_Inbox;
            m_HashCategory = p_HashCategory;
            m_IndexMails = p_IndexMails;
        }

        #endregion

        #region Events

        private void FormSearchEmails_Load(object sender, EventArgs e)
        {
            fillCheckedListBoxLabels();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            searchMail();
        }

        private void dataGridViewSearchMails_Enter(object sender, EventArgs e)
        {
            displayMail();
        }

        #endregion

        #region Methods

        private void fillCheckedListBoxLabels()
        {
            var v_ListFirstLevelCategory = CategoryConstant.ListCategoryGroupItemData;

            foreach(var i_FirstLevelCategory in v_ListFirstLevelCategory)
            {
                foreach(var i_SecondLevelCategory in i_FirstLevelCategory.ListCategorySecondLevel)
                {
                    var v_Key = i_FirstLevelCategory.PrimaryLevelCategory + "_" + i_SecondLevelCategory.SecondLevelCategory;
                    m_ListCategoryKeys.Add(v_Key);
                    string v_Description = new StringBuilder(i_FirstLevelCategory.PrimaryLevelCategory.ToString()).Append(".").Append(i_SecondLevelCategory.SecondLevelCategory).
                        Append("-").Append(i_SecondLevelCategory.SecondLevelCategoryDescription).ToString();
                    checkedListBoxLabels.Items.Add(v_Description);
                }
            }
        }

        private void searchMail()
        {
            BindingSource v_Source = new BindingSource();
            List<EmailData> v_List = new List<EmailData>();

            m_ListMailItem = new List<MailItem>();
            if (m_HashCategory != null)
            {
                Dictionary<string, string> v_ListMails = new Dictionary<string, string>();

                List<string> v_SelectedCategories = getSelectedCategories();

                foreach (var i_SelectedCategory in v_SelectedCategories)
                {
                    if (m_HashCategory.ContainsKey(i_SelectedCategory))
                    {
                        List<string> v_ListMailCategory = m_HashCategory[i_SelectedCategory];

                        if (v_ListMailCategory != null)
                        {
                            foreach (var i_MailCategory in v_ListMailCategory)
                            {
                                if (!v_ListMails.ContainsKey(i_MailCategory))
                                {
                                    v_ListMails.Add(i_MailCategory, i_MailCategory);
                                }
                            }
                        }
                    }
                }

                if (v_ListMails.Count() > 0)
                {
                    foreach (var i_EntryIdMail in v_ListMails)
                    {
                        MailItem v_MailItem = m_IndexMails[i_EntryIdMail.Key];
                        m_ListMailItem.Add(v_MailItem);
                        v_List.Add(new EmailData(v_MailItem.Subject, v_MailItem.SenderEmailAddress));
                    }
                }
                
            }
            
            v_Source.DataSource = v_List;
            dataGridViewSearchMails.DataSource = v_Source;
        }

        private List<string> getSelectedCategories()
        {
            var v_SelectedIndices = checkedListBoxLabels.SelectedIndices;
            List<string> v_SelectedCategories = new List<string>();

            foreach(var i_SelectedIndex in v_SelectedIndices)
            {
                v_SelectedCategories.Add(m_ListCategoryKeys[(int)i_SelectedIndex]);
            }

            return v_SelectedCategories;
        }

        private void displayMail()
        {
            MailItem v_MailItem = m_ListMailItem[dataGridViewSearchMails.CurrentRow.Index];
            v_MailItem.Display();
        }

        #endregion
    }
}

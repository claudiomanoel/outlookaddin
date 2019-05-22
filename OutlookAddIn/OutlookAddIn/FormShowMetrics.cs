using Microsoft.Office.Interop.Outlook;
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
    public partial class FormShowMetrics : Form
    {
        #region Members

        private MailItem m_MailItem;
        #endregion

        #region Constructors

        public FormShowMetrics(MailItem p_MailItem)
        {
            InitializeComponent();
            m_MailItem = p_MailItem;
            this.showMetrics();
        }

        #endregion

        #region Methods

        private void showMetrics()
        {
            string[] v_labels = new string[10];
            float[] v_values = new float[v_labels.Count()];

            v_labels[0] = "Admiration";
            v_labels[1] = "Sympathy";
            v_labels[2] = "Humor";
            v_labels[3] = "Friendship";
            v_labels[4] = "Confidentiality";
            v_labels[5] = "Concern";
            v_labels[6] = "Pride";
            v_labels[7] = "Scorn";
            v_labels[8] = "Jubilation";
            v_labels[9] = "Hope";

            for (int i_Index = 0; i_Index < v_labels.Count(); i_Index++)
            {
                v_values[i_Index] = (v_labels.Count() * 3) - ((i_Index + 1) * 2);
            }

            chartShowMetrics.Legends.Clear();

            var v_Serie = chartShowMetrics.Series[0];
            var v_ChartArea = chartShowMetrics.ChartAreas.FirstOrDefault();
            v_Serie.IsValueShownAsLabel = true;
            v_ChartArea.AxisX.Interval = 1;
            v_ChartArea.AxisX.LabelStyle.Angle = -45;
            v_ChartArea.AxisX.MinorGrid.Enabled = false;
            v_ChartArea.AxisY.MinorGrid.Enabled = false;
            v_ChartArea.AxisX.MajorGrid.Enabled = false;
            v_ChartArea.AxisY.MajorGrid.Enabled = false;

            v_Serie.Points.DataBindXY(v_labels, v_values);
        }

        #endregion
    }
}

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

            for (int i_Index = 0; i_Index < v_labels.Count(); i_Index++)
            {
                v_labels[i_Index] = "ABCAFSA" + i_Index.ToString();
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

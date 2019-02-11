namespace OutlookAddIn
{
    partial class FormShowMetrics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series serie = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowMetrics));
            this.chartShowMetrics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartShowMetrics)).BeginInit();
            this.SuspendLayout();
            // 
            // chartShowMetrics
            // 
            chartArea.Name = "ChartArea";
            this.chartShowMetrics.ChartAreas.Add(chartArea);
            this.chartShowMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            legend.Name = "Legend1";
            this.chartShowMetrics.Legends.Add(legend);
            this.chartShowMetrics.Location = new System.Drawing.Point(0, 0);
            this.chartShowMetrics.Name = "chartShowMetrics";
            serie.ChartArea = "ChartArea";
            serie.Legend = "Legend";
            serie.Name = "Series";
            this.chartShowMetrics.Series.Add(serie);
            this.chartShowMetrics.Size = new System.Drawing.Size(800, 450);
            this.chartShowMetrics.TabIndex = 0;
            this.chartShowMetrics.Text = "chart";
            // 
            // FormShowMetrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartShowMetrics);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormShowMetrics";
            this.Text = "Show Metrics";
            ((System.ComponentModel.ISupportInitialize)(this.chartShowMetrics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartShowMetrics;
    }
}
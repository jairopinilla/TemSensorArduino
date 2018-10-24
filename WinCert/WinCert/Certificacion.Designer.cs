namespace WinCert
{
    partial class Certificacion
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.Grafico = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Grafico)).BeginInit();
            this.SuspendLayout();
            // 
            // Grafico
            // 
            this.Grafico.AccessibleName = "Grafico";
            this.Grafico.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            chartArea1.Name = "ChartArea1";
            this.Grafico.ChartAreas.Add(chartArea1);
            this.Grafico.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.Grafico.Legends.Add(legend1);
            this.Grafico.Location = new System.Drawing.Point(0, 0);
            this.Grafico.Name = "Grafico";
            this.Grafico.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Grafico.Size = new System.Drawing.Size(800, 450);
            this.Grafico.TabIndex = 0;
            this.Grafico.Text = "chart1";
            this.Grafico.Click += new System.EventHandler(this.chart1_Click);
            // 
            // Certificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Grafico);
            this.Name = "Certificacion";
            this.Text = "Certificacion";
            this.Load += new System.EventHandler(this.Certificacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grafico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Grafico;
    }
}
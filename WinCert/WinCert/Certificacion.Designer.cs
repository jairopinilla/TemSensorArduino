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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Grafico = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Cliente = new System.Windows.Forms.TabPage();
            this.Certificador = new System.Windows.Forms.TabPage();
            this.button1limpiar = new System.Windows.Forms.Button();
            this.advertencia = new System.Windows.Forms.Label();
            this.botonInsertar = new System.Windows.Forms.Button();
            this.textBox1Apellido = new System.Windows.Forms.TextBox();
            this.textBox1Nombre = new System.Windows.Forms.TextBox();
            this.textBox1Rut = new System.Windows.Forms.TextBox();
            this.dataGridView1Certificadores = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Camara = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1borrarCertificador = new System.Windows.Forms.Button();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grafico)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Certificador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Certificadores)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(892, 624);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Certificaciones";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Grafico);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(892, 624);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graficos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Grafico
            // 
            this.Grafico.AccessibleName = "Grafico";
            this.Grafico.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.Grafico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.Grafico.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.Grafico.Legends.Add(legend3);
            this.Grafico.Location = new System.Drawing.Point(3, 3);
            this.Grafico.MinimumSize = new System.Drawing.Size(700, 400);
            this.Grafico.Name = "Grafico";
            this.Grafico.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.Grafico.Series.Add(series3);
            this.Grafico.Size = new System.Drawing.Size(800, 600);
            this.Grafico.TabIndex = 1;
            this.Grafico.Text = "chart1";
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleDescription = "";
            this.tabControl1.AccessibleName = "tab1";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.Cliente);
            this.tabControl1.Controls.Add(this.Certificador);
            this.tabControl1.Controls.Add(this.Camara);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabControl1.Location = new System.Drawing.Point(6, 48);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(30);
            this.tabControl1.MinimumSize = new System.Drawing.Size(850, 650);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 650);
            this.tabControl1.TabIndex = 1;
            // 
            // Cliente
            // 
            this.Cliente.AccessibleName = "ClienteTab";
            this.Cliente.Location = new System.Drawing.Point(4, 22);
            this.Cliente.Name = "Cliente";
            this.Cliente.Size = new System.Drawing.Size(892, 624);
            this.Cliente.TabIndex = 2;
            this.Cliente.Text = "Cliente";
            this.Cliente.UseVisualStyleBackColor = true;
            // 
            // Certificador
            // 
            this.Certificador.AccessibleName = "CertificadorTab";
            this.Certificador.Controls.Add(this.button1borrarCertificador);
            this.Certificador.Controls.Add(this.button1limpiar);
            this.Certificador.Controls.Add(this.advertencia);
            this.Certificador.Controls.Add(this.botonInsertar);
            this.Certificador.Controls.Add(this.textBox1Apellido);
            this.Certificador.Controls.Add(this.textBox1Nombre);
            this.Certificador.Controls.Add(this.textBox1Rut);
            this.Certificador.Controls.Add(this.dataGridView1Certificadores);
            this.Certificador.Controls.Add(this.label6);
            this.Certificador.Controls.Add(this.label5);
            this.Certificador.Controls.Add(this.label4);
            this.Certificador.Location = new System.Drawing.Point(4, 22);
            this.Certificador.Margin = new System.Windows.Forms.Padding(20);
            this.Certificador.Name = "Certificador";
            this.Certificador.Padding = new System.Windows.Forms.Padding(10);
            this.Certificador.Size = new System.Drawing.Size(892, 624);
            this.Certificador.TabIndex = 3;
            this.Certificador.Text = "Certificador";
            this.Certificador.UseVisualStyleBackColor = true;
            // 
            // button1limpiar
            // 
            this.button1limpiar.Location = new System.Drawing.Point(564, 81);
            this.button1limpiar.Name = "button1limpiar";
            this.button1limpiar.Size = new System.Drawing.Size(75, 23);
            this.button1limpiar.TabIndex = 14;
            this.button1limpiar.Text = "limpiar";
            this.button1limpiar.UseVisualStyleBackColor = true;
            this.button1limpiar.Click += new System.EventHandler(this.button1limpiar_Click);
            // 
            // advertencia
            // 
            this.advertencia.AutoSize = true;
            this.advertencia.ForeColor = System.Drawing.Color.Red;
            this.advertencia.Location = new System.Drawing.Point(492, 54);
            this.advertencia.Name = "advertencia";
            this.advertencia.Size = new System.Drawing.Size(0, 13);
            this.advertencia.TabIndex = 13;
            // 
            // botonInsertar
            // 
            this.botonInsertar.Location = new System.Drawing.Point(483, 81);
            this.botonInsertar.Name = "botonInsertar";
            this.botonInsertar.Size = new System.Drawing.Size(75, 23);
            this.botonInsertar.TabIndex = 12;
            this.botonInsertar.Text = "insertar";
            this.botonInsertar.UseVisualStyleBackColor = true;
            this.botonInsertar.Click += new System.EventHandler(this.botonInsertar_Click);
            // 
            // textBox1Apellido
            // 
            this.textBox1Apellido.Location = new System.Drawing.Point(107, 83);
            this.textBox1Apellido.Name = "textBox1Apellido";
            this.textBox1Apellido.Size = new System.Drawing.Size(356, 20);
            this.textBox1Apellido.TabIndex = 11;
            // 
            // textBox1Nombre
            // 
            this.textBox1Nombre.Location = new System.Drawing.Point(107, 51);
            this.textBox1Nombre.Name = "textBox1Nombre";
            this.textBox1Nombre.Size = new System.Drawing.Size(356, 20);
            this.textBox1Nombre.TabIndex = 10;
            // 
            // textBox1Rut
            // 
            this.textBox1Rut.Location = new System.Drawing.Point(107, 17);
            this.textBox1Rut.Name = "textBox1Rut";
            this.textBox1Rut.Size = new System.Drawing.Size(356, 20);
            this.textBox1Rut.TabIndex = 9;
            // 
            // dataGridView1Certificadores
            // 
            this.dataGridView1Certificadores.AccessibleName = "GridCertificador";
            this.dataGridView1Certificadores.AllowUserToAddRows = false;
            this.dataGridView1Certificadores.AllowUserToDeleteRows = false;
            this.dataGridView1Certificadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1Certificadores.Location = new System.Drawing.Point(17, 131);
            this.dataGridView1Certificadores.MultiSelect = false;
            this.dataGridView1Certificadores.Name = "dataGridView1Certificadores";
            this.dataGridView1Certificadores.ReadOnly = true;
            this.dataGridView1Certificadores.Size = new System.Drawing.Size(761, 480);
            this.dataGridView1Certificadores.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "Apellido";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "Nombre";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "Rut";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Camara
            // 
            this.Camara.AccessibleName = "CamaraTab";
            this.Camara.Location = new System.Drawing.Point(4, 22);
            this.Camara.Name = "Camara";
            this.Camara.Size = new System.Drawing.Size(892, 624);
            this.Camara.TabIndex = 4;
            this.Camara.Text = "CamaraTab";
            this.Camara.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.AccessibleName = "GeneracionTab";
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(892, 624);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Generacion";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AccessibleName = "Label1Sensor";
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(815, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AccessibleName = "Label2Sensor";
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(720, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AccessibleName = "Label3Sensor";
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(625, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button1borrarCertificador
            // 
            this.button1borrarCertificador.Location = new System.Drawing.Point(645, 81);
            this.button1borrarCertificador.Name = "button1borrarCertificador";
            this.button1borrarCertificador.Size = new System.Drawing.Size(75, 23);
            this.button1borrarCertificador.TabIndex = 15;
            this.button1borrarCertificador.Text = "Borrar";
            this.button1borrarCertificador.UseVisualStyleBackColor = true;
            this.button1borrarCertificador.Click += new System.EventHandler(this.button1borrarCertificador_Click);
            // 
            // Certificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 713);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(900, 700);
            this.Name = "Certificacion";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Certificacion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Certificacion_Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grafico)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Certificador.ResumeLayout(false);
            this.Certificador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Certificadores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Grafico;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage Cliente;
        private System.Windows.Forms.TabPage Certificador;
        private System.Windows.Forms.TabPage Camara;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1Certificadores;
        private System.Windows.Forms.Button botonInsertar;
        private System.Windows.Forms.TextBox textBox1Apellido;
        private System.Windows.Forms.TextBox textBox1Nombre;
        private System.Windows.Forms.TextBox textBox1Rut;
        private System.Windows.Forms.Label advertencia;
        private System.Windows.Forms.Button button1limpiar;
        private System.Windows.Forms.Button button1borrarCertificador;
    }
}
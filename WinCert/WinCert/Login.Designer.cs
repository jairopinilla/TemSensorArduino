namespace WinCert
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textcodigoinput = new System.Windows.Forms.TextBox();
            this.IngresarBoton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textcodigoinput
            // 
            this.textcodigoinput.AccessibleName = "codigoTextInput";
            this.textcodigoinput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textcodigoinput.Location = new System.Drawing.Point(156, 215);
            this.textcodigoinput.Name = "textcodigoinput";
            this.textcodigoinput.Size = new System.Drawing.Size(286, 31);
            this.textcodigoinput.TabIndex = 1;
            // 
            // IngresarBoton
            // 
            this.IngresarBoton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IngresarBoton.Location = new System.Drawing.Point(156, 252);
            this.IngresarBoton.Name = "IngresarBoton";
            this.IngresarBoton.Size = new System.Drawing.Size(286, 35);
            this.IngresarBoton.TabIndex = 2;
            this.IngresarBoton.Text = "Ingresar";
            this.IngresarBoton.UseVisualStyleBackColor = true;
            this.IngresarBoton.Click += new System.EventHandler(this.IngresarBoton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(156, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 47);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ingrese Codigo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IngresarBoton);
            this.Controls.Add(this.textcodigoinput);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codigoInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textcodigoinput;
        private System.Windows.Forms.Button IngresarBoton;
        private System.Windows.Forms.Label label2;
    }
}


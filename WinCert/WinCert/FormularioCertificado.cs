using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCert
{
    public partial class FormularioCertificado : Form
    {
        public FormularioCertificado(string cliente, string camara)
        {
            InitializeComponent();
            this.labelCliente.Text = cliente;
        }


        private void FormularioCertificado_Load(object sender, EventArgs e)
        {

        }
    }
}

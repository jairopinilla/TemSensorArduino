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
    public partial class Login : Form
    {
        public Certificacion certificacionForm = new Certificacion();
        public Login()
        {
            InitializeComponent();
           // this.Close();
            certificacionForm.Show();


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinCert
{
    public partial class FormularioCertificado : Form
    {
        public int Certificacion_id = 0;
        public DataTable dt = new DataTable();
        public float temperatura1 = 0;
        public float temperatura2 = 0;
        public float temperatura3 = 0;


        List<float> pointsArray1 = new List<float>();
        List<float> pointsArray2 = new List<float>();
        List<float> pointsArray3 = new List<float>();

        DataTable dtCamaras = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtCertificadores = new DataTable();
        DataTable dtLineaTemperaturasGenera = new DataTable();

        public FormularioCertificado(int Certificacion_idVar)
        {
            Certificacion_id = Certificacion_idVar;
            InitializeComponent();

           
        }


        private void FormularioCertificado_Load(object sender, EventArgs e)
        {
            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------

            chartGrafico.Series.Add("Sensor 1");
            chartGrafico.Series.Add("Sensor 2");
            chartGrafico.Series.Add("Sensor 3");

            chartGrafico.Series["Sensor 1"].ChartType = SeriesChartType.FastLine;
            chartGrafico.Series["Sensor 1"].Color = Color.Red;

            chartGrafico.Series["Sensor 2"].ChartType = SeriesChartType.FastLine;
            chartGrafico.Series["Sensor 2"].Color = Color.Blue;

            chartGrafico.Series["Sensor 3"].ChartType = SeriesChartType.FastLine;
            chartGrafico.Series["Sensor 3"].Color = Color.Green;

            chartGrafico.Series["Sensor 1"].XValueType = ChartValueType.Time;
            chartGrafico.Series["Sensor 2"].XValueType = ChartValueType.Time;
            chartGrafico.Series["Sensor 3"].XValueType = ChartValueType.Time;




             DateTime lastConnection = DateTime.Now;
             String dateString = "Paine " + lastConnection.ToString("dd") + " de " + lastConnection.ToString("MMMM", new CultureInfo("es-ES")) + " de " +
                "" + DateTime.Now.Year.ToString();

            label8fechaactual.Text = dateString;

            //------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------
        SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = " select " +
                /*int 0*/"Certificacion_id , " +
                  /*int 1*/ "Revision , " +
                  /*int 2*/"certificado , " +
                  /*date 3 */"FechaAprovacion , " +
                   /*string 4 */"CamaraCodigo , " +
                   /*string 5 */"Cliente , " +
                   /*string 6 */"RutCliente , " +
                   /*string 7 */"GiroCliente , " +
                   /*string 8 */"DireccionCliente , " +
                   /*string 9 */"Facturaguia , " +
                   /*string 10 */"Tipo , " +
                   /*string 11 */"Tamano , " +
                    /*int 12 */"Cantidad , " +
                    /*string 13 */"Descripcion , " +
                    /*string 14 */"NombreCertificador , " +
                    /*string 15 */"ApellidoCertificador " +
                    "from Certificacion where Certificacion_id=@Certificacion_id";

            Command.Parameters.AddWithValue("@Certificacion_id", Certificacion_id);

            SQLiteDataReader query = Command.ExecuteReader();

            while (query.Read())
            {

                textBox2Revision.Text = query.GetInt16(0).ToString();
               // labelRevision.Text = query.GetString(1);
              //  labelc.Text = query.GetInt16(2).ToString();
                textBox2FechaAprobacion.Text = query.GetString(3).ToString();
                textBox2Camara.Text = query.GetString(4).ToString();
                textBox2Cliente.Text = query.GetString(5).ToString();
                textBox2Rut.Text = query.GetString(6).ToString();
                textBox2Giro.Text = query.GetString(7).ToString();
                textBox2Direccion.Text = query.GetString(8).ToString();
                textBox2FacturaGuia.Text = query.GetString(9).ToString();
                textBox2Tipo.Text = query.GetString(10).ToString();
                textBox2Tamano.Text = query.GetString(11).ToString();
                textBox2Cantidad.Text = query.GetInt16(12).ToString();
              //  label.Text = query.GetString(13).ToString();
                textBox2nombrecert.Text = query.GetString(14).ToString();
                textBox2ApellidoCert.Text = query.GetString(15).ToString();

            }

            // -------------------------------------------------------------------

            actualizaGraficoGenera(Certificacion_id);


        }


        public void actualizaGraficoGenera(int certificaid)
        {

            //*********************************************************

            SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            conexion.Open();
            string consulta = "select * from LineaCertificacion where Certificacion_id=" + certificaid;
            SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

            DataSet ds = new DataSet();
            ds.Reset();

            dtCamaras = new DataTable();
            db.Fill(ds);

            dtLineaTemperaturasGenera = ds.Tables[0];
            chartGrafico.DataSource = dtLineaTemperaturasGenera;

            conexion.Close();

            //******************************************************


            chartGrafico.Series["Sensor 1"].XValueMember = "Fecha";
            chartGrafico.Series["Sensor 2"].XValueMember = "Fecha";
            chartGrafico.Series["Sensor 3"].XValueMember = "Fecha";

            chartGrafico.Series["Sensor 1"].YValueMembers = "Sensor1";
            chartGrafico.Series["Sensor 2"].YValueMembers = "Sensor2";
            chartGrafico.Series["Sensor 3"].YValueMembers = "Sensor3";

            chartGrafico.ChartAreas[0].AxisY.Minimum = 0;

            chartGrafico.DataBind();

        }


        private void label8_Click(object sender, EventArgs e)
        {
           
        }

        private void chartGrafico_Click(object sender, EventArgs e)
        {

        }

        private void button1Guardar_Click(object sender, EventArgs e)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "update Certificacion set certificado=1, Revision=Certificacion_id  where Certificacion_id=@Entry;";
            Command.Parameters.AddWithValue("@Entry", Certificacion_id);

            SQLiteDataReader query = Command.ExecuteReader();

            MessageBox.Show("Se guardo el reporte en la base de datos de certificaciones");

        }

        private void PrintPanel()
        {
            System.Drawing.Printing.PrintDocument doc = new PrintDocument();
            doc.PrintPage += new PrintPageEventHandler(doc_PrintPage);
            doc.Print();
        }

        private void PrintPanelChart()
        {
            System.Drawing.Printing.PrintDocument doc = new PrintDocument();
            doc.PrintPage += new PrintPageEventHandler(doc_PrintPageChart);
            doc.Print();
        }


        private void doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(panelCertificado.Width, panelCertificado.Height);

            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = bmp.Width;
            float srcHeightPx = bmp.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            bmp.SetResolution(dpiX, dpiY);

            panelCertificado.DrawToBitmap(bmp, panelCertificado.ClientRectangle);

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            e.Graphics.DrawImage(bmp, 0, 0, tgtWidthMM, tgtHeightMM);
        }

        private void doc_PrintPageChart(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(panelGrafico.Width, panelGrafico.Height);

            float tgtWidthMM = 210;  //A4 paper size
            float tgtHeightMM = 297;
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = bmp.Width;
            float srcHeightPx = bmp.Height;
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            bmp.SetResolution(dpiX, dpiY);

            panelGrafico.DrawToBitmap(bmp, panelGrafico.ClientRectangle);

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            e.Graphics.DrawImage(bmp, 0, 0, tgtWidthMM, tgtHeightMM);
        }


        private void button1imprimir_Click(object sender, EventArgs e)
        {
            int pixelsWidth = 612;
            int pixelsHeight = 792;     //1 cm ~ 37.5      
            panelCertificado.Size = new Size(pixelsWidth, pixelsHeight);

            PrintPanel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pixelsWidth = 612;
            int pixelsHeight = 792;     //1 cm ~ 37.5      
            panelGrafico.Size = new Size(pixelsWidth, pixelsHeight);

            PrintPanelChart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8fechaactual_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2Direccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}

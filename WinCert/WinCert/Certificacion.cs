using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SQLite;
using WinCert.Clases;

namespace WinCert
{
    public partial class Certificacion : Form
    {
        System.IO.Ports.SerialPort ArduinoPort;
        public float temperatura1 = 0;
        public float temperatura2 = 0;
        public float temperatura3 = 0;

       
        List<float> pointsArray1 = new List<float>();
        List<float> pointsArray2 = new List<float>();
        List<float> pointsArray3 = new List<float>();


        public Certificacion()
        {
            InitializeComponent();
        }

        private void Certificacion_Load(object sender, EventArgs e)
        {
         
            ArduinoPort = new System.IO.Ports.SerialPort();
            ArduinoPort.PortName = "COM4";  //sustituir por vuestro 
            ArduinoPort.BaudRate = 9600;

            ArduinoPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            ArduinoPort.Open();


            /**************************************************************/

            Grafico.Series.Add("Sensor 1");
            Grafico.Series.Add("Sensor 2");
            Grafico.Series.Add("Sensor 3");



            Grafico.Series["Sensor 1"].ChartType = SeriesChartType.FastLine;
            Grafico.Series["Sensor 1"].Color = Color.Red;

            Grafico.Series["Sensor 2"].ChartType = SeriesChartType.FastLine;
            Grafico.Series["Sensor 2"].Color = Color.Blue;

            Grafico.Series["Sensor 3"].ChartType = SeriesChartType.FastLine;
            Grafico.Series["Sensor 3"].Color = Color.Green;

            Grafico.Series["Sensor 1"].XValueType = ChartValueType.Time;
            Grafico.Series["Sensor 2"].XValueType = ChartValueType.Time;
            Grafico.Series["Sensor 3"].XValueType = ChartValueType.Time;


            /**************************************************************/

            ActualizarListaCertificadores();

        }


public void ActualizarListaCertificadores()
  {
     
      SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
      conexion.Open();
 
     // Lanzamos la consulta y preparamos la estructura para leer datos
     string consulta = "select * from Certificador";

     // Adaptador de datos, DataSet y tabla
    SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

     DataSet ds = new DataSet();
    ds.Reset();

     DataTable dt = new DataTable();
    db.Fill(ds);

     //Asigna al DataTable la primer tabla (ciudades) 
     // y la mostramos en el DataGridView
     dt = ds.Tables[0];

    dataGridView1Certificadores.DataSource = dt;

     // Y ya podemos cerrar la conexion
     conexion.Close();

 }



    private  void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string indata = sp.ReadLine();
            string curTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            temperatura1=float.Parse(indata, CultureInfo.InvariantCulture.NumberFormat);
            temperatura2 = temperatura1 + 3;
            temperatura3 = temperatura1 - 5;

            actualiza(temperatura1, temperatura2, temperatura3);
            Console.WriteLine(temperatura1);
            Console.WriteLine(indata);

        }


        private void actualiza(float t1, float t2, float t3)
        {
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            Console.WriteLine(t3);
            //------------------------------------------------------------------------------------------//

            if (InvokeRequired)
                Invoke(new Action(() => label1.Text = t1.ToString()));

            if (InvokeRequired)
                Invoke(new Action(() => label2.Text = t2.ToString()));

            if (InvokeRequired)
                Invoke(new Action(() => label3.Text = t2.ToString()));

            //--------------------------------------------------------------------------------------------//
            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series["Sensor 1"].Points.AddXY(DateTime.Now, t1)));

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series["Sensor 2"].Points.AddXY(DateTime.Now, t2)));

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series["Sensor 3"].Points.AddXY(DateTime.Now, t3)));







        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void botonInsertar_Click(object sender, EventArgs e)
        {
            advertencia.Text = "";

            if (textBox1Rut.Text == "" || textBox1Nombre.Text == "" || textBox1Apellido.Text == "")
            {
                advertencia.Text = "Faltan Datos";
            }
            else {

                List<String> entries = new List<string>();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select Rut from Certificador where Rut=@Entry;";
                Command.Parameters.AddWithValue("@Entry", textBox1Rut.Text);

                SQLiteDataReader query = Command.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                m_dbConnection.Close();


                if (entries.Count() > 0)
                {

                    advertencia.Text = "El certificador ya existe";

                }
                else {

                    m_dbConnection.Open();

                    Command = new SQLiteCommand();
                    Command.Connection = m_dbConnection;
                    Command.CommandText = "Insert into Certificador(Nombre, Apellido, Rut) values (@nombre,@apellido,@rut);";
                    Command.Parameters.AddWithValue("@rut", textBox1Rut.Text);
                    Command.Parameters.AddWithValue("@nombre", textBox1Nombre.Text);
                    Command.Parameters.AddWithValue("@apellido", textBox1Apellido.Text);

                    query = Command.ExecuteReader();

                    m_dbConnection.Close();

                    ActualizarListaCertificadores();

                    textBox1Rut.Text = "";
                    textBox1Nombre.Text = "";
                    textBox1Apellido.Text = "";


                }


            }
            
        }

        private void button1limpiar_Click(object sender, EventArgs e)
        {
            textBox1Rut.Text = "";
            textBox1Nombre.Text = "";
            textBox1Apellido.Text = "";
        }

        private void button1borrarCertificador_Click(object sender, EventArgs e)
        {

            advertencia.Text = "";

            if (dataGridView1Certificadores.SelectedRows.Count == 0)
            {

                advertencia.Text = "Debe seleccionar un certificador para borrar, la linea entera";

            }
            else {

               string rut= dataGridView1Certificadores.SelectedRows[0].Cells[2].Value.ToString();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "delete from Certificador where Rut=@Entry;";
                Command.Parameters.AddWithValue("@Entry", rut);

                SQLiteDataReader query = Command.ExecuteReader();
                m_dbConnection.Close();

            }

            ActualizarListaCertificadores();

        }
    }
}

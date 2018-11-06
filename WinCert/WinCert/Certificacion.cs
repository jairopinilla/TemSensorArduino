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

        DataTable dtCamaras = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtCertificadores = new DataTable();


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
            ActualizarListaCamaras();
            ActualizarListaClientes();

            comboBox1Camara.DisplayMember = "Codigo";
            comboBox1Camara.ValueMember = "Codigo";
            comboBox1Camara.DataSource = dtCamaras;

            comboBox1Certificador.DisplayMember = "Nombre";
            comboBox1Certificador.ValueMember = "Rut";
            comboBox1Certificador.DataSource = dtCertificadores;

            comboBox1Cliente.DisplayMember = "Nombre";
            comboBox1Cliente.ValueMember = "Rut";
            comboBox1Cliente.DataSource = dtClientes;

            label8AdvertenciaGeneracion.Text = "";
        }

        public void ActualizarListaCamaras()
        {

            SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            conexion.Open();

            // Lanzamos la consulta y preparamos la estructura para leer datos
            string consulta = "select * from Camara";

            // Adaptador de datos, DataSet y tabla
            SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

            DataSet ds = new DataSet();
            ds.Reset();

            dtCamaras= new DataTable();
            db.Fill(ds);

            //Asigna al DataTable la primer tabla (ciudades) 
            // y la mostramos en el DataGridView
            dtCamaras = ds.Tables[0];

            dataGridView1Camara.DataSource = dtCamaras;

            // Y ya podemos cerrar la conexion
            conexion.Close();

        }



        public void ActualizarListaClientes()
        {

            SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            conexion.Open();

            // Lanzamos la consulta y preparamos la estructura para leer datos
            string consulta = "select * from Cliente";

            // Adaptador de datos, DataSet y tabla
            SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

            DataSet ds = new DataSet();
            ds.Reset();

            dtClientes = new DataTable();
            db.Fill(ds);

            //Asigna al DataTable la primer tabla (ciudades) 
            // y la mostramos en el DataGridView
            dtClientes = ds.Tables[0];

            dataGridView1Clientes.DataSource = dtClientes;

            // Y ya podemos cerrar la conexion
            conexion.Close();

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

             dtCertificadores = new DataTable();
            db.Fill(ds);

                    //Asigna al DataTable la primer tabla (ciudades) 
                    // y la mostramos en el DataGridView
                    dtCertificadores = ds.Tables[0];

            dataGridView1Certificadores.DataSource = dtCertificadores;

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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1ingresarCliente_Click(object sender, EventArgs e)
        {

            label7clienteadvertencia.Text = "";

            if (textBox1rutcliente.Text == "" || textBox1nombrecliente.Text == "" || textBox1direccioncliente.Text == ""
                || textBox1girocliente.Text == "")
            {
                label7clienteadvertencia.Text = "Faltan Datos";
            }
            else
            {

                List<String> entries = new List<string>();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select Rut from Cliente where Rut=@Entry;";
                Command.Parameters.AddWithValue("@Entry", textBox1rutcliente.Text);

                SQLiteDataReader query = Command.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                m_dbConnection.Close();


                if (entries.Count() > 0)
                {

                    label7clienteadvertencia.Text = "El Cliente ya existe";

                }
                else
                {

                    m_dbConnection.Open();

                    Command = new SQLiteCommand();
                    Command.Connection = m_dbConnection;
                    Command.CommandText = "Insert into Cliente(Nombre, Giro, Rut, Direccion) values (@nombre,@giro,@rut,@direccion);";
                    Command.Parameters.AddWithValue("@rut", textBox1rutcliente.Text);
                    Command.Parameters.AddWithValue("@nombre", textBox1nombrecliente.Text);
                    Command.Parameters.AddWithValue("@giro", textBox1girocliente.Text);
                    Command.Parameters.AddWithValue("@direccion", textBox1direccioncliente.Text);

                    query = Command.ExecuteReader();

                    m_dbConnection.Close();

                    ActualizarListaClientes();

                    textBox1rutcliente.Text = "";
                    textBox1nombrecliente.Text = "";
                    textBox1girocliente.Text = "";
                    textBox1direccioncliente.Text = "";


                }


            }

        }

        private void button1clienteeliminar_Click(object sender, EventArgs e)
        {

            label7clienteadvertencia.Text = "";

            if (dataGridView1Clientes.SelectedRows.Count == 0)
            {

                label7clienteadvertencia.Text = "Debe seleccionar un cliente para borrar, la linea entera";

            }
            else
            {

                string rut = dataGridView1Clientes.SelectedRows[0].Cells[2].Value.ToString();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "delete from Cliente where Rut=@Entry;";
                Command.Parameters.AddWithValue("@Entry", rut);

                SQLiteDataReader query = Command.ExecuteReader();
                m_dbConnection.Close();

            }

            ActualizarListaClientes();

        }

        private void button1clientelimpiar_Click(object sender, EventArgs e)
        {

            textBox1rutcliente.Text = "";
            textBox1nombrecliente.Text = "";
            textBox1girocliente.Text = "";
            textBox1direccioncliente.Text = "";
        }

        private void button1insertarcamara_Click(object sender, EventArgs e)
        {
            label7advertenciacamara.Text = "";

            if (textBox1codigoCamara.Text == "" )
            {
                label7advertenciacamara.Text = "Faltan Datos";
            }
            else
            {

                List<String> entries = new List<string>();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select Codigo from Camara where Codigo=@Entry;";
                Command.Parameters.AddWithValue("@Entry", textBox1codigoCamara.Text);

                SQLiteDataReader query = Command.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                m_dbConnection.Close();


                if (entries.Count() > 0)
                {

                    label7advertenciacamara.Text = "La camara ya existe";

                }
                else
                {

                    m_dbConnection.Open();

                    Command = new SQLiteCommand();
                    Command.Connection = m_dbConnection;
                    Command.CommandText = "Insert into Camara(Codigo) values (@codigo);";
                    Command.Parameters.AddWithValue("@codigo", textBox1codigoCamara.Text);
                  
                    query = Command.ExecuteReader();

                    m_dbConnection.Close();

                    ActualizarListaCamaras();

                    textBox1codigoCamara.Text = "";
           


                }


            }
        }

        private void button1CamaraLimpiar_Click(object sender, EventArgs e)
        {
            textBox1codigoCamara.Text = "";
        }

        private void button1CamaraEliminar_Click(object sender, EventArgs e)
        {
            label7advertenciacamara.Text = "";

            if (dataGridView1Camara.SelectedRows.Count == 0)
            {

                label7advertenciacamara.Text = "Debe seleccionar una camara para borrar, la linea entera";

            }
            else
            {

                string codigo = dataGridView1Camara.SelectedRows[0].Cells[0].Value.ToString();

                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "delete from Camara where Codigo=@Entry;";
                Command.Parameters.AddWithValue("@Entry", codigo);

                SQLiteDataReader query = Command.ExecuteReader();
                m_dbConnection.Close();

            }

            ActualizarListaCamaras();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            ActualizarListaCamaras();
            ActualizarListaCertificadores();
            ActualizarListaClientes();

            comboBox1Camara.DisplayMember = "Codigo";
            comboBox1Camara.ValueMember = "Codigo";
            comboBox1Camara.DataSource = dtCamaras;

            comboBox1Certificador.DisplayMember = "Nombre";
            comboBox1Certificador.ValueMember = "Rut";
            comboBox1Certificador.DataSource = dtCertificadores;

            comboBox1Cliente.DisplayMember = "Nombre";
            comboBox1Cliente.ValueMember = "Rut";
            comboBox1Cliente.DataSource = dtClientes;
        }

        private void button1Generar_Click(object sender, EventArgs e)
        {

            label8AdvertenciaGeneracion.Text = "";

            if (  
                comboBox1Camara.SelectedValue.ToString() == "" || comboBox1Camara.SelectedValue.ToString() == null 
                || comboBox1Certificador.SelectedValue.ToString() == "" || comboBox1Certificador.SelectedValue.ToString() == null
                || comboBox1Cliente.SelectedValue.ToString() == "" || comboBox1Cliente.SelectedValue.ToString() == null
                || textBox1GeneraTamano.Text == "" || textBox1GeneraTamano.Text == null
                || textBox1GeneraTipo.Text == "" || textBox1GeneraTipo.Text == null
                || textBox1GeneraCantidad.Text == "" || textBox1GeneraCantidad.Text == null
                || textBox1GEneraFacturaGuia.Text == "" || textBox1GEneraFacturaGuia.Text == null

                )
            {

                label8AdvertenciaGeneracion.Text = "Debe Seleccionar todos los campos";

            }
        }

        private void button1Limpiargeneracion_Click(object sender, EventArgs e)
        {
            label8AdvertenciaGeneracion.Text = "";
        }

        private void textBox1GeneraTamano_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

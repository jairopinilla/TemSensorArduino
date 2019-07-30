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
using Newtonsoft.Json.Linq;

namespace WinCert
{
    public partial class Certificacion : Form
    {
        System.IO.Ports.SerialPort ArduinoPort;
        public float temperatura1 = 0;
        public float temperatura2 = 0;
        public float temperatura3 = 0;

        public Boolean GeneraMula = false;
        public Boolean EnCertificacion = false;
        public Boolean esPerandoCumplirTiempoparaCer = false;

        public int idCertificadoenCurso = 0;
        public int tiempoenCertificacion = 0;

        public int temperaturaParaCertificar = 0;
        public int MinutosTerminarCertificacion = 2;


        public DateTime fechaInicioCertificacion;
       


        List<float> pointsArray1 = new List<float>();
        List<float> pointsArray2 = new List<float>();
        List<float> pointsArray3 = new List<float>();

        DataTable dtCamaras = new DataTable();
        DataTable dtClientes = new DataTable();
        DataTable dtCertificadores = new DataTable();
        DataTable dtCertificaciones = new DataTable();
        DataTable dtLineaTemperaturasGenera = new DataTable();


        


        public Certificacion()
        {
            InitializeComponent();
        }

        private void Certificacion_Load(object sender, EventArgs e)
        {

            CheckForIllegalCrossThreadCalls = false;

            TiempoInicio.Enabled = false;
            minutoEnCertificacion.Enabled = false;

            button1VerReporte.Enabled = false;
            button1Generar.Visible = GeneraMula;

            ArduinoPort = new System.IO.Ports.SerialPort();
            ArduinoPort.PortName = "COM3";  //sustituir por vuestro 
            ArduinoPort.BaudRate = 9600;

            ArduinoPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            ArduinoPort.Open();


            /**************************************************************/

            chart1GraficoGenera.Series.Add("Sensor 1");
            chart1GraficoGenera.Series.Add("Sensor 2");
            chart1GraficoGenera.Series.Add("Sensor 3");

            chart1GraficoGenera.Series["Sensor 1"].ChartType = SeriesChartType.FastLine;
            chart1GraficoGenera.Series["Sensor 1"].Color = Color.Red;

            chart1GraficoGenera.Series["Sensor 2"].ChartType = SeriesChartType.FastLine;
            chart1GraficoGenera.Series["Sensor 2"].Color = Color.Blue;

            chart1GraficoGenera.Series["Sensor 3"].ChartType = SeriesChartType.FastLine;
            chart1GraficoGenera.Series["Sensor 3"].Color = Color.Green;

            chart1GraficoGenera.Series["Sensor 1"].XValueType = ChartValueType.Time;
            chart1GraficoGenera.Series["Sensor 2"].XValueType = ChartValueType.Time;
            chart1GraficoGenera.Series["Sensor 3"].XValueType = ChartValueType.Time;


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
            ActualizarListaCertificaciones();

            comboBox1Camara.DisplayMember = "Codigo";
            comboBox1Camara.ValueMember = "Codigo";
            comboBox1Camara.DataSource = dtCamaras;

            tabPage1.Visible = false;

            comboBox1Certificador.DisplayMember = "Nombre";
            comboBox1Certificador.ValueMember = "Rut";
            comboBox1Certificador.DataSource = dtCertificadores;

            comboBox1Cliente.DisplayMember = "Nombre";
            comboBox1Cliente.ValueMember = "Rut";
            comboBox1Cliente.DataSource = dtClientes;

            label8AdvertenciaGeneracion.Text = "";

            botonCancelar.Enabled = false;
        }


        public void ActualizarListaCertificaciones()
        {

            SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            conexion.Open();

            // Lanzamos la consulta y preparamos la estructura para leer datos
            string consulta = "select " +
                  /*int 1*/ "Revision , " +
                  /*int 2*/"certificado , " +
                   /*string 5 */"Cliente , " +
                   /*string 9 */"Facturaguia , " +
                   /*string 10 */"Tipo , " +
                   /*string 11 */"Tamano , " +
                    /*int 12 */"Cantidad , " +
                    /*string 14 */"NombreCertificador  " +
                    " from Certificacion where certificado=1";

            // Adaptador de datos, DataSet y tabla
            SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

            DataSet ds = new DataSet();
            ds.Reset();

            dtCertificaciones = new DataTable();
            db.Fill(ds);
            
            //Asigna al DataTable la primer tabla (ciudades) 
            // y la mostramos en el DataGridView
            dtCertificaciones = ds.Tables[0];

         dataGridViewCertificaciones.DataSource = dtCertificaciones;

            // Y ya podemos cerrar la conexion
            conexion.Close();

        }

        public void actualizaGraficoGenera(int certificaid) {

            //*********************************************************

            SQLiteConnection conexion = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            conexion.Open();
            string consulta = "select * from LineaCertificacion where Certificacion_id="+certificaid;
            SQLiteDataAdapter db = new SQLiteDataAdapter(consulta, conexion);

            DataSet ds = new DataSet();
            ds.Reset();

            dtCamaras = new DataTable();
            db.Fill(ds);

            dtLineaTemperaturasGenera = ds.Tables[0];
            chart1GraficoGenera.DataSource = dtLineaTemperaturasGenera;

            conexion.Close();

            //******************************************************


            chart1GraficoGenera.Series["Sensor 1"].XValueMember = "Fecha";
            chart1GraficoGenera.Series["Sensor 2"].XValueMember = "Fecha";
            chart1GraficoGenera.Series["Sensor 3"].XValueMember = "Fecha";

            chart1GraficoGenera.Series["Sensor 1"].YValueMembers = "Sensor1";
            chart1GraficoGenera.Series["Sensor 2"].YValueMembers = "Sensor2";
            chart1GraficoGenera.Series["Sensor 3"].YValueMembers = "Sensor3";

            chart1GraficoGenera.DataBind();

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

            dtCamaras = new DataTable();
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



        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string indata = sp.ReadLine();
            string curTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            JObject jObject = JObject.Parse(indata);
            var jsonData = JObject.Parse(jObject.ToString());


            var random = new Random();
            int multiplicador = random.Next(0, 2);

            float temp1 = (float)jsonData["temp1"];
            float temp2 = (float)jsonData["temp2"];
            float temp3 = temp2 + multiplicador * 1;

            temperatura1 = temp1;
            temperatura2 = temp2;
            temperatura3 = temp3;

            actualiza(temperatura1, temperatura2, temperatura3);
            Console.WriteLine(temperatura1);
            Console.WriteLine(indata);

            /*****************si esta en certificacion*****************************************************************/

            /*
                public Boolean GeneraMula = false;
                public Boolean EnCertificacion = false;
                public Boolean esPerandoCumplirTiempoparaCer = false;

                public int idCertificadoenCurso = 0;
                public int tiempoenCertificacion = 0;

                public int temperaturaParaCertificar = 0;
                public int MinutosTerminarCertificacion = 3;


                public DateTime fechaInicioCertificacion;
                */

            if (EnCertificacion == true && idCertificadoenCurso != 0) {

                if (temp1 >= temperaturaParaCertificar && temp2 >= temperaturaParaCertificar && temp3 >= temperaturaParaCertificar)
                {

                    tiempoenCertificacion = (DateTime.Now - fechaInicioCertificacion).Minutes;

                    if (InvokeRequired)
                        Invoke(new Action(() => minutoEnCertificacion.Text = tiempoenCertificacion.ToString()));
                  
                    /*****************************************************************************/
                    if (tiempoenCertificacion >= MinutosTerminarCertificacion) {

                        cerrarCertificacionExitosa();

                    }

                }
                else {

                    tiempoenCertificacion = 0;
                }

                /**********************************************/

                ingresaLineaCertificado(idCertificadoenCurso, temperatura1, temperatura2, temperatura3, DateTime.Now);


            }




            /**********************************************************************************/
         

        }

        private void cerrarCertificacionExitosa() {

            /**********************************************************************************/

            System.Windows.Forms.MessageBox.Show("La certificacion ha finalizado de manera exitosa");


            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();



            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "update Certificacion set finalizado=1, certificado=1, Revision=Certificacion_id where Certificacion_id=@Certificacion_id";
         

            Command.Parameters.AddWithValue("@Certificacion_id", idCertificadoenCurso);
            SQLiteDataReader query = Command.ExecuteReader();



            /**********************************************************************************/
            CancelarOTerminarCertificacion();

        }

        private void  ingresaLineaCertificado(int idcertificado, float temp1, float temp2, float temp3, DateTime fecha) {


            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "insert into LineaCertificacion(Certificacion_id, enCertificacion,Sensor1,Sensor2,Sensor3,Fecha ) values" +
                "(@Certificacion_id, @enCertificacion,@Sensor1,@Sensor2,@Sensor3,@Fecha ) ";

            Command.Parameters.AddWithValue("@Certificacion_id", idcertificado);
            Command.Parameters.AddWithValue("@enCertificacion", 1);
            Command.Parameters.AddWithValue("@Sensor1", temp1);
            Command.Parameters.AddWithValue("@Sensor2", temp2);
            Command.Parameters.AddWithValue("@Sensor3", temp3);
            Command.Parameters.AddWithValue("@Fecha", fecha);
 

            SQLiteDataReader query = Command.ExecuteReader();
            dtLineaTemperaturasGenera.Load(query);

   


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

            if (GeneraMula == false)
            {

                if (InvokeRequired)
                    Invoke(new Action(() => chart1GraficoGenera.Series["Sensor 1"].Points.AddXY(DateTime.Now, t1)));

                if (InvokeRequired)
                    Invoke(new Action(() => chart1GraficoGenera.Series["Sensor 2"].Points.AddXY(DateTime.Now, t2)));

                if (InvokeRequired)
                    Invoke(new Action(() => chart1GraficoGenera.Series["Sensor 3"].Points.AddXY(DateTime.Now, t3)));




            }
            else {

                if (InvokeRequired)
                    Invoke(new Action(() => Grafico.Series["Sensor 1"].Points.AddXY(DateTime.Now, t1)));

                if (InvokeRequired)
                    Invoke(new Action(() => Grafico.Series["Sensor 2"].Points.AddXY(DateTime.Now, t2)));

                if (InvokeRequired)
                    Invoke(new Action(() => Grafico.Series["Sensor 3"].Points.AddXY(DateTime.Now, t3)));

            }








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

                string rut = dataGridView1Certificadores.SelectedRows[0].Cells[2].Value.ToString();

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

            if (textBox1codigoCamara.Text == "")
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
           
            button1VerReporte.Enabled = false;

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
            else {

                string codigocamara = comboBox1Camara.SelectedValue.ToString();
                string rutcliente = comboBox1Cliente.SelectedValue.ToString();
                string rutcertificador = comboBox1Certificador.SelectedValue.ToString();
                string tamano = textBox1GeneraTamano.Text;
                string tipo = textBox1GeneraTipo.Text;
                string cantidad = textBox1GeneraCantidad.Text;
                string facturaguia = textBox1GEneraFacturaGuia.Text;

                string fechageneracion = dateTimePicker1InicioGeneracion.Value.ToShortDateString();
                string horageneracion = dateTimePicker1generaciontime.Value.ToShortTimeString();

                string nombrecertificador = "";
                string apellidocertificador = "";

                string nombrecliente = "";
                string girocliente = "";
                string direccioncliente = "";

                button1VerReporte.Enabled = true;
                //----------------------------------------------------------------------


                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select nombre,apellido, rut from Certificador where Rut=@rut;";
                Command.Parameters.AddWithValue("@rut", rutcertificador);

                SQLiteDataReader query = Command.ExecuteReader();

                while (query.Read())
                {
                    nombrecertificador = query.GetString(0);
                    apellidocertificador = query.GetString(1);
                }


                //------------------------------------------------------------------

                Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select nombre,giro, rut, direccion from Cliente where Rut=@rut;";
                Command.Parameters.AddWithValue("@rut", rutcliente);

                query = Command.ExecuteReader();

                while (query.Read())
                {
                    nombrecliente = query.GetString(0);
                    girocliente = query.GetString(1);
                    direccioncliente = query.GetString(3);
                }

                m_dbConnection.Close();

                //------------------------------------------------------------------

 


                label8AdvertenciaGeneracion.Text = "";

                generaDatos(nombrecertificador, apellidocertificador, nombrecliente, girocliente, direccioncliente, rutcliente,
                    codigocamara, tamano, tipo, cantidad, facturaguia, fechageneracion, horageneracion);

            }



        }

        //**************************************************************************************************

        private int ingresaCertificacion(string nombrecert, string apellidocert, string nombreclient, string giroclient, string direccionclient,
    string rutclient, string codigocamara, string tamano, string tipo, string cantidad, string facturaguia, string fecha, string hora)
        {


            int Certificacion_id = 0;

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "insert into Certificacion (finalizado,certificado, FechaAprovacion, CamaraCodigo, Cliente, " +
                " RutCliente,GiroCliente,DireccionCliente,Facturaguia,Tipo,Tamano,Cantidad,NombreCertificador,ApellidoCertificador) " +
                "values   (0,0, @FechaAprovacion, @CamaraCodigo, @Cliente, " +
                " @RutCliente,@GiroCliente,@DireccionCliente,@Facturaguia,@Tipo,@Tamano,@Cantidad,@NombreCertificador,@ApellidoCertificador) ";


            Command.Parameters.AddWithValue("@FechaAprovacion", fecha);
            Command.Parameters.AddWithValue("@CamaraCodigo", codigocamara);
            Command.Parameters.AddWithValue("@Cliente", nombreclient);
            Command.Parameters.AddWithValue("@RutCliente", rutclient);
            Command.Parameters.AddWithValue("@GiroCliente", giroclient);
            Command.Parameters.AddWithValue("@DireccionCliente", direccionclient);
            Command.Parameters.AddWithValue("@Facturaguia", facturaguia);
            Command.Parameters.AddWithValue("@Tipo", tipo);
            Command.Parameters.AddWithValue("@Tamano", tamano);
            Command.Parameters.AddWithValue("@Cantidad", cantidad);
            Command.Parameters.AddWithValue("@NombreCertificador", nombrecert);
            Command.Parameters.AddWithValue("@ApellidoCertificador", apellidocert);

            SQLiteDataReader query = Command.ExecuteReader();

            while (query.Read())
            {
                Console.WriteLine(query.GetString(0));

            }

            m_dbConnection.Close();

            //**************************************************************************************************
            m_dbConnection.Open();

            Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "SELECT Certificacion_id from Certificacion order by Certificacion_id DESC limit 1";
            query = Command.ExecuteReader();

            while (query.Read())
            {
                Certificacion_id = query.GetInt16(0);
            }

            m_dbConnection.Close();


            DateTime fechainicio = DateTime.Parse(fecha + " " + hora);

            return Certificacion_id;


        }
        //**************************************************************************************************
        private void generaDatos(string nombrecert, string apellidocert, string nombreclient, string giroclient, string direccionclient,
            string rutclient, string codigocamara, string tamano, string tipo, string cantidad, string facturaguia, string fecha, string hora) {

            //**************************************************************************************************

            int Certificacion_id=0;

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "insert into Certificacion (finalizado,certificado, FechaAprovacion, CamaraCodigo, Cliente, " +
                " RutCliente,GiroCliente,DireccionCliente,Facturaguia,Tipo,Tamano,Cantidad,NombreCertificador,ApellidoCertificador) " +
                "values   (0,0, @FechaAprovacion, @CamaraCodigo, @Cliente, " +
                " @RutCliente,@GiroCliente,@DireccionCliente,@Facturaguia,@Tipo,@Tamano,@Cantidad,@NombreCertificador,@ApellidoCertificador) ";


            Command.Parameters.AddWithValue("@FechaAprovacion", fecha);
            Command.Parameters.AddWithValue("@CamaraCodigo", codigocamara);
            Command.Parameters.AddWithValue("@Cliente", nombreclient);
            Command.Parameters.AddWithValue("@RutCliente", rutclient);
            Command.Parameters.AddWithValue("@GiroCliente", giroclient);
            Command.Parameters.AddWithValue("@DireccionCliente", direccionclient);
            Command.Parameters.AddWithValue("@Facturaguia", facturaguia);
            Command.Parameters.AddWithValue("@Tipo", tipo);
            Command.Parameters.AddWithValue("@Tamano", tamano);
            Command.Parameters.AddWithValue("@Cantidad", cantidad);
            Command.Parameters.AddWithValue("@NombreCertificador", nombrecert);
            Command.Parameters.AddWithValue("@ApellidoCertificador", apellidocert);

            SQLiteDataReader query = Command.ExecuteReader();

            while (query.Read())
            {
                Console.WriteLine(query.GetString(0));

            }

            m_dbConnection.Close();

            //**************************************************************************************************
            m_dbConnection.Open();

            Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "SELECT Certificacion_id from Certificacion order by Certificacion_id DESC limit 1";
            query = Command.ExecuteReader();

            while (query.Read())
            {
                Certificacion_id = query.GetInt16(0);
            }

            m_dbConnection.Close();


            DateTime fechainicio = DateTime.Parse(fecha+" "+hora);


            generaGrafico(Certificacion_id, fechainicio);

        }

        public void generaGrafico(int certificacionid, DateTime fechainicio) {

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();


            Random rnd = new Random();

            int sensor1Tem = 0;
            int sensor2Tem = 0;
            int sensor3Tem = 0;

            int sensoren56 = rnd.Next(1, 4);
            int partidaSensorI = rnd.Next(1, 11) + 56;
            int partidaSensorII = rnd.Next(1, 11) + 56;

            if (sensoren56 == 1) { sensor1Tem = 56; sensor2Tem = partidaSensorI; sensor3Tem = partidaSensorII; }
            if (sensoren56 == 2) { sensor2Tem = 56; sensor3Tem = partidaSensorI; sensor1Tem = partidaSensorII; }
            if (sensoren56 == 3) { sensor3Tem = 56; sensor2Tem = partidaSensorI; sensor1Tem = partidaSensorII; }

            /*
            LineaCertificacion_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Certificacion_id INTEGER, " +
                    "enCertificacion integer, " +
                    "Sensor1 real, Sensor2 real, Sensor3 real, Fecha datetime," +
                    "FOREIGN KEY(Certificacion_id) REFERENCES Certificacion(Certificacion_id)) ";
                    */


            for (int i = 0; i < 45; i++)
            {

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "insert into LineaCertificacion(Certificacion_id, enCertificacion,Sensor1,Sensor2,Sensor3,Fecha ) values" +
                    "(@Certificacion_id, @enCertificacion,@Sensor1,@Sensor2,@Sensor3,@Fecha ) ";

                Command.Parameters.AddWithValue("@Certificacion_id", certificacionid);
                Command.Parameters.AddWithValue("@enCertificacion", 1);
                Command.Parameters.AddWithValue("@Sensor1", sensor1Tem);
                Command.Parameters.AddWithValue("@Sensor2", sensor2Tem);
                Command.Parameters.AddWithValue("@Sensor3", sensor3Tem);
                Command.Parameters.AddWithValue("@Fecha", fechainicio);


                sensor1Tem= rnd.Next(0, 2)* rnd.Next(0, 2) + sensor1Tem;
                sensor2Tem = rnd.Next(0, 2) * rnd.Next(0, 2) + sensor2Tem;
                sensor3Tem = rnd.Next(0, 2) * rnd.Next(0, 2) + sensor3Tem;

                SQLiteDataReader query = Command.ExecuteReader();
                dtLineaTemperaturasGenera.Load(query);

                fechainicio =fechainicio.AddMinutes(1);
            }


            actualizaGraficoGenera(certificacionid);

        }

        private void button1Limpiargeneracion_Click(object sender, EventArgs e)
        {
            label8AdvertenciaGeneracion.Text = "";
        }

        private void textBox1GeneraTamano_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1VerReporte_Click(object sender, EventArgs e)
        {
            int Certificacion_id=0;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand Command = new SQLiteCommand();
            Command.Connection = m_dbConnection;
            Command.CommandText = "SELECT Certificacion_id from Certificacion order by Certificacion_id DESC limit 1";
            SQLiteDataReader query = Command.ExecuteReader();

            while (query.Read())
            {
                Certificacion_id = query.GetInt16(0);
            }

            m_dbConnection.Close();

            FormularioCertificado formulario = new FormularioCertificado(Certificacion_id);
            formulario.Show();

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1ActualizarCer_Click(object sender, EventArgs e)
        {
            ActualizarListaCertificaciones();
        }

        private void textBox1GeneraCantidad_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1GeneraCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
  if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
    if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridView1Camara_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label11certadver.Text = "";

            if (dataGridViewCertificaciones.SelectedRows.Count == 0)
            {

                label11certadver.Text = "Debe seleccionar un certificado";

            }
            else {

                string id = dataGridViewCertificaciones.SelectedRows[0].Cells[0].Value.ToString();
                int idint= Int32.Parse(id);

               FormularioCertificado formulario = new FormularioCertificado(idint);
                formulario.Show();


            }
        }

        /**********************************************************************************************************/
        /********************************INICIAR REPORTE**************************************************************************/
        private void Button2_Click(object sender, EventArgs e)
        {

            button1VerReporte.Enabled = false;

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
            else
            {

                string codigocamara = comboBox1Camara.SelectedValue.ToString();
                string rutcliente = comboBox1Cliente.SelectedValue.ToString();
                string rutcertificador = comboBox1Certificador.SelectedValue.ToString();
                string tamano = textBox1GeneraTamano.Text;
                string tipo = textBox1GeneraTipo.Text;
                string cantidad = textBox1GeneraCantidad.Text;
                string facturaguia = textBox1GEneraFacturaGuia.Text;

                string fechageneracion = dateTimePicker1InicioGeneracion.Value.ToShortDateString();
                string horageneracion = dateTimePicker1generaciontime.Value.ToShortTimeString();

                string nombrecertificador = "";
                string apellidocertificador = "";

                string nombrecliente = "";
                string girocliente = "";
                string direccioncliente = "";

                button1VerReporte.Enabled = true;
                //----------------------------------------------------------------------


                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();

                SQLiteCommand Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select nombre,apellido, rut from Certificador where Rut=@rut;";
                Command.Parameters.AddWithValue("@rut", rutcertificador);

                SQLiteDataReader query = Command.ExecuteReader();

                while (query.Read())
                {
                    nombrecertificador = query.GetString(0);
                    apellidocertificador = query.GetString(1);
                }


                //------------------------------------------------------------------

                Command = new SQLiteCommand();
                Command.Connection = m_dbConnection;
                Command.CommandText = "select nombre,giro, rut, direccion from Cliente where Rut=@rut;";
                Command.Parameters.AddWithValue("@rut", rutcliente);

                query = Command.ExecuteReader();

                while (query.Read())
                {
                    nombrecliente = query.GetString(0);
                    girocliente = query.GetString(1);
                    direccioncliente = query.GetString(3);
                }

                m_dbConnection.Close();

                //------------------------------------------------------------------

                //           public Boolean EnCertificacion = false;
                //public int idCertificadoenCurso = 0;
                //public int esPerandoCumplirTiempoparaCer = 0;

                //public int tiempoenCertificacion = 0;

                if (EnCertificacion == false && idCertificadoenCurso == 0 && esPerandoCumplirTiempoparaCer == false) {

                        idCertificadoenCurso=ingresaCertificacion(nombrecertificador, apellidocertificador, nombrecliente, girocliente, direccioncliente, rutcliente,
                          codigocamara, tamano, tipo, cantidad, facturaguia, fechageneracion, horageneracion);

                    iniciaCertificacion();




                                         }

                 label8AdvertenciaGeneracion.Text = "";


         

            }

        }


        private void iniciaCertificacion() {

            button1VerReporte.Enabled = false;
            button1Limpiargeneracion.Enabled = false;
            botonIniciaReporte.Enabled = false;
            botonCancelar.Enabled = true;

            panel1Controles.Enabled = false;


            EstadoText.Text = "En certificacion";
            EstadoText.BackColor = Color.Green;

            TiempoInicio.Text = DateTime.Now.ToString();
            tiempoenCertificacion = 0;

            EnCertificacion = true;
            esPerandoCumplirTiempoparaCer = true;

            fechaInicioCertificacion = DateTime.Now;

        }

        private void TiempoInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void EstadoText_Click(object sender, EventArgs e)
        {

        }

        private void BotonCancelar_Click(object sender, EventArgs e)
        {

            CancelarOTerminarCertificacion();

    }


        private void CancelarOTerminarCertificacion() {

            button1VerReporte.Enabled = true;
            button1Limpiargeneracion.Enabled = true;
            panel1Controles.Enabled = true;

            botonCancelar.Enabled = false;
            botonIniciaReporte.Enabled = true;

            EstadoText.Text = "Pause";
            EstadoText.BackColor = Color.Red;

            EnCertificacion = false;
            esPerandoCumplirTiempoparaCer = false;

            idCertificadoenCurso = 0;
            tiempoenCertificacion = 0;
            fechaInicioCertificacion = DateTime.Now;

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }
    }
}

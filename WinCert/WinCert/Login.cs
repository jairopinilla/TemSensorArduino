using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace WinCert
{
    public partial class Login : Form
    {
        public Certificacion certificacionForm = new Certificacion();

        private const string DBName = "cert.sqlite";
        private const string SQLScript = @"..\..\Util\database.sql";
        private static bool IsDbRecentlyCreated = false;
        public static SQLiteConnection cn;

        public Login()
        {
            InitializeComponent();

            textcodigoinput.Text = "";

            Up();

        }

        public static void Up()
        {
            // Crea la base de datos y registra usuario solo una vez
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
                m_dbConnection.Open();
                //----------------------------------------------------------------
                var commandStr = "CREATE TABLE Acceso(Codigo char(50))";
                SQLiteCommand createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();
                //----------------------------------------------------------------

                commandStr = "CREATE TABLE Certificador(Nombre char(50), Apellido char(50), Rut char(50) PRIMARY KEY)";
                createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();

                //----------------------------------------------------------------
                 commandStr = "CREATE TABLE Cliente(Nombre char(100), Giro char(100), Rut char(100) PRIMARY KEY, Direccion char(500))";
                createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();

                //----------------------------------------------------------------

                commandStr = "CREATE TABLE Certificacion(" +
                       "Certificacion_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                       "Revision char(100), " +
                      "finalizado integer, " +
                      "certificado integer, " +
                      "FechaAprovacion datetime, " +
                       "CamaraCodigo char(500), " +
                       "Cliente char(500), "+
                       "RutCliente char(500), " +
                       "GiroCliente char(500), " +
                       "DireccionCliente char(500), " +
                       "Facturaguia char(500), " +
                       "Tipo char(500), " +
                       "Tamano char(500), " +
                      "Cantidad integer, " +
                       "Descripcion char(1000), " +
                       "NombreCertificador char(500), " +
                       "ApellidoCertificador char(500) )";


                createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();

                //----------------------------------------------------------------

                commandStr = "CREATE TABLE LineaCertificacion(" +
                    "LineaCertificacion_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Certificacion_id INTEGER, " +
                    "enCertificacion integer, " +
                    "Sensor1 real, Sensor2 real, Sensor3 real, Fecha datetime,"+
                    "FOREIGN KEY(Certificacion_id) REFERENCES Certificacion(Certificacion_id)) ";
         createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();

                //----------------------------------------------------------------

                commandStr = "CREATE TABLE Camara(Codigo char(100) PRIMARY KEY)";
                createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();

                //---------------------------------------------------

                SQLiteCommand insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;
                //----------------------------------------------------------------
                insertCommand.CommandText = "INSERT INTO Acceso VALUES (@Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", "certifica2019");

                insertCommand.ExecuteReader();

                //----------------------------------------------------------------
                insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;
                insertCommand.CommandText = "INSERT INTO Acceso VALUES (@Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", "certificamula2019");

                insertCommand.ExecuteReader();

                //----------------------------------------------------------------
                insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;
                insertCommand.CommandText = "INSERT INTO Camara VALUES (@Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", "CL 13136");

                insertCommand.ExecuteReader();

                //----------------------------------------------------------------
                insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;
                insertCommand.CommandText = "INSERT INTO Cliente(Nombre,Giro,Rut,Direccion) VALUES (@Nombre,@Giro,@Rut,@Direccion);";
                insertCommand.Parameters.AddWithValue("@Nombre", "Materiales de Embalaje S.A");
                insertCommand.Parameters.AddWithValue("@Giro", "Fabricacion de otros articulos de papel");
                insertCommand.Parameters.AddWithValue("@Rut", "96.528.070-7");
                insertCommand.Parameters.AddWithValue("@Direccion", "Almirante Riveros N 0351 San Bernardo");

                insertCommand.ExecuteReader();

                //----------------------------------------------------------------
                insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;
                insertCommand.CommandText = "INSERT INTO Certificador(Nombre,Apellido,Rut) values (@Nombre,@Apellido,@Rut);";
                insertCommand.Parameters.AddWithValue("@Nombre", "Priscilla");
                insertCommand.Parameters.AddWithValue("@Apellido", "Navarrete Leon");
                insertCommand.Parameters.AddWithValue("@Rut", "15.549.792-0");

                insertCommand.ExecuteReader();



            }
        }

        //****************************************************************************************************************//

        public static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(
                string.Format("Data Source={0};Version=3;", DBName)
            );

            db.Open();

            return db;
        }

        //****************************************************************************************************************//


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void IngresarBoton_Click(object sender, EventArgs e)
        {

            List<String> entries = new List<string>();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=cert.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand selectCommand = new SQLiteCommand("SELECT Codigo from Acceso", m_dbConnection);
            SQLiteDataReader query = selectCommand.ExecuteReader();

            while (query.Read())
            {
                entries.Add(query.GetString(0));
            }

            m_dbConnection.Close();

            if (entries[0] == textcodigoinput.Text)
            {

                certificacionForm.Show();
                this.Hide();

            }

        }
    }
}

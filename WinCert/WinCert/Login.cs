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

                var commandStr = "CREATE TABLE Acceso(Codigo char(50))";
                SQLiteCommand createTable = new SQLiteCommand(commandStr, m_dbConnection);
                createTable.ExecuteReader();


                SQLiteCommand insertCommand = new SQLiteCommand();
                insertCommand.Connection = m_dbConnection;

                insertCommand.CommandText = "INSERT INTO Acceso VALUES (@Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", "certifica2013");

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

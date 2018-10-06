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

namespace WinCert
{
    public partial class Certificacion : Form
    {
       System.IO.Ports.SerialPort ArduinoPort;
       public float temperatura1 = 0;
       public float temperatura2 = 0;
       public float temperatura3 = 0;

        public string[] seriesArray = { "Temperatura 1", "Temperatura 2","Temperatura 2" };
        public int[] pointsArray = { 0, 0,0 };
        public Series series;
   


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

            this.Grafico.Palette = ChartColorPalette.SeaGreen;
            this.Grafico.Titles.Add("Temperatura");

            ArduinoPort.Open();



        }

        private  void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
      

            //  string indata = sp.ReadExisting();
            string indata = sp.ReadLine();
            string curTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            temperatura1=float.Parse(indata, CultureInfo.InvariantCulture.NumberFormat);
            temperatura2 = temperatura1 + 3;
            temperatura3 = temperatura1 - 5;

            series = this.Grafico.Series.Add(seriesArray[0]);
            series.Points.Add(pointsArray[(int)Math.Round(temperatura1)]);

            Console.WriteLine(temperatura1);
            Console.WriteLine(indata);
            //  Console.WriteLine(curTime + "," + indata);  //write to console


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

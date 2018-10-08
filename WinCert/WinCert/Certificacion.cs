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

        public string[] seriesArray = { "Temperatura 1", "Temperatura 2", "Temperatura 2" };
        List<float> pointsArray = new List<float>();

  

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

            this.Grafico.Legends.Add("Temperatura 1");
            this.Grafico.Series.Add(seriesArray[0]);
            this.Grafico.Series[0].ChartType = SeriesChartType.Line;


            pointsArray.Add(0);
            pointsArray.Add(1);
            pointsArray.Add(2);
            pointsArray.Add(3);
            pointsArray.Add(4);


            this.Grafico.Series[0].Points.DataBindY(pointsArray);

            this.Grafico.DataBind();

           

            ArduinoPort.Open();



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

            pointsArray.Add(t1);

            if (InvokeRequired)
                Invoke(new Action(() =>  Grafico.Series[0].Points.DataBindY(pointsArray)));

          

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

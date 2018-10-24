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

            Grafico.Series.Add("Temperatura 1");
            Grafico.Series.Add("Temperatura 2");

       

            Grafico.Series["Temperatura 1"].ChartType = SeriesChartType.FastLine;
            Grafico.Series["Temperatura 1"].Color = Color.Red;

            Grafico.Series["Temperatura 2"].ChartType = SeriesChartType.FastLine;
            Grafico.Series["Temperatura 2"].Color = Color.Blue;

            Grafico.Series["Temperatura 1"].XValueType = ChartValueType.Time;
            Grafico.Series["Temperatura 2"].XValueType = ChartValueType.Time;
 




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

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series["Temperatura 1"].Points.AddXY(DateTime.Now, t1)));

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series["Temperatura 2"].Points.AddXY(DateTime.Now, t2)));



            /*

            if (InvokeRequired)
                Invoke(new Action(() =>  Grafico.Series[0].Points.DataBindY(pointsArray1)));

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series[1].Points.DataBindY(pointsArray2)));

            if (InvokeRequired)
                Invoke(new Action(() => Grafico.Series[2].Points.DataBindY(pointsArray3)));

    */



        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

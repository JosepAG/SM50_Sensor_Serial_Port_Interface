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
using System.Globalization;
using System.IO;
using System.Diagnostics;

namespace Port
{
    public partial class Form1 : Form
    {
        
        int data2;
        int data3;
        int data4;
        int data5;
        int data6;
        int data7;
        int data8;
        int data9;
        int data10;
        int data11;
        int data12;
        int data13;
        int data14;
        int data15;
        int data16;
        string combine;
        float salida;
        string receivedData;
        Single salidabien;
        DateTime dataahora;
        String LinePort;
        int LineBaud;
        public Form1()
        {

            InitializeComponent();
            getAvailablePorts();
            chart1.Series["Ozone"].IsValueShownAsLabel = true;

        }
        void getAvailablePorts()
        {
            String[] ports= SerialPort.GetPortNames();
            
            comboBox1.Items.AddRange(ports);
        }

        ///START-UP////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
             int start = 1;
             if(start == 1)
             {
               ///////Leer Configuración inicial Port/Bauds///////////////////////
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader("Port.txt");
                    
                    //Read the first line of text
                    LinePort = sr.ReadLine();
                    comboBox1.Text = LinePort;
                    String LineBaudS = sr.ReadLine();
                    LineBaud = Int32.Parse(LineBaudS);
                    comboBox1.Text = LinePort;
                    comboBox2.Text = LineBaudS;
                    data_tb.Text = "Wait 1 minute to load the data";
                    sr.Close();


                }
                catch (Exception)
                {
                    Console.WriteLine("Introduce the Start-up port and Bauds in the Port.txt");
                }
                finally
                {
                    Console.WriteLine("Conected to "+LinePort+" with "+LineBaud+" Baudrate");
                   }
                //////////////////////////////////////////////////////////
              
                 serialPort1.PortName = LinePort;
                 serialPort1.Close();
                 serialPort1.BaudRate = LineBaud;
                 serialPort1.Parity = Parity.None;
                 serialPort1.StopBits = StopBits.One;
                 serialPort1.DataBits = 8;
                 serialPort1.Handshake = Handshake.None;
                 serialPort1.RtsEnable = false;

                 serialPort1.DataReceived += new
                     SerialDataReceivedEventHandler(port_DataReceived);
                 serialPort1.Open();
                 timer1.Enabled = true;
                 value_pb.Value = 100;
                 stop_btn.Enabled = true;
                 start_btn.Enabled = false;
                start = 0;
                
             }
            
        }
        
        /////////////////////////////////////
      

        private void start_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    data_tb.Text = "Please select port settings";

                }
                else
                {
                    StreamWriter ss = new StreamWriter("Port.txt");

                    ss.WriteLine(comboBox1.Text);
                    ss.WriteLine(comboBox2.Text);
                    ss.Close();
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Close();
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.DataBits = 8;
                    serialPort1.Handshake = Handshake.None;
                    serialPort1.RtsEnable = false;
                    
                    serialPort1.DataReceived += new
                        SerialDataReceivedEventHandler(port_DataReceived);
                    serialPort1.Open();
                    timer1.Enabled = true;
                    value_pb.Value = 100;
                    stop_btn.Enabled = true;
                    start_btn.Enabled = false;

                }
            }
            catch (UnauthorizedAccessException)
            {
                data_tb.Text = "Unauthorized Acess";
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            timer1.Enabled = false;
            value_pb.Value = 0;
            start_btn.Enabled = true;
            stop_btn.Enabled = false;

        }
        private void port_DataReceived(object sender,
                                 SerialDataReceivedEventArgs e)
        {
           
            data2 = serialPort1.ReadByte();
            string hexdata2 = data2.ToString("X");
            data3 = serialPort1.ReadByte();
            string hexdata3 = data3.ToString("X");
            data4 = serialPort1.ReadByte();
            string hexdata4 = data4.ToString("X");
            data5 = serialPort1.ReadByte();
            string hexdata5 = data5.ToString("X");
            data6 = serialPort1.ReadByte();
            string hexdata6 = data6.ToString("X");
            data7 = serialPort1.ReadByte();
            string hexdata7 = data7.ToString("X");
            data8 = serialPort1.ReadByte();
            string hexdata8 = data8.ToString("X");
            data9 = serialPort1.ReadByte();
            string hexdata9 = data9.ToString("X");
            data10 = serialPort1.ReadByte();
            string hexdata10 = data10.ToString("X");
            data11 = serialPort1.ReadByte();
            string hexdata11 = data11.ToString("X");
            data12 = serialPort1.ReadByte();
            string hexdata12 = data12.ToString("X");
            data13 = serialPort1.ReadByte();
            string hexdata13 = data13.ToString("X");
            data14 = serialPort1.ReadByte();
            string hexdata14 = data14.ToString("X");
            data15 = serialPort1.ReadByte();
            string hexdata15 = data15.ToString("X");
            data16 = serialPort1.ReadByte();
            string hexdata16 = data16.ToString("X");
            if (hexdata7.Length == 1)
            {
                hexdata7 = hexdata7.PadLeft(2, '0');    
            }
            if (hexdata6.Length == 1)
            {
                hexdata6 = hexdata6.PadLeft(2, '0');
            }
            if (hexdata5.Length == 1)
            {
                hexdata5 = hexdata5.PadLeft(2, '0');
            }
            if (hexdata4.Length == 1)
            {
                hexdata4 = hexdata4.PadLeft(2, '0');
            }
            combine = (hexdata7) + (hexdata6) + (hexdata5) + (hexdata4);
            Int32 IntRep = Int32.Parse(combine, NumberStyles.AllowHexSpecifier);
            salida = BitConverter.ToSingle(BitConverter.GetBytes(IntRep), 0);
            salidabien = salida * 1000;
            dataahora = DateTime.Now;
            String SalidaData = salida + "," + dataahora;
            File.AppendAllText("Data.txt", SalidaData + Environment.NewLine);

            this.Invoke(new EventHandler(Showdata));

        }

        private void Showdata(object sender, EventArgs e)
        {

            double salida1 = Math.Round((salida * 1000),3);
            data_tb.Text = salida1.ToString() + " ppb ";
           /* if (data_tb.Text.Length > 13)
            {
                int foundS1 = data_tb.Text.IndexOf(" ");
                int foundS2 = data_tb.Text.IndexOf(",",foundS1+1);
                if (foundS1 != foundS2 && foundS1 >= 0)
                {

                    data_tb.Text = data_tb.Text.Remove(foundS1 + 1, foundS2 - foundS1);

                    
                }
            }
            */

            dataahora = DateTime.Now;
            Console.WriteLine(dataahora);
            chart1.Series["Ozone"].Points.AddXY(dataahora, Math.Round(salidabien, 2));
            if (chart1.Series["Ozone"].Points.Count > 20) { 
            chart1.Series["Ozone"].Points.RemoveAt(0);
                chart1.ResetAutoValues();
            }    
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e) 
        {
            receivedData = combine;
          
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            File.WriteAllText("DataForCsv.txt", File.ReadAllText("Headers.txt") + Environment.NewLine+File.ReadAllText("Data.txt"));
            Process.Start("notepad.exe", "DataForCsv.txt");

        }
    }
}

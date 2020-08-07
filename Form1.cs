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
using System.Runtime.Hosting;

namespace Port
{
    public partial class Form1 : Form
    {
        int pth;
        String pathSave;
        int Cont;
        int Control;
        int Control2;
        int Activar;
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
        float salidaAVG;
        String SalidaDataAVG;
        float AVG;
        string receivedData;
        Single salidabien;
        Single salidabien2;
        DateTime dataahora1;
        DateTime TimeControl;
        String dataahora;
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
             
                StreamReader sw = new StreamReader("Path.txt");
                pathSave = sw.ReadLine();
                sw.Close();
                button2.Text = "Data is saved in: " + pathSave;
              
            


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
                    data_tb.Text = "Wait 1 minute to load the data plot.";
                    data_tb2.Text = "Wait to get the 30 minutes average data plot.";
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
                if(SerialPort.GetPortNames().ToList().Contains(LinePort)){
                    serialPort1.Open();
                }
                else
                {
                    MessageBox.Show("The predetermined COM Port is not available, please change it." ,"COM port error", MessageBoxButtons.OK);

                }
                 timer1.Enabled = true;
                 value_pb.Value = 100;
                 stop_btn.Enabled = true;
                 start_btn.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;

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
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;

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
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
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
            dataahora1 = DateTime.Now;
            
            dataahora = dataahora1.ToString("dd/MM/yyyy HH:mm");


            ////////////////////AVG 30 mins/////////////////////////


           

            if ((dataahora1.Minute == 01 && Control2==0) || (dataahora1.Minute == 31 && Control2==0) || (dataahora1.Minute == 02 && Control==1) || (dataahora1.Minute == 32 && Control==1))
                {                   
                    Cont = 0;
                    Activar = 1;
                    Control = 0;
                }

                if (Activar == 1)
                {
                    //Cont = Cont+1;

                    if (salida != 0)
                    {
                        Cont = Cont + 1;
                        salidaAVG = salidaAVG + salida;
                    }
                    if (dataahora1.Minute == 00 || TimeControl.Minute == 30 || (dataahora1.Minute == 01 && Control2 == 1) || (dataahora1.Minute == 31 && Control2 == 1))
                        {
                            if (Cont != 0)
                             {
                             AVG = salidaAVG / Cont;
                             SalidaDataAVG = AVG + "," + dataahora;
                             salidabien2 = AVG * 1000;
                             File.AppendAllText("DataAvg.txt", SalidaDataAVG + Environment.NewLine);
                             File.WriteAllText("DataForCsvAvg.txt", File.ReadAllText("HeadersAvg.txt") + Environment.NewLine + File.ReadAllText("DataAvg.txt"));
                             File.WriteAllText(pathSave +"/DataForCsvAvg.txt", File.ReadAllText("HeadersAvg.txt") + Environment.NewLine + File.ReadAllText("DataAvg.txt"));                           
                            this.Invoke(new EventHandler(Showdata2));                             
                             }
                            salidaAVG = 0;
                            Cont = 0;
                            Activar = 0;
                            Control = 1;
                            Control2 = 0;
                        }
                    if(TimeControl.Minute == 59 || TimeControl.Minute == 29)
                    {
                        Control2 = 1;
                    }
                    
                }
            




            /////////////////////////////////////////////////////
            
            String SalidaData = salida + "," + dataahora;
            File.AppendAllText("Data.txt", SalidaData + Environment.NewLine);
            File.WriteAllText("DataForCsv.txt", File.ReadAllText("Headers.txt") + Environment.NewLine + File.ReadAllText("Data.txt"));
            File.WriteAllText(pathSave + "/DataForCsv.txt", File.ReadAllText("Headers.txt") + Environment.NewLine + File.ReadAllText("Data.txt"));
            

            this.Invoke(new EventHandler(Showdata));

        }

        private void Showdata(object sender, EventArgs e)
        {
            double salida1 = Math.Round((salida * 1000),3);
            data_tb.Text = salida1.ToString() + " ppb ";
            String dataahora2 = dataahora1.ToString("HH:mm");

            chart1.Series["Ozone"].Points.AddXY(dataahora2, Math.Round(salidabien, 1));
            if (chart1.Series["Ozone"].Points.Count > 30) { 
            chart1.Series["Ozone"].Points.RemoveAt(0);
                chart1.ResetAutoValues();
            }
        }
        private void Showdata2(object sender, EventArgs e)
        {
            double salida2 = Math.Round((AVG * 1000), 3);     
            data_tb2.Text = salida2.ToString() + " ppb ";
            String dataahora3 = dataahora1.ToString("d/M/yy HH:mm");

            chart2.Series["Ozone"].Points.AddXY(dataahora3, Math.Round(salidabien2, 1));
            if (chart2.Series["Ozone"].Points.Count > 30)
            {
                chart2.Series["Ozone"].Points.RemoveAt(0);
                chart2.ResetAutoValues();
            }
        }

        private void timer1_Tick(object sender, EventArgs e) 
        {           
            TimeControl = DateTime.Now;          
        }

        private void save_btn_Click(object sender, EventArgs e)
        {           
            Process.Start("notepad.exe", "DataForCsv.txt");
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {           
            Process.Start("notepad.exe", "DataForCsvAvg.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog In_path = new FolderBrowserDialog();
            if(In_path.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("The data in .txt format will be saved in: "+In_path.SelectedPath);
                pathSave = In_path.SelectedPath;
                StreamWriter sp = new StreamWriter("Path.txt");
                sp.WriteLine(pathSave);                              
                sp.Close();
                button2.Text = "Data is saved in: " + In_path.SelectedPath;               
            }
        }


    }
}

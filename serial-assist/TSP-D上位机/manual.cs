using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TSP_D上位机
{
    public partial class Manual : Form
    {

        //实例化串口对象
        SerialPort serialPort = new SerialPort();

        public Manual()
        {
            InitializeComponent();
        }

        //初始化串口界面参数设置
        private void Init_Port_Confs()
        {
            /*------串口界面参数设置------*/

            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }
            //添加串口
            foreach (string s in str)
            {
                comboBox1.Items.Add(s);
            }
            //设置默认串口选项
            comboBox1.SelectedIndex = 0;
            AsciiradioButton1.Checked = true;

        }

        private void Manual_Load(object sender, EventArgs e)
        {
            Init_Port_Confs();

            Control.CheckForIllegalCrossThreadCalls = false;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);


            //准备就绪              
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //设置数据读取超时为1秒
            serialPort.ReadTimeout = 1000;

            serialPort.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)//串口处于关闭状态
            {

                try
                {

                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Error: 无效的端口,请重新选择", "Error");
                        return;
                    }
                    string strSerialName = comboBox1.SelectedItem.ToString();

                 

                    serialPort.PortName = strSerialName;//串口号
                    serialPort.BaudRate =38400;//波特率
                    serialPort.DataBits =8;//数据位
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;

                    //打开串口
                    serialPort.Open();

                    //打开串口后设置将不再有效
                    button9.Text = "关闭串口";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    return;
                }
            }
            else //串口处于打开状态
            {

                serialPort.Close();//关闭串口
                //串口关闭时设置有效
                comboBox1.Enabled = true;

                button9.Text = "打开串口";            

            }
        }
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
               
                DateTime dateTimeNow = DateTime.Now;               
                //textBox1.Text += string.Format("{0}\r", dateTimeNow);
                textBox1.ForeColor = Color.Red;    //改变字体的颜色    
                if (AsciiradioButton1.Checked == true)
                {
                    Byte[] receivedData = new Byte[serialPort.BytesToRead];        //创建接收字节数组
                    serialPort.Read(receivedData, 0, receivedData.Length);
                    serialPort.DiscardInBuffer();
                    string strRcv = null;
                    for (int i = 0; i < receivedData.Length; i++)
                    {
                        strRcv += receivedData[i].ToString("X2");
                       // strRcv += " ";
                       // System.Threading.Thread.Sleep(5);
                    }
                    byte[] buff = new byte[strRcv.Length / 2];
                    int index = 0;
                    for (int i = 0; i < strRcv.Length; i += 2)
                    {
                        buff[index] = Convert.ToByte(strRcv.Substring(i, 2), 16);
                        ++index;
                    }
                    string result = Encoding.Default.GetString(buff);
                    textBox1.Text = result + "\r\n";
                    // String input = serialPort.ReadLine();
                    //textBox1.Text += input + "\r\n";
                }
                else
                {
                    Byte[] receivedData = new Byte[serialPort.BytesToRead];        //创建接收字节数组
                    serialPort.Read(receivedData, 0, receivedData.Length);
                    serialPort.DiscardInBuffer();
                    string strRcv = null;
                    for (int i = 0; i < receivedData.Length; i++)
                    {
                        strRcv += receivedData[i].ToString("X2");
                        strRcv += " ";
                        System.Threading.Thread.Sleep(5);
                    }
                    textBox1.Text += strRcv + "\r\n";

                    //  string input = serialPort.ReadLine();
                    // char[] values = input.ToCharArray();
                    // foreach (char letter in values)
                    // {
                    ///  // Get the integral value of the character.
                        //  int value = Convert.ToInt32(letter);
                        //   // Convert the decimal value to a hexadecimal value in string form.
                        //   string hexOutput = String.Format("{0:X}", value);
                        //  textBox1.AppendText(hexOutput + " ");
                        //  textBox1.SelectionStart = textBox1.Text.Length;
                        //   textBox1.ScrollToCaret();//滚动到光标处
                        //   textBox1.Text += hexOutput + " ";

                }
                }
                                                                                          
              //  textBox1.SelectionStart = textBox1.Text.Length;
               // textBox1.ScrollToCaret();//滚动到光标处
               // serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer           
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 
           // textBox1.Text = "";
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }

            String strSend = "|00FFWR0D018101";//发送框数据
            serialPort.WriteLine(strSend);//发送一行数据 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 
          //  textBox1.Text = "";
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }

            String strSend = "|00FFWR0D012001";//发送框数据
            serialPort.WriteLine(strSend);//发送一行数据
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 
           // textBox1.Text = "";
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }
                if (button4.BackColor == Color.Transparent)
                {
                    String strSend = "|00FFBW0M5073011";//发送框数据
                    serialPort.WriteLine(strSend);//发送一行数据
                    button4.BackColor = Color.SpringGreen;
                    button4.Text = "关闭夹爪";
                }
                else if (button4.BackColor == Color.SpringGreen)
                {
                    String strSend = "|00FFBW0M5074011";//发送框数据
                    serialPort.WriteLine(strSend);//发送一行数据
                    button4.BackColor = Color.Transparent;
                    button4.Text = "打开夹爪";
                }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 
          //  textBox1.Text = "";
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }

            String strSend = "|00FFBR0M080001";//发送框数据
            serialPort.WriteLine(strSend);//发送一行数据
        }
     
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSP_D上位机
{
    public partial class Main : Form
    {
        private Alarm almfrm = null;
        private double intOKCounts = 0;
        private double intNGCounts = 0;
        private double intTotalCounts = 0;
        private string[] oldState = new string[16];
        private double[] ccdData = new double[100];    
        private double[] Weight = new double[100];
        private double[] FinalWt = new double[100];
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            global.tcpPLC = new ModbusTCP();

            if (!global.tcpPLC.tcpConnect("169.254.199.61"))
            {
                MessageBox.Show("PLC连接错误!");
            }
        }

        private void 登录toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login admin = new Login();
            admin.ShowDialog();
        }

        private void 手动toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Manual manualfrm = new Manual();
            manualfrm.ShowDialog();
        }

        private void 参数设置toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Parameter_Setting tl = new Parameter_Setting();
            tl.ShowDialog();
        }

        private void 计数清零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intOKCounts = 0;
            intNGCounts = 0;
            intTotalCounts = 0;
        }

        private void 退出toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show(this, "要退出当前界面吗？", "是否退出？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (dr)
            {
                case System.Windows.Forms.DialogResult.Yes://保存修改    
                    this.Close();
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 报警触发
            global.alarmString = global.tcpPLC.readD("20");


            global.alarmString = Convert.ToString(Convert.ToInt32(global.alarmString), 2).PadLeft(4, '0');
            if (global.alarmString != "0000")
            //报警发生
            {
                if (almfrm.Visible == false)
                {
                    almfrm.Visible = true;                              //报警框弹出
                }
            }
            else
            {
                if (almfrm.Visible == true)
                {
                    almfrm.Visible = false;
                }
            }

            #endregion
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (global.adm)
            {
                参数设置toolStripMenuItem3.Visible = true;
                手动toolStripMenuItem2.Visible = true;
            }
            else
            {
                参数设置toolStripMenuItem3.Visible = false;
                手动toolStripMenuItem2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Transparent)
            {
                global.tcpPLC.writeD("0", "1");
                button1.Text = "自动中";
                button1.BackColor = Color.SpringGreen;
            }
            else if (button1.BackColor == Color.SpringGreen)
            {
                global.tcpPLC.writeD("0", "0");
                button1.Text = "手动中";
                button1.BackColor = Color.Transparent;
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == Color.Transparent)
            {
                global.tcpPLC.SetM("2");
                button2.BackColor = Color.SpringGreen;
            }
            else if (button2.BackColor == Color.SpringGreen)
            {
                global.tcpPLC.RstM("2");
                button2.BackColor = Color.Transparent;
            }
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            global.tcpPLC.SetM("3");
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("3");
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            global.tcpPLC.SetM("4");
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("4");
        }
    }
}

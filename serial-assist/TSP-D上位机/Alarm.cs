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
    public partial class Alarm : Form
    {
        string[] oldState = new string[16];
        public Alarm()
        {
            InitializeComponent();
        }
        public void almAdd(string almMsg)
        {
            listBox1.Items.Add(almMsg);
        }
        public void almRemove(string almMsg)
        {
            listBox1.Items.Remove(almMsg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] newState = new string[16];
            newState[0] = global.alarmString.Substring(2, 1);
            if (oldState[0] != newState[0])
            {
                oldState[0] = newState[0];
                if (newState[0] == "1")
                {
                    this.almAdd("紧急停止");
                }
                else
                {
                    this.almRemove("紧急停止");
                }
            }
        }


    }
}

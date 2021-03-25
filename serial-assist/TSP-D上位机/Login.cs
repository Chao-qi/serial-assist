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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            global.adm = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "123456")
            {
                global.adm = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }
    }
}

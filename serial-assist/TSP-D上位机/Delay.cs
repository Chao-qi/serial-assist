using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSP_D上位机
{
    class Delay
    {
        #region 延时函数
        public static void delay(uint ms)
        {
            int dwStart = System.Environment.TickCount;
            while (System.Environment.TickCount - dwStart < ms)
            {
                Application.DoEvents();
            }
        }
        #endregion
    }
}

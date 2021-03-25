using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_D上位机
{
    class global
    {
        public static ModbusTCP tcpPLC;
        public static TimeSpan startTime, endTime, cycleTime;//开始时间，结束时间
        public static string alarmString = "0000";
        public static bool adm;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Connection
{
    public class ConnectionInfoDTO
    {
        public DateTime ConnectionTimeStamp { get; set; }
        public bool ConnectionIsAlive { get; set; }
        public string ServerHostInstanceName { get; set; }
        public string ClientHostInstanceName { get; set; }
        public string ClientIP { get; set; }
        public string ServerIP { get; set; }
        public int Port { get; set; }
        public string Socket { get; set; }
        //Starting time of activity
        public DateTime StartTime { get; }
        //Ending time of activity
        public DateTime EndTime { get; }
        //total time to perform action
        public double Period { get; }
    }
}

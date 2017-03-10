using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Client
{
    public class ClientConnectionDTO
    {
        public string ClienttHostName { get; set; }
        public string ClientIP { get; set; }
        public string ClientPort { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
 
    }
}

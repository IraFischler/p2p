using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.File
{
    [Serializable]
    [DataContract]
    public class FileResponseDTO
    {
        [DataMember]
        public string Ip { get; set; }
        [DataMember]
        public int Port { get; set; }
    }
}

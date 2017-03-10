using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace p2p.Entities.File
{
    [XmlRoot("FileSearchDTO")]
    [Serializable]
    [DataContract]
    public class FileSearchDTO
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string SearchText { get; set; }
    }
}

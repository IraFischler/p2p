using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace p2p.Entities.User
{
    [XmlRoot("UserRegisterDTO")]
    [Serializable]//to transfer in the net
    [DataContract]//wcf can identify 
    public class UserRegisterDTO
    {
        [DataMember]//wcf can identify 
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        
    }
}

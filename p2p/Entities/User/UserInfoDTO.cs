using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.User
{
    [Serializable]//to transfer in the net
    [DataContract]//wcf can identify 
    public class UserInfoDTO
    {
        [DataMember]
        public long  Id { get; set; }
        [DataMember]//wcf can identify 
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool Enabled { get; set; }



        public override string ToString()
        {
            return UserName + " | " + Password+ " | " + Email;
        }

    }
}

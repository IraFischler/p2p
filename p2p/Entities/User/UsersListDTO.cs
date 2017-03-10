using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.User
{
    [Serializable]
    [DataContract]
    public class UsersListDTO
    {
        [DataMember]
        public List<UserInfoDTO> Users { get; set; }

        [DataMember]
        public string SearchResult { get; set; }
    }
}

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
    public class FileRequestDTO
    {
 
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        //public decimal FileSize { get; set; }
        //[DataMember]
        public int Id { get; set; }
        




    }
}

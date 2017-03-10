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
    public class FileInfoDTO
    {
        [DataMember]
        public string FileName { get; set; }
        //txt, movie, pdf, jpg etc...
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        //File size represented in MB
        public decimal FileSize { get; set; }

        [DataMember]
        public long NumOfUsers { get; set; }

        public override string ToString()
        {
            return FileName + FileType + "  | size:  " + FileSize + "MB" ;
        }

    }
}

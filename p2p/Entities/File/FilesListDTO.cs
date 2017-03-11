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
    public class FilesListDTO
    {
        [DataMember]
        public List<FileInfoDTO> Files { get; set; }

        [DataMember]
        public string SearchResult { get; set; }
    }
}


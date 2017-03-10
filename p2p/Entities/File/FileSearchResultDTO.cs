using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace p2p.Entities.File
{
    [XmlRoot("FileSearchResultDTO")]
    [Serializable]
    [DataContract]
    public class FileSearchResultDTO
    {
        [DataMember]
        [XmlArray("Files"), XmlArrayItem(typeof(FileInfoDTO), ElementName = "File")]
        public List<FileInfoDTO> Files { get; set; }

        [DataMember]
        public string SearchResult { get; set; }
    }
}

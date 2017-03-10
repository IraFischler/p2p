using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p2p.Entities.File;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace p2p.Entities
{
    [XmlRoot("UserLoginDTO")]
    [Serializable]
    [DataContract]
    public  class UserLoginDTO
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Ip { get; set; }
        [DataMember]
        public int Port { get; set; }
        [DataMember]
        public string UploadPath { get; set; }
        [DataMember]
        public string DownloadPath { get; set; }
        [DataMember]
        [XmlArray("Files"), XmlArrayItem(typeof(FileInfoDTO), ElementName = "File")]
        public List<FileInfoDTO> Files { get; set; }
    }
}

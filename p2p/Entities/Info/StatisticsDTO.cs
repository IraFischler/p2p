using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Info
{
    [Serializable]
    [DataContract]
    public class StatisticsDTO
    {
        [DataMember]
        public long NumOfUsers { get; set; }

        [DataMember]
        public long NumOfActiveUsers { get; set; }

        [DataMember]
        public long NumOfFiles { get; set; }
    }
}

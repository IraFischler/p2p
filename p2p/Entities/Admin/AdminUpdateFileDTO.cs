using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Admin
{
    public class AdminUpdateFileDTO
    {
        //Do I realy need it?????
        public string FileName { get; set; }
        //txt, movie, pdf etc...
        public string FileType { get; set; }
        public int FileSize { get; set; }
    }
}

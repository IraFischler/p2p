using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Admin
{
    public class AdminDisableEnableUserDTO
    {
        public int UserID { get; set; }
        //the admin is able to enable/disable user whitout remove him
        public bool IsEnable { get; set; }
    }
}

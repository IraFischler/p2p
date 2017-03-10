using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Admin
{
    public class AdminCreateUserDTO
    {
        public string CreateUserName { get; set; }
        public string CreateUserNamePassword { get; set; }
        public string CreateUserEmail { get; set; }
        public DateTime CreateUserDateOfBirth { get; set; }
        public int CreateUserID { get; set; }
        public DateTime TimeStampe { get; set; }
    }
}

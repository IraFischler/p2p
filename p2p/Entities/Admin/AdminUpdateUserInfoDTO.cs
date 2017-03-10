using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2p.Entities.Admin
{
    public class AdminUpdateUserInfoDTO
    {
        public string UserName { get; set; }
        public string NewUserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string DateOfBirth { get; set; }
        public string NewDateOfBirth { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public DateTime TimeStampe { get; set; }
    }
}

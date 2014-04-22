using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captone.Models
{
    public class StaffModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string BackupPassword { get; set; }
        public string Email { get; set; }
        public int StationID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorridorWPF.Models
{
    class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public bool isAdmin { get; set; }
        public string roomNr { get; set; }
    }
}

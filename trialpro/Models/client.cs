using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trialpro.Models
{
    public class Client
    {
        public string username { get; set; }
        public string password { get; set; }
        public Client(string Username,string Password)
        {
            this.username = Username;
            this.password = Password;
        }
    }
}

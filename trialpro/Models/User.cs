﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trialpro.Models
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string mailId { get; set; }
        public string otp { get; set; }
    }
}

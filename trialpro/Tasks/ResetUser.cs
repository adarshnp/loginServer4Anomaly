﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using trialpro.Models;
using trialpro.Services;

namespace trialpro.Tasks
{
    public class ResetUser
    {
        private readonly IConnectionProvider con;
        private readonly IUserProcessor upcr;
        private readonly IUserProvider updr;
        private readonly ITokenProvider token;
        public string otpstring;
        private readonly string conStr;

        public ResetUser(IConnectionProvider con, IUserProcessor upcr, IUserProvider updr, ITokenProvider token)
        {
            this.con = con;
            this.upcr = upcr;
            this.updr = updr;
            this.token = token;
        }
        public async Task<bool> CheckOtp(string otp, string username)
        {
            Client c = new Client(username, "none");
            IDbConnection db = con.GetConnection();
            User u = new User();
            string otpstring = await upcr.GetOtp(u);
            if (otpstring == otp)
            {
                Console.WriteLine("otp confirmed");
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task ResetPassword(string password)
        {
            User user = new User();
            IDbConnection db = con.GetConnection();
            int nra = await upcr.ChangePassword(user, password);
        }
    }
}

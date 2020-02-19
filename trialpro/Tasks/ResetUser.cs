using System;
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
        public string otpstring;

        public ResetUser(IConnectionProvider con, IUserProcessor upcr)
        {
            this.con = con;
            this.upcr = upcr;
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

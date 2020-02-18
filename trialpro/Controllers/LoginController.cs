using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trialpro.Models;
using trialpro.Services;
using trialpro.Tasks;
using System.Data;

namespace trialpro.Controllers
{
    public class LoginController : ControllerBase
    {
        private bool OTP_MATCHED = false;
        private readonly string conStr;
        private readonly IConnectionProvider con;
        private readonly IUserProcessor upcr;
        private readonly IUserProvider updr;
        private readonly ITokenProvider token;
        RequestOtp otp;
        IDbConnection db;
        public LoginController(IConnectionProvider con, IUserProcessor upcr, IUserProvider updr, ITokenProvider token, DBConnectionConfig config,RequestOtp requestOtp)
        {
            otp = requestOtp;
            this.con = con;
            this.upcr = upcr;
            this.updr = updr;
            this.token = token;
            conStr = config.MyConnectionString;
        }
        [Route("signin")]
        [HttpPost]
        public async Task<string> loginEndpoint(string username, string password)
        {
            Login log = new Login(con, upcr, updr, token);
            return await log.login(username, password);
        }


        [Route("signup")]
        [HttpPost]
        public async Task<string> SignUpEndpoint(string username, string password)
        {
            Logup log = new Logup(con, upcr, updr, token);
            return await log.logup(username, password);
        }


        [Route("requestotp")]
        [HttpPost]
        public async Task OtpEndpoint(string username)
        {
            db = con.GetConnection();
            await otp.getOtp(username, db);
        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task ResetEndpoint(string username, string password, string otp)
        {
            ResetUser reset = new ResetUser(con, upcr, updr, token);
            OTP_MATCHED = await reset.CheckOtp(otp, username);
            if (OTP_MATCHED)
            {
                await reset.ResetPassword(password);
            }
        }  
    }
}
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

        private readonly Login log;
        private readonly Logup logup;
        private readonly RequestOtp otp;
        private readonly ResetUser resetUser;

        public LoginController(Login log, Logup logup, RequestOtp otp, ResetUser resetUser)
        {
            this.log = log;
            this.logup = logup;
            this.otp = otp;
            this.resetUser = resetUser;
        }

        [Route("signin")]
        [HttpPost]
        public async Task<string> loginEndpoint(string username, string password)
        {
            return await log.login(username, password);
        }


        [Route("signup")]
        [HttpPost]
        public async Task<string> SignUpEndpoint(string username, string password)
        {
            return await logup.logup(username, password);
        }


        [Route("requestotp")]
        [HttpPost]
        public async Task OtpEndpoint(string username)
        {
            await otp.getOtp(username);
        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task ResetEndpoint(string username, string password, string otp)
        {
            OTP_MATCHED = await resetUser.CheckOtp(otp, username);
            if (OTP_MATCHED)
            {
                await resetUser.ResetPassword(password);
            }
        }  
    }
}
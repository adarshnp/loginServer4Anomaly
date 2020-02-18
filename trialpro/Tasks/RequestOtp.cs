using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using trialpro.Models;
using trialpro.Services;

namespace trialpro.Tasks
{
    public class RequestOtp
    {
        IUserProcessor _userProcessor;
        SentMail s;
        public RequestOtp(IUserProcessor userProcessor,SentMail sentMail)
        {
            _userProcessor = userProcessor;
            s = sentMail;
        }
        public async Task getOtp(string username,IDbConnection db)
        { 
            OTPGenerator og = new OTPGenerator();
            string otp = await og.GenerateOtp();
            User user = new User();
            int no =await _userProcessor.PushOtp(user, otp);
            s.SentOtp(otp);
            await Task.CompletedTask;
        }
    }
}

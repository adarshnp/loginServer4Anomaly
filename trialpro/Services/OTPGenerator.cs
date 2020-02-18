using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public class OTPGenerator
    {
        public Task<string> GenerateOtp()
        {
            Random rand = new Random();
            string sample = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string otp = "";
            for(int i=0;i<6;i++)
            {
                otp += sample[rand.Next(62)];
            }
            return Task.FromResult(otp); 
        }
    }
}

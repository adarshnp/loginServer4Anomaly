using System;
using System.Threading.Tasks;
using trialpro.Models;

namespace trialpro.Services
{
    public class UserProvider : IUserProvider
    {
        public Task<bool> CheckPassword(Client c, User u)
        {
            if(c.password == u.password)
            {
                Console.WriteLine("password matched");
                return Task.FromResult(true);
            }
            else
            {
                Console.WriteLine("password error");
                return Task.FromResult(false);
            }
        }
        public Task<bool> CheckUserName(Client c, User u)
        {
            if (u!=null && c.username == u.username)
            {
                Console.WriteLine("username found");
                return Task.FromResult(true);
            }
            else
            {
                Console.WriteLine("username not exist");
                return Task.FromResult(false);
            }
        }
    }
}

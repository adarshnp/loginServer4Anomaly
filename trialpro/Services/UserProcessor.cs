using System.Threading.Tasks;
using trialpro.Models;
using Dapper;
using System.Data;
using System.Linq;
using System;

namespace trialpro.Services
{
    public class UserProcessor : IUserProcessor
    {
        private IDbConnection db =>connectionProvider.GetConnection();
        private readonly IConnectionProvider connectionProvider;

        public UserProcessor(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }

        public async Task<int> Create(Client c)
        {
            int no_rows_affected =await db.ExecuteAsync("insert into user (Username,Password,MailId) values(@username,@password,@mailId)", c);
            return no_rows_affected;
        }
        public async Task<User> GetUser(Client c)
        {
            User user = (await db.QueryAsync<User>("select * from user where username = @username",c)).FirstOrDefault();
            return user;
        }
        public async Task<int> PushOtp(User u,string otp)
        {
            int no_rows_affected = await db.ExecuteAsync("UPDATE user SET otp = @otp WHERE username=@username ", new{ otp,username = u.username });
            return no_rows_affected;
        }
        public async Task<string> GetOtp(User u)
        {
            string otp = (await db.QueryAsync<string>("select otp from user where username=@username", new { username = u.username })).FirstOrDefault();
            return otp;
        }
        public async Task<int> ChangePassword(User u,string password)
        {
            int no_rows_affected = await db.ExecuteAsync("UPDATE user SET password = @password WHERE username=@username ", new { password=password, username = u.username });
            return no_rows_affected;
        }
    }
}

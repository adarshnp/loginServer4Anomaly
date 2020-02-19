using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using trialpro.Models;
using trialpro.Services;

namespace trialpro.Tasks
{
    public class Login
    {
        private readonly IConnectionProvider con;
        private readonly IUserProcessor upcr;
        private readonly IUserProvider updr;
        private readonly ITokenProvider token;
        public Login(IConnectionProvider con, IUserProcessor upcr, IUserProvider updr, ITokenProvider token)
        {
            this.con = con;
            this.upcr = upcr;
            this.updr = updr;
            this.token = token;
        }
        public async Task<string> login(string username, string password)
        {
            var USER_AUTHORIZED = false;
            User user = new User();
            //fetch clientinfo
            Client client = new Client(username, password);
            //open connection
            IDbConnection db = con.GetConnection();
            //retrieve userinfo
            user = await upcr.GetUser(client);
            //match user
            var USER_FOUND = await updr.CheckUserName(client, user);
            //match password

            if (USER_FOUND == true)
            {
                var PASSWORD_MATCHED = await updr.CheckPassword(client, user);
                if (PASSWORD_MATCHED == true)
                {
                    USER_AUTHORIZED = true;
                }
            }
            else
            {
                Console.WriteLine("try again");
            }
            if (USER_AUTHORIZED == true)
            {
                Console.WriteLine("token sent to user");
                return await token.createToken(user);
            }
            else
            {
                Console.WriteLine("SORRY!!!token cant be sent");
                return "wrong token";
            }
        }
    }
}

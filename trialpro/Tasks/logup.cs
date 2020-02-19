using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using trialpro.Models;
using trialpro.Services;

namespace trialpro.Tasks
{
    public class Logup
    {


        private readonly IConnectionProvider con;
        private readonly IUserProcessor upcr;
        private readonly IUserProvider updr;
        private readonly ITokenProvider token;
        public Logup(IConnectionProvider con, IUserProcessor upcr, IUserProvider updr, ITokenProvider token)
        {
            this.con = con;
            this.upcr = upcr;
            this.updr = updr;
            this.token = token;
        }
        public async Task<string> logup(string username, string password)
        {
            var USER_AUTHORIZED = false;
            User user = null;
            //fetch clientinfo
            Client client = new Client(username, password);
            //open connection
            IDbConnection db = con.GetConnection();
            //retrieve userinfo
            user = await upcr.GetUser(client);
            if (user == null)
            {
                USER_AUTHORIZED = true;
                if( await upcr.Create(client) == -1)
                {
                    return "user created";
                }
            }
            else
            {
                USER_AUTHORIZED = false;
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

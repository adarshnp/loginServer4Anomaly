using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trialpro.Models;
using trialpro.Services;
using System.Data;

namespace trialpro.Controllers
{
    public class LoginController : ControllerBase
    {
        public bool USER_FOUND = false;
        public bool PASSWORD_MATCHED = false;
        public bool USER_AUTHORIZED = false;

        private readonly IConnectionProvider con;
        private readonly IUserProcessor upcr;
        private readonly IUserProvider updr;
        private readonly ITokenProvider token;

        private readonly string conStr;

        public LoginController(IConnectionProvider con, IUserProcessor upcr, IUserProvider updr, ITokenProvider token,DBConnectionConfig config)
        {
            this.con = con;
            this.upcr = upcr;
            this.updr = updr;
            this.token = token;
            conStr = config.MyConnectionString;
        }
        [Route("get")]
        [HttpGet]
        public string GetCSTR() 
        {
            return conStr;
        }
        [Route("signin")]
        [HttpPost]
        public async Task<int> login(string username, string password)
        {
            User user = new User();
            //fetch clientinfo
            Client client = new Client(username, password);
            //open connection
            IDbConnection db = await con.GetConnection();
            //retrieve userinfo
            user = await upcr.GetUser(client, db);
            //match user
            USER_FOUND = await updr.CheckUserName(client, user);
            //match password
            if (USER_FOUND == true)
            {
                PASSWORD_MATCHED = await updr.CheckPassword(client, user);
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
                return -1;
            }


        }
        [Route("signup")]
        [HttpPost]
        public async Task<int> logup(string username, string password)
        {

            User user = null;
            //fetch clientinfo
            Client client = new Client(username, password);
            //open connection
            IDbConnection db = await con.GetConnection();
            //retrieve userinfo
            user = await upcr.GetUser(client, db);
            if (user == null)
            {
                USER_AUTHORIZED = true;
                return await upcr.Create(client, db);
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
                return -1;
            }


        }
    }
}
using System.Threading.Tasks;
using trialpro.Models;
using Dapper;
using System.Data;
using System.Linq;

namespace trialpro.Services
{
    public class UserProcessor : IUserProcessor
    {
        public async Task<int> Create(Client c, IDbConnection db)
        {
            int no_rows_affected =await db.ExecuteAsync("insert into user (Username,Password) values(@username,@password)", c);
            return no_rows_affected;
        }

        public async Task<User> GetUser(Client c,IDbConnection db)
        {
            User user = (await db.QueryAsync<User>("select * from user where username = @username",c)).FirstOrDefault();
            return user;
        }
    }
}

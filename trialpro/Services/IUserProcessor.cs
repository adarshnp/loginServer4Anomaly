using System.Data;
using System.Threading.Tasks;
using trialpro.Models;

namespace trialpro.Services
{
    //provide service from db
    public interface IUserProcessor
    {
        Task<int> Create(Client c, IDbConnection db);
        Task<User> GetUser(Client c, IDbConnection db);//fetch user from db
    }
}

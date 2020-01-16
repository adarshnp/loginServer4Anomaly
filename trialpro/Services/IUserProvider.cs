using System.Threading.Tasks;
using trialpro.Models;
namespace trialpro.Services
{
    //provide service to db
    public interface IUserProvider
    {
        //Task<Client> FetchClient(string username,string password);
        Task<bool> CheckUserName(Client c,User u);
        Task<bool> CheckPassword(Client c,User u);
        //Task<bool> CheckUser(Client c,User u);
    }
}

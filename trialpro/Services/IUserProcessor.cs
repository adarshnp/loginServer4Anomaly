using System.Data;
using System.Threading.Tasks;
using trialpro.Models;

namespace trialpro.Services
{
    //provide service from db
    public interface IUserProcessor
    {
        Task<int> Create(Client c);
        Task<User> GetUser(Client c);
        Task<int> PushOtp(User u, string otp);
        Task<string> GetOtp(User u);
        Task<int> ChangePassword(User u, string password);
    }
}

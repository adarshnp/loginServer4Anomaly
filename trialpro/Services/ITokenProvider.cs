using System.Threading.Tasks;
using trialpro.Models;

namespace trialpro.Services
{
    public interface ITokenProvider
    {
        Task<string> createToken(User user);
    }
}

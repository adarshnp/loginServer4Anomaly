using System.Threading.Tasks;
using trialpro.Models;
namespace trialpro.Services
{
    public class TokenProvider:ITokenProvider
    {
        public  Task<int> createToken(User user)
        {
            return Task.FromResult(1000);
        }
    }
}

using System.Threading.Tasks;
using trialpro.Models;
using SessionKeyManager;

namespace trialpro.Services
{
    public class TokenProvider:ITokenProvider
    {
        private ISessionKeyManager sessionKeyManager;
        
        public TokenProvider(ISessionKeyManager sessionKeyManager)
        {
            this.sessionKeyManager = sessionKeyManager;
        }

        public  async Task<string> createToken(User user)
        {

            var token = sessionKeyManager.GenerateNewSessionKey(user.userId.ToString());
            return token;
        }
        public string refreshToken(string UserID)
        {
            return sessionKeyManager.RefreshSessionKey(UserID);
        }
        public void RevokeID(string UserID)
        {
            sessionKeyManager.ReleaseSessionKey(UserID);
        }
    }
}

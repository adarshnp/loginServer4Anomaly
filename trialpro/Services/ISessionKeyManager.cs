using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public interface ISessionKeyManager
    {
        string GenerateNewSessionKey(string userID);
        void ReleaseSessionKey(string key);
    }
}

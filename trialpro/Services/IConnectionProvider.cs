using System.Data;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
        void CloseConnection(IDbConnection db);
    }
}

using System.Data;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public interface IConnectionProvider
    {
        Task<IDbConnection> GetConnection();
        Task CloseConnection(IDbConnection db);
    }
}

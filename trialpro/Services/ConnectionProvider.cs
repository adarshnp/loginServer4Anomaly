using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace trialpro.Services
{
    public class ConnectionProvider : IConnectionProvider
    {
        private string connstring;
        private IDbConnection db;
        public ConnectionProvider(DBConnectionConfig config)
        {
            connstring = config.MyConnectionString;
        }
        public void CloseConnection(IDbConnection db)
        {
            db.Close();
        }

        public IDbConnection GetConnection()
        {
            if (db == null)
            {
                var connection = new MySqlConnection(connstring);
                connection.Open();
                db = connection;
            }
            return db;
        }
    }
}

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
        public ConnectionProvider()
        {
            // connstring = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString; 
            connstring = "server = localhost;database =  projectlogin; userid = anomaly ; password = a4adarsh";
        }
        public Task CloseConnection(IDbConnection db)
        {
            db?.Close();
            return Task.CompletedTask;
        }

        public Task<IDbConnection> GetConnection()
        {
            if (db == null)
            {
                var connection = new MySqlConnection(connstring);
                connection.Open();
                db = connection;
            }
            return Task.FromResult(db);
        }
    }
}

using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
namespace Roulette.Repositories
{
    public class SqlDbConnection
    {
        public SqlConnection connection; 
        public SqlDbConnection()
        {
            var configuration = GetConfiguration();
            connection = new SqlConnection(configuration.GetSection("Data").GetSection("SqlConnectionString").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}

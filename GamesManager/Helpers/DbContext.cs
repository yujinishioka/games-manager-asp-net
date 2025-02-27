using Npgsql;
using System.Data;

namespace GamesManager.Helpers
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        public DbContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
            }
            catch (Exception ex) {
                throw new NpgsqlException("Erro:", ex);
            }
        }
    }
}

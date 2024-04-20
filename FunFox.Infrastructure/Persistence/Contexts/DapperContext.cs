using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FunFox.Infrastructure.Persistence.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetSection("ConnectionStrings")
                .GetValue<string>("DefaultConnection"));
    }
}

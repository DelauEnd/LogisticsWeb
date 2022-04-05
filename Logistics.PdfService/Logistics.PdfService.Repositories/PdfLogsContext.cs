using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Logistics.PdfService.Repositories
{
    public class PdfLogsContext
    {
        private readonly IConfiguration _configuration;
        public PdfLogsContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection() 
            => new SqlConnection(_configuration.GetConnectionString("npgsqlConnection"));
    }
}

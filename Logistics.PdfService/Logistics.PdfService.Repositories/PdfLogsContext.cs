using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;

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
        {
            var con = new NpgsqlConnection(_configuration.GetSection("npgsqlConnection").Value);
            con.Open();
            return con;
        }
    }
}

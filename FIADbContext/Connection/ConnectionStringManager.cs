using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FIADbContext.Connection
{
    class ConnectionStringManager
    {
        public string ConnectionString { get; set; }

        public ConnectionStringManager(string appsettingsPath = "appsettings.json",
            string connectionStringName = "DefaultConnection",
            string environmentVaiableName = "FIADb_ConnectionString")
        {

            var preConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            var config = preConfig.AddJsonFile(appsettingsPath, optional: true)
                .Build();

            ConnectionString = string.Format(config.GetConnectionString(connectionStringName)
                ?? Environment.GetEnvironmentVariable(environmentVaiableName));
        }
    }
}

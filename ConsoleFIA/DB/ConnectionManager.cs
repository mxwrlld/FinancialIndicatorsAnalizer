using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ConsoleFIA.DB
{
    class ConnectionManager
    {
        public string ConnectionString { get; set; }

        public ConnectionManager(string appsettingsPath = "appsettings.json",
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

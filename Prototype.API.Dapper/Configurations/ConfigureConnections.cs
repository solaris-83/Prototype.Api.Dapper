using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prototype.API.Domain.DbInfoConnection;
using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Prototype.API.Dapper.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = string.Empty;
            DBConnectionEnum dBConnectionType = DBConnectionEnum.SQLServer;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (configuration.GetConnectionString("Sqlite") != null)
                {
                    connection = GetConnection().ConnectionString;
                    dBConnectionType = DBConnectionEnum.SQLite;
                    CreateDb();
                }
                else if (configuration.GetConnectionString("SQLServerWindows") != null)
                {
                    connection = configuration.GetConnectionString("SQLServerWindows");
                }
                else
                    throw new ArgumentException("Database string not configured");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("SQLServerDocker") ??
                                 "Server=localhost,1433;Database=Chinook;User=sa;Password=Pa55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddSingleton(new DbInfo(connection, dBConnectionType));

            return services;
        }



        private static string DbFile
        {
            get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\SQLITEDB1.sqlite"; }
        }

        private static SQLiteConnection GetConnection(string dbPath = null)
        {
            var path = dbPath ?? DbFile;
            return new SQLiteConnection("Data Source=" + path);
        }

        private static void CreateDb()
        {
            if (!File.Exists(DbFile))
            {
                try
                {
                    using (var cnn = GetConnection())
                    {
                        cnn.Open();

                        var createDbQuery = File.ReadAllText(@"Database\script_SQLITE.sql");

                        cnn.Execute(createDbQuery);
                    }
                }
                catch(Exception)
                {
                    if (File.Exists(DbFile))
                        File.Delete(DbFile);
                }
            }
        }
    }
}

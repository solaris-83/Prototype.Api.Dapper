using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prototype.API.Domain.DbInfoConnection;
using System;
using System.Data.SQLite;
using System.IO;
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
                else if (configuration.GetConnectionString("ChinookDBWindows") != null)
                {
                    connection = configuration.GetConnectionString("ChinookDBWindows");
                }
                else
                    throw new ArgumentException("Database string not configured");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("ChinookDbDocker") ??
                                 "Server=localhost,1433;Database=Chinook;User=sa;Password=Pa55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddSingleton(new DbInfo(connection, dBConnectionType));

            return services;
        }



        private static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\SQLITEDB1.sqlite"; }
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
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }
    
}

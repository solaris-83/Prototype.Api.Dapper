

namespace Prototype.API.Domain.DbInfoConnection
{
    public class DbInfo : IDbInfo
    {
        public string ConnectionStrings { get; }

        public DBConnectionEnum DBConnectionType { get; set; }

        public DbInfo(string connectionStrings, DBConnectionEnum dbConnectionType)
        {
            ConnectionStrings = connectionStrings; DBConnectionType = dbConnectionType;
        }
    }
}
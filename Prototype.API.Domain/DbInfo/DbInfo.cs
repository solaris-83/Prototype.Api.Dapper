namespace Prototype.API.Domain
{
    public class DbInfo: IDbInfo
    {
        public string ConnectionStrings { get; }

        public DbInfo(string connectionStrings) => ConnectionStrings = connectionStrings;
    }
}
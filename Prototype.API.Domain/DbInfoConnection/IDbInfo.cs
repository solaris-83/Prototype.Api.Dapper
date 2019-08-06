
namespace Prototype.API.Domain.DbInfoConnection
{ 
    public interface IDbInfo
    {
        string ConnectionStrings { get; }

        DBConnectionEnum DBConnectionType { get; set; }
    }
}
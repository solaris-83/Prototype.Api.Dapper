namespace Prototype.API.Domain
{ 
    public interface IDbInfo
    {
         string ConnectionStrings { get; }
        // TODO: Come back and look into why this wasn't being used and instead the DbInfo was.
        // I would have imagined this having he ConnectionString property on it.
    }
}
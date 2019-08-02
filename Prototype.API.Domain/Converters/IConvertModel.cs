
namespace Prototype.API.Domain.Converters
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert { get; }
    }
}

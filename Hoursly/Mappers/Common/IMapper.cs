namespace Hoursly.Mappers.Common
{
    public interface IMapper<in TEntity, out TModel>
    {
        TModel MapFrom(TEntity source);
    }
}
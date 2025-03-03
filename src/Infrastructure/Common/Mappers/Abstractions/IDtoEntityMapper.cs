namespace Common.Mappers.Abstractions;

public interface IDtoEntityMapper<TDto, TEntity>
{
    TEntity Map(TDto dto);
}
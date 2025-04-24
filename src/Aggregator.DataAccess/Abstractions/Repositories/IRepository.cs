using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория, предоставляющий базовые операции CRUD для сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Асинхронно получает сущность по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность типа <typeparamref name="T"/> или null, если не найдена.</returns>
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получает все сущности типа <typeparamref name="T"/>.
    /// </summary>
    /// <returns>Список всех сущностей.</returns>
    IQueryable<T> GetAll();
    
    /// <summary>
    /// Получает все сущности, удовлетворяющие заданному предикату.
    /// </summary>
    /// <param name="predicate">Условие для фильтрации сущностей.</param>
    /// <returns>Список сущностей, удовлетворяющих условию.</returns>
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Асинхронно проверяет существование сущности, удовлетворяющей заданному предикату.
    /// </summary>
    /// <param name="predicate">Условие для поиска сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если сущность найдена; в противном случае, false.</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Асинхронно добавляет новую сущность в репозиторий.
    /// </summary>
    /// <param name="entity">Сущность для добавления.</param>
    void Add(T entity);
    
    /// <summary>
    /// Удаляет указанную сущность из репозитория.
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    void Remove(T entity);
    
    /// <summary>
    /// Асинхронно находит первую сущность, удовлетворяющую заданному предикату.
    /// </summary>
    /// <param name="predicate">Условие для поиска сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Найденная сущность или null, если ни одна не удовлетворяет условию.</returns>
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получает список сущностей по их идентификаторам с использованием сырого SQL-запроса.
    /// </summary>
    /// <param name="ids">Коллекция идентификаторов сущностей.</param>
    /// <returns>Запрос найденных сущностей.</returns>
    IQueryable<T>  GetQueryByIds(IReadOnlyCollection<long> ids);
    
    /// <summary>
    /// Асинхронно добавляет коллекцию сущностей в репозиторий.
    /// </summary>
    /// <param name="entities">Коллекция сущностей для добавления.</param>
    void AddRange(IEnumerable<T> entities);
    
    /// <summary>
    /// Удаляет коллекцию сущностей из репозитория.
    /// </summary>
    /// <param name="entities">Коллекция сущностей для удаления.</param>
    void RemoveRange(IEnumerable<T> entities);
    
    /// <summary>
    /// Асинхронно возвращает список сущностей, удовлетворяющих заданному условию.
    /// </summary>
    /// <param name="expression">Условие фильтрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей, удовлетворяющих условию.</returns>
    Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
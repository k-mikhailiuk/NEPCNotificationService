using System.Linq.Expressions;

namespace ControlPanel.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс базового репозитория для работы с сущностями.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Асинхронно получает сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность или null, если не найдена.</returns>
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Асинхронно возвращает все сущности.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция сущностей.</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Асинхронно проверяет наличие сущности, удовлетворяющей условию.
    /// </summary>
    /// <param name="predicate">Условие для проверки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>True, если сущность найдена, иначе false.</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Асинхронно добавляет новую сущность.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Удаляет указанную сущность.
    /// </summary>
    /// <param name="entity">Сущность для удаления.</param>
    void Remove(T entity);
    
    /// <summary>
    /// Асинхронно ищет первую сущность, удовлетворяющую условию.
    /// </summary>
    /// <param name="predicate">Условие поиска.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Найденная сущность или null, если не найдена.</returns>
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
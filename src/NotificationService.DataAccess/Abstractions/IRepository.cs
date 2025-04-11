using System.Linq.Expressions;

namespace NotificationService.DataAccess.Abstractions;

/// <summary>
/// Универсальный интерфейс репозитория для работы с сущностями.
/// Определяет метод получения данных по условию.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Асинхронно возвращает список сущностей, удовлетворяющих заданному условию.
    /// </summary>
    /// <param name="expression">Условие фильтрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей, удовлетворяющих условию.</returns>
    Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
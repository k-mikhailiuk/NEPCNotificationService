using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Services.Abstractions;

public partial interface IKeyWordBuilder<in T> where T : class, INotification
{
    string BuildKeyWordsAsync(string message, T entity);
}
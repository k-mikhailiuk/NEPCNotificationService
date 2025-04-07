using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Common.Enums;

namespace Aggregator.Core.Services.Abstractions;

public partial interface IKeyWordBuilder<in T> where T : class, INotification
{
    Task<string> BuildKeyWordsAsync(string? message, T entity, Language language);
}
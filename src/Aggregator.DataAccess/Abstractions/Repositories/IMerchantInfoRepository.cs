using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с MerchantInfo.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="MerchantInfo"/>.
/// </remarks>
public interface IMerchantInfoRepository : IRepository<MerchantInfo>
{
    
}
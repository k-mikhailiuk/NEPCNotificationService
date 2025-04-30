using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Entities;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с MerchantInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IMerchantInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="MerchantInfo"/>.
/// </remarks>
public class MerchantInfoRepository(AggregatorDbContext context)
    : Repository<MerchantInfo>(context), IMerchantInfoRepository;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

/// <summary>
/// Репозиторий для работы с MerchantInfo.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IMerchantInfoRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="MerchantInfo"/>.
/// </remarks>
public class MerchantInfoRepository(AggregatorDbContext context)
    : Repository<MerchantInfo>(context), IMerchantInfoRepository;
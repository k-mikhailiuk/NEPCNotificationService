using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CardInfoLimitWrapperRepository(AggregatorDbContext context)
    : Repository<CardInfoLimitWrapper>(context), ICardInfoLimitWrapperRepository;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class CardInfoRepository(AggregatorDbContext context) : Repository<CardInfo>(context), ICardInfoRepository;
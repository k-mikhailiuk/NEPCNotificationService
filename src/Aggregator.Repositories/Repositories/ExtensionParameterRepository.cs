using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class ExtensionParameterRepository(AggregatorDbContext context)
    : Repository<ExtensionParameter>(context), IExtensionParameterRepository;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class ExtensionParameterRepository : Repository<ExtensionParameter>, IExtensionParameterRepository
{
    public ExtensionParameterRepository(AggregatorDbContext context) : base(context)
    {
    }
}
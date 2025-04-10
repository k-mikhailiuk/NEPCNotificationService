using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.Repositories.Repositories.IssFinAuth;

public class IssFinAuthDetailsRepository(AggregatorDbContext context)
    : Repository<IssFinAuthDetails>(context), IIssFinAuthDetailsRepository;
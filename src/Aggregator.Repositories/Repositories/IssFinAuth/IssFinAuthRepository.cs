using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;

namespace Aggregator.Repositories.Repositories.IssFinAuth;

public class IssFinAuthRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.IssFinAuth.IssFinAuth>(context), IIssFinAuthRepository;
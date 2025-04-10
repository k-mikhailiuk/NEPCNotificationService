using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.Repositories.Repositories.AcqFinAuth;

public class AcqFinAuthDetailsRepository(AggregatorDbContext context)
    : Repository<AcqFinAuthDetails>(context), IAcqFinAuthDetailsRepository;
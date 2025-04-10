using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;

namespace Aggregator.Repositories.Repositories.AcqFinAuth;

public class AcqFinAuthRepository(AggregatorDbContext context)
    : Repository<DataAccess.Entities.AcqFinAuth.AcqFinAuth>(context), IAcqFinAuthRepository;
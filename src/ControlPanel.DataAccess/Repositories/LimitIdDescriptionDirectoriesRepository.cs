using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

public class LimitIdDescriptionDirectoriesRepository : Repository<LimitIdDescriptionDirectory>, ILimitIdDescriptionDirectoriesRepository
{
    public LimitIdDescriptionDirectoriesRepository(ControlPanelDbContext context) : base(context)
    {
    }
}
using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

public class LimitIdDescriptionDirectoriesRepository(ControlPanelDbContext context)
    : Repository<LimitIdDescriptionDirectory>(context), ILimitIdDescriptionDirectoriesRepository;
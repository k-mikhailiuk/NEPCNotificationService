using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с LimitIdDescriptionDirectory.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ILimitIdDescriptionDirectoriesRepository"/> и наследует базовый класс <see cref="Repository{LimitIdDescriptionDirectory}"/>.
/// </remarks>
public class LimitIdDescriptionDirectoriesRepository(ControlPanelDbContext context)
    : Repository<LimitIdDescriptionDirectory>(context), ILimitIdDescriptionDirectoriesRepository;
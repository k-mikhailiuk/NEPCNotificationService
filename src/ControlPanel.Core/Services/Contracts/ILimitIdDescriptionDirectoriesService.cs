using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

public interface ILimitIdDescriptionDirectoriesService
{
    Task<List<LimitIdDescriptionDirectory>> GetLimitIdDescriptionDirectoriesAsync(CancellationToken cancellationToken);
}
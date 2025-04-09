using ControlPanel.Core.DTOs.LimitIdDescription;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

public interface ILimitIdDescriptionDirectoriesService
{
    Task<List<LimitIdDescriptionDirectory>> GetLimitIdDescriptionDirectoriesAsync(CancellationToken cancellationToken);
    Task CreateLimitIdDescription(AddLimitIdDescriptionDto dto, CancellationToken cancellationToken);
    Task EditLimitIdDescription(EditLimitIdDescriptionDto dto, CancellationToken cancellationToken);
    Task DeleteLimitIdDescription(int limitId, CancellationToken cancellationToken);
}
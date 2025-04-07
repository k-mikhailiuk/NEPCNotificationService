using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

public class LimitIdDescriptionDirectoriesService(IUnitOfWork unitOfWork) : ILimitIdDescriptionDirectoriesService
{
    public async Task<List<LimitIdDescriptionDirectory>> GetLimitIdDescriptionDirectoriesAsync(
        CancellationToken cancellationToken)
    {
        var limitIdDirectories = await unitOfWork.LimitIdDescriptionDirectories.GetAllAsync(cancellationToken);

        return limitIdDirectories.ToList();
    }
}
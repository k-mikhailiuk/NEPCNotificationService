using ControlPanel.Core.DTOs.LimitIdDescription;
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

    public async Task CreateLimitIdDescription(AddLimitIdDescriptionDto dto, CancellationToken cancellationToken)
    {
        var limitIdDescription = LimitIdDescriptionDirectory.Create(dto.LimitCode, dto.Name, dto.DescriptionRu, dto.DescriptionKg, dto.DescriptionEn);
        
        await unitOfWork.LimitIdDescriptionDirectories.AddAsync(limitIdDescription, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task EditLimitIdDescription(EditLimitIdDescriptionDto dto, CancellationToken cancellationToken)
    {
        var limitIdDescription = await unitOfWork.LimitIdDescriptionDirectories.GetByIdAsync(dto.Id, cancellationToken);
        
        if (limitIdDescription == null)
            return;
        
        limitIdDescription.LimitCode = dto.LimitCode;
        limitIdDescription.Name = dto.Name;
        limitIdDescription.DescriptionRu = dto.DescriptionRu;
        limitIdDescription.DescriptionKg = dto.DescriptionKg;
        limitIdDescription.DescriptionEn = dto.DescriptionEn;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLimitIdDescription(int id, CancellationToken cancellationToken)
    {
        var limitIdDescription = await unitOfWork.LimitIdDescriptionDirectories.GetByIdAsync(id, cancellationToken);

        if (limitIdDescription != null) unitOfWork.LimitIdDescriptionDirectories.Remove(limitIdDescription);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
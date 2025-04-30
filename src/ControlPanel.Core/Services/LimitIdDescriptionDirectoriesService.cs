using ControlPanel.Core.DTOs.LimitIdDescription;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

/// <inheritdoc/>
public class LimitIdDescriptionDirectoriesService(IControlPanelUnitOfWork controlPanelUnitOfWork) : ILimitIdDescriptionDirectoriesService
{
    /// <inheritdoc/>
    public async Task<List<LimitIdDescriptionDirectory>> GetLimitIdDescriptionDirectoriesAsync(
        CancellationToken cancellationToken)
    {
        var limitIdDirectories = await controlPanelUnitOfWork.LimitIdDescriptionDirectories.GetAllAsync(cancellationToken);

        return limitIdDirectories.ToList();
    }

    /// <inheritdoc/>
    public async Task CreateLimitIdDescription(AddLimitIdDescriptionDto dto, CancellationToken cancellationToken)
    {
        var limitIdDescription = LimitIdDescriptionDirectory.Create(dto.LimitCode, dto.Name, dto.DescriptionRu, dto.DescriptionKg, dto.DescriptionEn);
        
        await controlPanelUnitOfWork.LimitIdDescriptionDirectories.AddAsync(limitIdDescription, cancellationToken);
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task EditLimitIdDescription(EditLimitIdDescriptionDto dto, CancellationToken cancellationToken)
    {
        var limitIdDescription = await controlPanelUnitOfWork.LimitIdDescriptionDirectories.GetByIdAsync(dto.Id, cancellationToken);
        
        if (limitIdDescription == null)
            return;
        
        limitIdDescription.LimitCode = dto.LimitCode;
        limitIdDescription.Name = dto.Name;
        limitIdDescription.DescriptionRu = dto.DescriptionRu;
        limitIdDescription.DescriptionKg = dto.DescriptionKg;
        limitIdDescription.DescriptionEn = dto.DescriptionEn;
        
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteLimitIdDescription(int id, CancellationToken cancellationToken)
    {
        var limitIdDescription = await controlPanelUnitOfWork.LimitIdDescriptionDirectories.GetByIdAsync(id, cancellationToken);

        if (limitIdDescription != null) controlPanelUnitOfWork.LimitIdDescriptionDirectories.Remove(limitIdDescription);
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
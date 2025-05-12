using Aggregator.DataAccess.Entities;
using Aggregator.DTOs;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers;

/// <summary>
/// Класс для маппинга данных MerchantInfo из DTO в сущность.
/// </summary>
public class MerchantInfoMapper(ILogger<MerchantInfoMapper> logger)
{
    /// <summary>
    /// Преобразует объект <see cref="AggregatorMerchantInfoDto"/> в экземпляр <see cref="MerchantInfo"/>.
    /// </summary>
    /// <param name="dto">Объект DTO для MerchantInfo.</param>
    /// <returns>
    /// Экземпляр <see cref="MerchantInfo"/> с заполненными полями из <paramref name="dto"/>.
    /// Если <paramref name="dto"/> равен null, возвращается null.
    /// </returns>
    public MerchantInfo? MapMerchantInfo(AggregatorMerchantInfoDto? dto)
    {
        if (dto == null)
        {
            logger.LogInformation("MerchantInfo is null");
            return null;
        }

        logger.LogInformation("Mapping MerchantInfo, merchantInfoId: {dto.Id}", dto.Id);

        return new MerchantInfo
        {
            MerchantId = dto.Id,
            Mcc = dto.MCC,
            TerminalId = dto.TerminalId,
            Aid = dto.Aid,
            Name = dto.Name,
            Street = dto.Street,
            City = dto.City,
            Country = dto.Country,
            ZipCode = dto.ZipCode
        };
    }
}
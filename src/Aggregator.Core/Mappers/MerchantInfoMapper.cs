using Aggregator.DataAccess.Entities;
using Aggregator.DTOs;

namespace Aggregator.Core.Mappers;

public static class MerchantInfoMapper
{
    public static MerchantInfo MapMerchantInfo(AggregatorMerchantInfoDto dto)
    {
        if (dto == null)
        {
            Console.WriteLine("MerchantInfo is null");
            return null;
        }

        Console.WriteLine($"Mapping MerchantInfo, merchantInfoId: {dto.Id}");

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
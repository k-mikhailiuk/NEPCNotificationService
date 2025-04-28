using Aggregator.Core.Services;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwnedEntities;
using Aggregator.DTOs;
using Microsoft.Extensions.Logging;

namespace Aggregator.Core.Mappers;

public class AccountsInfoMapper(
    ILogger<AccountsInfoMapper> logger,
    DateTimeConverter dateTimeConverter,
    ConversionExtensionsHelper conversionExtensionsHelper)
{
    /// <summary>
    /// Преобразует список DTO информации о счетах в список сущностей <see cref="AccountsInfo"/>.
    /// </summary>
    /// <param name="dto">Список DTO объектов информации о счетах.</param>
    /// <param name="notificationId">Идентификатор уведомления.</param>
    /// <returns>Список сущностей <see cref="AccountsInfo"/> или null, если список dto пуст или равен null.</returns>
    public List<AccountsInfo> MapAccountsInfo(List<AggregatorAccountInfoDto> dto, long notificationId)
    {
        if (dto == null || dto.Count == 0)
        {
            logger.LogInformation("AccountsInfo is null");
            return null;
        }

        logger.LogInformation("Mapping AccountsInfo");

        return dto.Select(x => new AccountsInfo()
        {
            AccountsInfoId = x.Id,
            NotificationId = notificationId,
            NotificationType = NotificationType.AcctBalChange,
            Type = x.Type,
            AviableBalance = conversionExtensionsHelper.ConvertMoneyDtoToEntity<AviableBalance>(x.AvailableBalance),
            ExceedLimit = x.ExceedLimit != null
                ? conversionExtensionsHelper.ConvertMoneyDtoToEntity<ExceedLimitMoney>(x.ExceedLimit)
                : new ExceedLimitMoney { Amount = null, Currency = null },
            Limits = x.Limits?.Select(l => new AccountsInfoLimitWrapper
            {
                LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                Limit = new Limit
                {
                    LimitId = l.AmtLimit?.Id ?? l.CntLimit?.Id ?? 0,
                    LimitType = l.AmtLimit != null ? LimitType.AmtLimit : LimitType.CntLimit,
                    Currency = l.AmtLimit?.Currency ?? null,
                    CycleLength = l.AmtLimit != null ? l.AmtLimit.CycleLength : 0,
                    CycleType = l.AmtLimit?.CycleType,
                    EndTime = l.AmtLimit != null
                        ? dateTimeConverter.SafeConvertTime(l.AmtLimit.EndTime)
                        : dateTimeConverter.SafeConvertTime(l.CntLimit.EndTime),
                    TrsValue = l.AmtLimit != null ? decimal.Round(l.AmtLimit.TrsAmount, 2) / 100 : l.CntLimit.TrsValue,
                    UsedValue = l.AmtLimit != null
                        ? decimal.Round(l.AmtLimit.UsedAmount, 2) / 100
                        : l.CntLimit.UsedValue
                }
            }).ToList(),
        }).ToList();
    }
}
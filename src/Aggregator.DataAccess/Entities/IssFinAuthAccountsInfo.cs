using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities;

/// <summary>
/// Информация об аккаунте для уведомления IssFinAuth
/// </summary>
public class IssFinAuthAccountsInfo : AccountsInfo
{
    /// <summary>
    /// Уникальный идентификатор IssFinAuth
    /// </summary>
    public long IssFinAuthId { get; set; }
}
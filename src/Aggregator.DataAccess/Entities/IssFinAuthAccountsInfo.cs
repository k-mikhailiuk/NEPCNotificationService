using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities;

public class IssFinAuthAccountsInfo : AccountsInfo
{
    [Key]
    public long IssFinAuthAccountsInfoId { get; set; }
}
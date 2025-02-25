using System.ComponentModel.DataAnnotations;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Entities;

public class AcctBalChangeAccountsInfo : AccountsInfo
{
    [Key]
    public long AcctBalChangeAccountsInfoId { get; set; }
}
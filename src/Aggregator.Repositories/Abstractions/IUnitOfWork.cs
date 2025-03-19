using Aggregator.Repositories.Abstractions.Repositories;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;

namespace Aggregator.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IInboxRepository Inbox { get; }
    IAcctBalChangeDetailsRepository AcctBalChangeDetails { get; }
    IAcctBalChangeRepository AcctBalChange { get; }
    IAcqFinAuthDetailsRepository AcqFinAuthDetails { get; }
    IAcqFinAuthRepository AcqFinAuth { get; }
    ICardStatusChangeDetailsRepository CardStatusChangeDetails { get; }
    ICardStatusChangeRepository CardStatusChange { get; }
    IIssFinAuthDetailsRepository IssFinAuthDetails { get; }
    IIssFinAuthRepository IssFinAuth { get; }
    IOwiUserActionDetailsRepository OwiUserActionDetails { get; }
    IOwiUserActionRepository OwiUserAction { get; }
    IPinChangeDetailsRepository PinChangeDetails { get; }
    IPinChangeRepository PinChange { get; }
    ITokenStatusChangeDetailsRepository TokenStatusChangeDetails { get; }
    ITokenStatusChangeRepository TokenStatusChange { get; }
    IUnholdDetailsRepository UnholdDetails { get; }
    IUnholdRepository Unhold { get; }
    IAccountsInfoLimitWrapperRepository AccountsInfoLimitWrapper { get; }
    IAcctBalChangeAccountsInfoRepository AcctBalChangeAccountsInfo { get; }
    ICardInfoLimitWrapperRepository CardInfoLimitWrapper { get; }
    ICardInfoRepository CardInfo { get; }
    ICheckedLimitRepository CheckedLimit { get; }
    IExtensionParameterRepository ExtensionParameter { get; }
    IFinTransactionRepository FinTransaction { get; }
    IIssFinAuthAccountsInfoRepository IssFinAuthAccountsInfo { get; }
    ILimitRepository Limit { get; }
    IMerchantInfoRepository MerchantInfo { get; }
    INotificationExtensionRepository NotificationExtension { get; }
    IInboxArchiveMessageRepository InboxArchiveMessage { get; }
    INotificationMessageRepository NotificationMessage { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class;
    
    void BeginTransactionAsync();
    
    void CommitTransactionAsync();
    
    void RollbackTransactionAsync();
}
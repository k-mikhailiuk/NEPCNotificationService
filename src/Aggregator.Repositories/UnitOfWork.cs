using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions;
using Aggregator.Repositories.Abstractions.Repositories;
using Aggregator.Repositories.Abstractions.Repositories.AcctBalChange;
using Aggregator.Repositories.Abstractions.Repositories.AcqFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.CardStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.IssFinAuth;
using Aggregator.Repositories.Abstractions.Repositories.OwiUserAction;
using Aggregator.Repositories.Abstractions.Repositories.PinChange;
using Aggregator.Repositories.Abstractions.Repositories.TokenStatusChange;
using Aggregator.Repositories.Abstractions.Repositories.Unhold;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AggregatorDbContext _context;

    public IInboxRepository Inbox => _inbox.Value;
    private readonly Lazy<IInboxRepository> _inbox;
    
    public IAcctBalChangeDetailsRepository AcctBalChangeDetails => _acctBalChangeDetails.Value;
    private readonly Lazy<IAcctBalChangeDetailsRepository> _acctBalChangeDetails;
    
    public IAcctBalChangeRepository AcctBalChange => _acctBalChange.Value;
    private readonly Lazy<IAcctBalChangeRepository> _acctBalChange;
    
    public IAcqFinAuthDetailsRepository AcqFinAuthDetails => _acqFinAuthDetails.Value;
    private readonly Lazy<IAcqFinAuthDetailsRepository> _acqFinAuthDetails;
    
    public IAcqFinAuthRepository AcqFinAuth => _acqFinAuth.Value;
    private readonly Lazy<IAcqFinAuthRepository> _acqFinAuth;
    
    public ICardStatusChangeDetailsRepository CardStatusChangeDetails => _cardStatusChangeDetails.Value;
    private readonly Lazy<ICardStatusChangeDetailsRepository> _cardStatusChangeDetails;
    
    public ICardStatusChangeRepository CardStatusChange => _cardStatusChange.Value;
    private readonly Lazy<ICardStatusChangeRepository> _cardStatusChange;
    
    public IIssFinAuthDetailsRepository IssFinAuthDetails => _issFinAuthDetails.Value;
    private readonly Lazy<IIssFinAuthDetailsRepository> _issFinAuthDetails;
    
    public IIssFinAuthRepository IssFinAuth => _issFinAuth.Value;
    private readonly Lazy<IIssFinAuthRepository> _issFinAuth;
    
    public IOwiUserActionDetailsRepository OwiUserActionDetails => _owiUserActionDetails.Value;
    private readonly Lazy<IOwiUserActionDetailsRepository> _owiUserActionDetails;
    
    public IOwiUserActionRepository OwiUserAction => _owiUserAction.Value;
    private readonly Lazy<IOwiUserActionRepository> _owiUserAction;
    
    public IPinChangeDetailsRepository PinChangeDetails => _pinChangeDetails.Value;
    private readonly Lazy<IPinChangeDetailsRepository> _pinChangeDetails;
    
    public IPinChangeRepository PinChange => _pinChange.Value;
    private readonly Lazy<IPinChangeRepository> _pinChange;
    
    public ITokenStatusChangeDetailsRepository TokenStatusChangeDetails => _tokenStatusChangeDetails.Value;
    private readonly Lazy<ITokenStatusChangeDetailsRepository> _tokenStatusChangeDetails;
    
    public ITokenStatusChangeRepository TokenStatusChange => _tokenStatusChange.Value;
    private readonly Lazy<ITokenStatusChangeRepository> _tokenStatusChange;
    
    public IUnholdDetailsRepository UnholdDetails => _unholdDetails.Value;
    private readonly Lazy<IUnholdDetailsRepository> _unholdDetails;
    
    public IUnholdRepository Unhold => _unhold.Value;
    private readonly Lazy<IUnholdRepository> _unhold;
    
    public IAccountsInfoLimitWrapperRepository AccountsInfoLimitWrapper => _accountsInfoLimitWrapper.Value;
    private readonly Lazy<IAccountsInfoLimitWrapperRepository> _accountsInfoLimitWrapper;
    
    public IAcctBalChangeAccountsInfoRepository AcctBalChangeAccountsInfo => _acctBalChangeAccountsInfo.Value;
    private readonly Lazy<IAcctBalChangeAccountsInfoRepository> _acctBalChangeAccountsInfo;
    
    public ICardInfoLimitWrapperRepository CardInfoLimitWrapper => _cardInfoLimitWrapper.Value;
    private readonly Lazy<ICardInfoLimitWrapperRepository> _cardInfoLimitWrapper;
    
    public ICardInfoRepository CardInfo => _cardInfo.Value;
    private readonly Lazy<ICardInfoRepository> _cardInfo;
    
    public ICheckedLimitRepository CheckedLimit => _checkedLimit.Value;
    private readonly Lazy<ICheckedLimitRepository> _checkedLimit;
    
    public IExtensionParameterRepository ExtensionParameter => _extensionParameter.Value;
    private readonly Lazy<IExtensionParameterRepository> _extensionParameter;
    
    public IFinTransactionRepository FinTransaction => _finTransaction.Value;
    private readonly Lazy<IFinTransactionRepository> _finTransaction;
    
    public IIssFinAuthAccountsInfoRepository IssFinAuthAccountsInfo => _issFinAuthAccountsInfo.Value;
    private readonly Lazy<IIssFinAuthAccountsInfoRepository> _issFinAuthAccountsInfo;
    
    public ILimitRepository Limit => _limit.Value;
    private readonly Lazy<ILimitRepository> _limit;
    
    public IMerchantInfoRepository MerchantInfo => _merchantInfo.Value;
    private readonly Lazy<IMerchantInfoRepository> _merchantInfo;
    
    public INotificationExtensionRepository NotificationExtension => _notificationExtension.Value;
    private readonly Lazy<INotificationExtensionRepository> _notificationExtension;
    
    public IInboxArchiveMessageRepository InboxArchiveMessage  => _inboxArchiveMessage.Value;
    private readonly Lazy<IInboxArchiveMessageRepository> _inboxArchiveMessage;
    
    public UnitOfWork(AggregatorDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        
        _inbox = new Lazy<IInboxRepository>(() => serviceProvider.GetService<IInboxRepository>() ?? throw new InvalidOperationException());
        _acctBalChangeDetails = new Lazy<IAcctBalChangeDetailsRepository>(() => serviceProvider.GetService<IAcctBalChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _acctBalChange = new Lazy<IAcctBalChangeRepository>(() => serviceProvider.GetService<IAcctBalChangeRepository>() ?? throw new InvalidOperationException());
        _acqFinAuthDetails = new Lazy<IAcqFinAuthDetailsRepository>(() => serviceProvider.GetService<IAcqFinAuthDetailsRepository>() ?? throw new InvalidOperationException());
        _acqFinAuth = new Lazy<IAcqFinAuthRepository>(() => serviceProvider.GetService<IAcqFinAuthRepository>() ?? throw new InvalidOperationException());
        _cardStatusChangeDetails = new Lazy<ICardStatusChangeDetailsRepository>(() => serviceProvider.GetService<ICardStatusChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _cardStatusChange = new Lazy<ICardStatusChangeRepository>(() => serviceProvider.GetService<ICardStatusChangeRepository>() ?? throw new InvalidOperationException());
        _issFinAuthDetails = new Lazy<IIssFinAuthDetailsRepository>(() => serviceProvider.GetService<IIssFinAuthDetailsRepository>() ?? throw new InvalidOperationException());
        _issFinAuth = new Lazy<IIssFinAuthRepository>(() => serviceProvider.GetService<IIssFinAuthRepository>() ?? throw new InvalidOperationException());
        _owiUserActionDetails = new Lazy<IOwiUserActionDetailsRepository>(() => serviceProvider.GetService<IOwiUserActionDetailsRepository>() ?? throw new InvalidOperationException());
        _owiUserAction = new Lazy<IOwiUserActionRepository>(() => serviceProvider.GetService<IOwiUserActionRepository>() ?? throw new InvalidOperationException());
        _pinChangeDetails = new Lazy<IPinChangeDetailsRepository>(() => serviceProvider.GetService<IPinChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _pinChange = new Lazy<IPinChangeRepository>(() => serviceProvider.GetService<IPinChangeRepository>() ?? throw new InvalidOperationException());
        _tokenStatusChangeDetails = new Lazy<ITokenStatusChangeDetailsRepository>(() => serviceProvider.GetService<ITokenStatusChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _tokenStatusChange = new Lazy<ITokenStatusChangeRepository>(() => serviceProvider.GetService<ITokenStatusChangeRepository>() ?? throw new InvalidOperationException());
        _acctBalChangeAccountsInfo = new Lazy<IAcctBalChangeAccountsInfoRepository>(() => serviceProvider.GetService<IAcctBalChangeAccountsInfoRepository>() ?? throw new InvalidOperationException());
        _unholdDetails = new Lazy<IUnholdDetailsRepository>(() => serviceProvider.GetService<IUnholdDetailsRepository>() ?? throw new InvalidOperationException());
        _unhold = new Lazy<IUnholdRepository>(() => serviceProvider.GetService<IUnholdRepository>() ?? throw new InvalidOperationException());
        _accountsInfoLimitWrapper = new Lazy<IAccountsInfoLimitWrapperRepository>(() => serviceProvider.GetService<IAccountsInfoLimitWrapperRepository>() ?? throw new InvalidOperationException());
        _cardInfoLimitWrapper = new Lazy<ICardInfoLimitWrapperRepository>(() => serviceProvider.GetService<ICardInfoLimitWrapperRepository>() ?? throw new InvalidOperationException());
        _cardInfo = new Lazy<ICardInfoRepository>(() => serviceProvider.GetService<ICardInfoRepository>() ?? throw new InvalidOperationException());
        _checkedLimit = new Lazy<ICheckedLimitRepository>(() => serviceProvider.GetService<ICheckedLimitRepository>() ?? throw new InvalidOperationException());
        _extensionParameter = new Lazy<IExtensionParameterRepository>(() => serviceProvider.GetService<IExtensionParameterRepository>() ?? throw new InvalidOperationException());
        _finTransaction = new Lazy<IFinTransactionRepository>(() => serviceProvider.GetService<IFinTransactionRepository>() ?? throw new InvalidOperationException());
        _issFinAuthAccountsInfo = new Lazy<IIssFinAuthAccountsInfoRepository>(() => serviceProvider.GetService<IIssFinAuthAccountsInfoRepository>() ?? throw new InvalidOperationException());
        _limit = new Lazy<ILimitRepository>(() => serviceProvider.GetService<ILimitRepository>() ?? throw new InvalidOperationException());
        _merchantInfo = new Lazy<IMerchantInfoRepository>(() => serviceProvider.GetService<IMerchantInfoRepository>() ?? throw new InvalidOperationException());
        _notificationExtension = new Lazy<INotificationExtensionRepository>(() => serviceProvider.GetService<INotificationExtensionRepository>() ?? throw new InvalidOperationException());
        _inboxArchiveMessage = new Lazy<IInboxArchiveMessageRepository>(() => serviceProvider.GetService<IInboxArchiveMessageRepository>() ?? throw new InvalidOperationException());
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
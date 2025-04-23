using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Aggregator.DataAccess.Abstractions.Repositories.AcctBalChange;
using Aggregator.DataAccess.Abstractions.Repositories.AcqFinAuth;
using Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;
using Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;
using Aggregator.DataAccess.Abstractions.Repositories.IssFinAuth;
using Aggregator.DataAccess.Abstractions.Repositories.OwiUserAction;
using Aggregator.DataAccess.Abstractions.Repositories.PinChange;
using Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;
using Aggregator.DataAccess.Abstractions.Repositories.Unhold;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.DataAccess;

/// <summary>
/// Реализация единицы работы (Unit of Work) для агрегирования доступа к репозиториям.
/// </summary>
/// <remarks>
/// Предоставляет свойства для доступа к различным репозиториям, а также методы для сохранения изменений, выполнения сырого SQL-запроса и управления транзакциями.
/// </remarks>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Контекст базы данных Aggregator.
    /// </summary>
    protected AggregatorDbContext _context { get; }

    /// <inheritdoc/>
    public IInboxRepository Inbox => _inbox.Value;

    private readonly Lazy<IInboxRepository> _inbox;

    /// <inheritdoc/>
    public INotificationsRepository Notifications => _notifications.Value;

    private readonly Lazy<INotificationsRepository> _notifications;

    /// <inheritdoc/>
    public IAcctBalChangeDetailsRepository AcctBalChangeDetails => _acctBalChangeDetails.Value;

    private readonly Lazy<IAcctBalChangeDetailsRepository> _acctBalChangeDetails;

    /// <inheritdoc/>
    public IAcctBalChangeRepository AcctBalChange => _acctBalChange.Value;

    private readonly Lazy<IAcctBalChangeRepository> _acctBalChange;

    /// <inheritdoc/>
    public IAcqFinAuthDetailsRepository AcqFinAuthDetails => _acqFinAuthDetails.Value;

    private readonly Lazy<IAcqFinAuthDetailsRepository> _acqFinAuthDetails;

    /// <inheritdoc/>
    public IAcqFinAuthRepository AcqFinAuth => _acqFinAuth.Value;

    private readonly Lazy<IAcqFinAuthRepository> _acqFinAuth;

    /// <inheritdoc/>
    public ICardStatusChangeDetailsRepository CardStatusChangeDetails => _cardStatusChangeDetails.Value;

    private readonly Lazy<ICardStatusChangeDetailsRepository> _cardStatusChangeDetails;

    /// <inheritdoc/>
    public ICardStatusChangeRepository CardStatusChange => _cardStatusChange.Value;

    private readonly Lazy<ICardStatusChangeRepository> _cardStatusChange;

    /// <inheritdoc/>
    public IIssFinAuthDetailsRepository IssFinAuthDetails => _issFinAuthDetails.Value;

    private readonly Lazy<IIssFinAuthDetailsRepository> _issFinAuthDetails;

    /// <inheritdoc/>
    public IIssFinAuthRepository IssFinAuth => _issFinAuth.Value;

    private readonly Lazy<IIssFinAuthRepository> _issFinAuth;

    /// <inheritdoc/>
    public IOwiUserActionDetailsRepository OwiUserActionDetails => _owiUserActionDetails.Value;

    private readonly Lazy<IOwiUserActionDetailsRepository> _owiUserActionDetails;

    /// <inheritdoc/>
    public IOwiUserActionRepository OwiUserAction => _owiUserAction.Value;

    private readonly Lazy<IOwiUserActionRepository> _owiUserAction;

    /// <inheritdoc/>
    public IPinChangeDetailsRepository PinChangeDetails => _pinChangeDetails.Value;

    private readonly Lazy<IPinChangeDetailsRepository> _pinChangeDetails;

    /// <inheritdoc/>
    public IPinChangeRepository PinChange => _pinChange.Value;

    private readonly Lazy<IPinChangeRepository> _pinChange;

    /// <inheritdoc/>
    public ITokenStatusChangeDetailsRepository TokenStatusChangeDetails => _tokenStatusChangeDetails.Value;

    private readonly Lazy<ITokenStatusChangeDetailsRepository> _tokenStatusChangeDetails;

    /// <inheritdoc/>
    public ITokenStatusChangeRepository TokenStatusChange => _tokenStatusChange.Value;

    private readonly Lazy<ITokenStatusChangeRepository> _tokenStatusChange;

    /// <inheritdoc/>
    public IUnholdDetailsRepository UnholdDetails => _unholdDetails.Value;

    private readonly Lazy<IUnholdDetailsRepository> _unholdDetails;

    /// <inheritdoc/>
    public IUnholdRepository Unhold => _unhold.Value;

    private readonly Lazy<IUnholdRepository> _unhold;

    /// <inheritdoc/>
    public IAccountsInfoLimitWrapperRepository AccountsInfoLimitWrapper => _accountsInfoLimitWrapper.Value;

    private readonly Lazy<IAccountsInfoLimitWrapperRepository> _accountsInfoLimitWrapper;

    /// <inheritdoc/>
    public ICardInfoLimitWrapperRepository CardInfoLimitWrapper => _cardInfoLimitWrapper.Value;

    private readonly Lazy<ICardInfoLimitWrapperRepository> _cardInfoLimitWrapper;

    /// <inheritdoc/>
    public ICardInfoRepository CardInfo => _cardInfo.Value;

    private readonly Lazy<ICardInfoRepository> _cardInfo;

    /// <inheritdoc/>
    public ICheckedLimitRepository CheckedLimit => _checkedLimit.Value;

    private readonly Lazy<ICheckedLimitRepository> _checkedLimit;

    /// <inheritdoc/>
    public IExtensionParameterRepository ExtensionParameter => _extensionParameter.Value;

    private readonly Lazy<IExtensionParameterRepository> _extensionParameter;

    /// <inheritdoc/>
    public IFinTransactionRepository FinTransaction => _finTransaction.Value;

    private readonly Lazy<IFinTransactionRepository> _finTransaction;

    /// <inheritdoc/>
    public IAccountsInfoRepository AccountsInfos => _accountsInfo.Value;

    private readonly Lazy<IAccountsInfoRepository> _accountsInfo;

    /// <inheritdoc/>
    public ILimitRepository Limit => _limit.Value;

    private readonly Lazy<ILimitRepository> _limit;

    /// <inheritdoc/>
    public IMerchantInfoRepository MerchantInfo => _merchantInfo.Value;

    private readonly Lazy<IMerchantInfoRepository> _merchantInfo;

    /// <inheritdoc/>
    public INotificationExtensionRepository NotificationExtension => _notificationExtension.Value;

    private readonly Lazy<INotificationExtensionRepository> _notificationExtension;

    /// <inheritdoc/>
    public IInboxArchiveMessageRepository InboxArchiveMessage => _inboxArchiveMessage.Value;

    private readonly Lazy<IInboxArchiveMessageRepository> _inboxArchiveMessage;

    /// <inheritdoc/>
    public INotificationMessageRepository NotificationMessage => _notificationMessage.Value;

    private readonly Lazy<INotificationMessageRepository> _notificationMessage;

    /// <inheritdoc/>
    public INotificationMessageKeyWordsRepository NotificationMessageKeyWords => _notificationMessageKeyWords.Value;

    private readonly Lazy<INotificationMessageKeyWordsRepository> _notificationMessageKeyWords;

    /// <inheritdoc/>
    public INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories =>
        _notificationMessageTextDirectories.Value;

    private readonly Lazy<INotificationMessageTextDirectoriesRepository> _notificationMessageTextDirectories;

    /// <inheritdoc/>
    public ICurrenciesRepository Currencies => _currencies.Value;

    private readonly Lazy<ICurrenciesRepository> _currencies;

    /// <inheritdoc/>
    public IAcsOtpRepository AcsOtps => _acsOtps.Value;

    private readonly Lazy<IAcsOtpRepository> _acsOtps;

    /// <inheritdoc/>
    public ILimitIdDescriptionDirectoriesRepository LimitIdDescriptionDirectories =>
        _limitIdDescriptionDirectories.Value;

    private readonly Lazy<ILimitIdDescriptionDirectoriesRepository> _limitIdDescriptionDirectories;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfWork"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных Aggregator.</param>
    /// <param name="serviceProvider">Провайдер сервисов для разрешения зависимостей репозиториев.</param>
    public UnitOfWork(AggregatorDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;

        _inbox = new Lazy<IInboxRepository>(() =>
            serviceProvider.GetService<IInboxRepository>() ?? throw new InvalidOperationException());
        _acctBalChangeDetails = new Lazy<IAcctBalChangeDetailsRepository>(() =>
            serviceProvider.GetService<IAcctBalChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _acctBalChange = new Lazy<IAcctBalChangeRepository>(() =>
            serviceProvider.GetService<IAcctBalChangeRepository>() ?? throw new InvalidOperationException());
        _acqFinAuthDetails = new Lazy<IAcqFinAuthDetailsRepository>(() =>
            serviceProvider.GetService<IAcqFinAuthDetailsRepository>() ?? throw new InvalidOperationException());
        _acqFinAuth = new Lazy<IAcqFinAuthRepository>(() =>
            serviceProvider.GetService<IAcqFinAuthRepository>() ?? throw new InvalidOperationException());
        _cardStatusChangeDetails = new Lazy<ICardStatusChangeDetailsRepository>(() =>
            serviceProvider.GetService<ICardStatusChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _cardStatusChange = new Lazy<ICardStatusChangeRepository>(() =>
            serviceProvider.GetService<ICardStatusChangeRepository>() ?? throw new InvalidOperationException());
        _issFinAuthDetails = new Lazy<IIssFinAuthDetailsRepository>(() =>
            serviceProvider.GetService<IIssFinAuthDetailsRepository>() ?? throw new InvalidOperationException());
        _issFinAuth = new Lazy<IIssFinAuthRepository>(() =>
            serviceProvider.GetService<IIssFinAuthRepository>() ?? throw new InvalidOperationException());
        _owiUserActionDetails = new Lazy<IOwiUserActionDetailsRepository>(() =>
            serviceProvider.GetService<IOwiUserActionDetailsRepository>() ?? throw new InvalidOperationException());
        _owiUserAction = new Lazy<IOwiUserActionRepository>(() =>
            serviceProvider.GetService<IOwiUserActionRepository>() ?? throw new InvalidOperationException());
        _pinChangeDetails = new Lazy<IPinChangeDetailsRepository>(() =>
            serviceProvider.GetService<IPinChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _pinChange = new Lazy<IPinChangeRepository>(() =>
            serviceProvider.GetService<IPinChangeRepository>() ?? throw new InvalidOperationException());
        _tokenStatusChangeDetails = new Lazy<ITokenStatusChangeDetailsRepository>(() =>
            serviceProvider.GetService<ITokenStatusChangeDetailsRepository>() ?? throw new InvalidOperationException());
        _tokenStatusChange = new Lazy<ITokenStatusChangeRepository>(() =>
            serviceProvider.GetService<ITokenStatusChangeRepository>() ?? throw new InvalidOperationException());
        _unholdDetails = new Lazy<IUnholdDetailsRepository>(() =>
            serviceProvider.GetService<IUnholdDetailsRepository>() ?? throw new InvalidOperationException());
        _unhold = new Lazy<IUnholdRepository>(() =>
            serviceProvider.GetService<IUnholdRepository>() ?? throw new InvalidOperationException());
        _accountsInfoLimitWrapper = new Lazy<IAccountsInfoLimitWrapperRepository>(() =>
            serviceProvider.GetService<IAccountsInfoLimitWrapperRepository>() ?? throw new InvalidOperationException());
        _cardInfoLimitWrapper = new Lazy<ICardInfoLimitWrapperRepository>(() =>
            serviceProvider.GetService<ICardInfoLimitWrapperRepository>() ?? throw new InvalidOperationException());
        _cardInfo = new Lazy<ICardInfoRepository>(() =>
            serviceProvider.GetService<ICardInfoRepository>() ?? throw new InvalidOperationException());
        _checkedLimit = new Lazy<ICheckedLimitRepository>(() =>
            serviceProvider.GetService<ICheckedLimitRepository>() ?? throw new InvalidOperationException());
        _extensionParameter = new Lazy<IExtensionParameterRepository>(() =>
            serviceProvider.GetService<IExtensionParameterRepository>() ?? throw new InvalidOperationException());
        _finTransaction = new Lazy<IFinTransactionRepository>(() =>
            serviceProvider.GetService<IFinTransactionRepository>() ?? throw new InvalidOperationException());
        _accountsInfo = new Lazy<IAccountsInfoRepository>(() =>
            serviceProvider.GetService<IAccountsInfoRepository>() ?? throw new InvalidOperationException());
        _limit = new Lazy<ILimitRepository>(() =>
            serviceProvider.GetService<ILimitRepository>() ?? throw new InvalidOperationException());
        _merchantInfo = new Lazy<IMerchantInfoRepository>(() =>
            serviceProvider.GetService<IMerchantInfoRepository>() ?? throw new InvalidOperationException());
        _notificationExtension = new Lazy<INotificationExtensionRepository>(() =>
            serviceProvider.GetService<INotificationExtensionRepository>() ?? throw new InvalidOperationException());
        _inboxArchiveMessage = new Lazy<IInboxArchiveMessageRepository>(() =>
            serviceProvider.GetService<IInboxArchiveMessageRepository>() ?? throw new InvalidOperationException());
        _notificationMessage = new Lazy<INotificationMessageRepository>(() =>
            serviceProvider.GetService<INotificationMessageRepository>() ?? throw new InvalidOperationException());

        _notificationMessageKeyWords = new Lazy<INotificationMessageKeyWordsRepository>(() =>
            serviceProvider.GetService<INotificationMessageKeyWordsRepository>() ??
            throw new InvalidOperationException());
        _notificationMessageTextDirectories = new Lazy<INotificationMessageTextDirectoriesRepository>(() =>
            serviceProvider.GetService<INotificationMessageTextDirectoriesRepository>() ??
            throw new InvalidOperationException());
        _currencies = new Lazy<ICurrenciesRepository>(() =>
            serviceProvider.GetService<ICurrenciesRepository>() ?? throw new InvalidOperationException());
        _limitIdDescriptionDirectories = new Lazy<ILimitIdDescriptionDirectoriesRepository>(() =>
            serviceProvider.GetService<ILimitIdDescriptionDirectoriesRepository>() ??
            throw new InvalidOperationException());

        _acsOtps = new Lazy<IAcsOtpRepository>(() =>
            serviceProvider.GetService<IAcsOtpRepository>() ?? throw new InvalidOperationException());
        _notifications = new Lazy<INotificationsRepository>(() =>
            serviceProvider.GetService<INotificationsRepository>() ?? throw new InvalidOperationException());
    }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);

    /// <inheritdoc/>
    public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class
    {
        return _context.Set<T>().FromSqlRaw(sql, parameters);
    }

    /// <inheritdoc/>
    public void BeginTransactionAsync()
    {
        _context.Database.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public void CommitTransactionAsync()
    {
        _context.Database.CommitTransactionAsync();
    }

    /// <inheritdoc/>
    public void RollbackTransactionAsync()
    {
        _context.Database.RollbackTransactionAsync();
    }

    /// <inheritdoc/>
    public void Attach<TEntity>(TEntity entity) where TEntity : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            _context.Attach(entity);
        }
    }

    /// <summary>
    /// Освобождает ресурсы, используемые контекстом базы данных.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}
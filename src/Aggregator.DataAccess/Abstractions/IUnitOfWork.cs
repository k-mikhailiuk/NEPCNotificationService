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

namespace Aggregator.DataAccess.Abstractions;

/// <summary>
/// Интерфейс единицы работы (Unit of Work) для агрегирования репозиториев.
/// </summary>
/// <remarks>
/// Обеспечивает доступ к различным репозиториям, а также методы для сохранения изменений и управления транзакциями.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Получает репозиторий для работы с входящими сообщениями.
    /// </summary>
    IInboxRepository Inbox { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с base уведомлениями.
    /// </summary>
    INotificationsRepository Notifications { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями изменения баланса счета.
    /// </summary>
    IAcctBalChangeDetailsRepository AcctBalChangeDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями изменения баланса счета.
    /// </summary>
    IAcctBalChangeRepository AcctBalChange { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями авторизации AcqFinAuth.
    /// </summary>
    IAcqFinAuthDetailsRepository AcqFinAuthDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями авторизации AcqFinAuth.
    /// </summary>
    IAcqFinAuthRepository AcqFinAuth { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями изменения статуса карты.
    /// </summary>
    ICardStatusChangeDetailsRepository CardStatusChangeDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями изменения статуса карты.
    /// </summary>
    ICardStatusChangeRepository CardStatusChange { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями авторизации IssFinAuth.
    /// </summary>
    IIssFinAuthDetailsRepository IssFinAuthDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями авторизации IssFinAuth.
    /// </summary>
    IIssFinAuthRepository IssFinAuth { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями действий пользователя (OwiUserAction).
    /// </summary>
    IOwiUserActionDetailsRepository OwiUserActionDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями действий пользователя (OwiUserAction).
    /// </summary>
    IOwiUserActionRepository OwiUserAction { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями изменения PIN-кода.
    /// </summary>
    IPinChangeDetailsRepository PinChangeDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями изменения PIN-кода.
    /// </summary>
    IPinChangeRepository PinChange { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями изменения статуса токена.
    /// </summary>
    ITokenStatusChangeDetailsRepository TokenStatusChangeDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями изменения статуса токена.
    /// </summary>
    ITokenStatusChangeRepository TokenStatusChange { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с деталями операций разблокировки.
    /// </summary>
    IUnholdDetailsRepository UnholdDetails { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями разблокировки.
    /// </summary>
    IUnholdRepository Unhold { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с обёртками лимитов информации по счетам.
    /// </summary>
    IAccountsInfoLimitWrapperRepository AccountsInfoLimitWrapper { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с обёртками лимитов информации по картам.
    /// </summary>
    ICardInfoLimitWrapperRepository CardInfoLimitWrapper { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с информацией по картам.
    /// </summary>
    ICardInfoRepository CardInfo { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с проверенными лимитами.
    /// </summary>
    ICheckedLimitRepository CheckedLimit { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с параметрами расширений.
    /// </summary>
    IExtensionParameterRepository ExtensionParameter { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с финансовыми транзакциями.
    /// </summary>
    IFinTransactionRepository FinTransaction { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с информацией по счетам.
    /// </summary>
    IAccountsInfoRepository AccountsInfos { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с лимитами.
    /// </summary>
    ILimitRepository Limit { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с информацией о торговцах.
    /// </summary>
    IMerchantInfoRepository MerchantInfo { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с расширениями уведомлений.
    /// </summary>
    INotificationExtensionRepository NotificationExtension { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с архивированными сообщениями входящих.
    /// </summary>
    IInboxArchiveMessageRepository InboxArchiveMessage { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с сообщениями уведомлений.
    /// </summary>
    INotificationMessageRepository NotificationMessage { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с ключевыми словами сообщений уведомлений.
    /// </summary>
    INotificationMessageKeyWordsRepository NotificationMessageKeyWords { get; }
    
    /// <summary>
    /// Получает репозиторий для работы со справочником текстов сообщений уведомлений.
    /// </summary>
    INotificationMessageTextDirectoriesRepository NotificationMessageTextDirectories { get; }
    
    /// <summary>
    /// Получает репозиторий для работы со справочником описаний лимитов.
    /// </summary>
    ILimitIdDescriptionDirectoriesRepository LimitIdDescriptionDirectories { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с валютами.
    /// </summary>
    ICurrenciesRepository Currencies { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями AcsOtp.
    /// </summary>
    IAcsOtpRepository AcsOtps { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями dbo.Accounts.
    /// </summary>
    IAccountsRepository Accounts { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями PushNotification.Settings.
    /// </summary>
    IPushNotificationSettingsRepository PushNotificationSettings { get; }
    
    /// <summary>
    /// Получает репозиторий для работы с операциями cards.Offices.
    /// </summary>
    IOfficesRepository Offices { get; }
    
    /// <summary>
    /// Асинхронно сохраняет все изменения, внесённые в контекст.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Количество объектов, записанных в базу данных.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает запрос IQueryable для выполнения сырого SQL-запроса.
    /// </summary>
    /// <typeparam name="T">Тип сущности, к которой применяется запрос.</typeparam>
    /// <param name="sql">SQL-запрос.</param>
    /// <param name="parameters">Параметры запроса.</param>
    /// <returns>IQueryable для указанного SQL-запроса.</returns>
    IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class;
    
    /// <summary>
    /// Начинает транзакцию асинхронно.
    /// </summary>
    void BeginTransactionAsync();
    
    /// <summary>
    /// Фиксирует текущую транзакцию асинхронно.
    /// </summary>
    void CommitTransactionAsync();
    
    /// <summary>
    /// Откатывает текущую транзакцию асинхронно.
    /// </summary>
    void RollbackTransactionAsync();
    
    /// <summary>
    /// Прикрепляет указанную сущность к контексту.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <param name="entity">Сущность для прикрепления.</param>
    void Attach<TEntity>(TEntity entity) where TEntity : class;
    
    /// <summary>
    /// Даёт IQueryable для любого Entity-типа T.
    /// </summary>
    IQueryable<T> Query<T>() where T : class;
    
    /// <summary>
    /// Добавляет новую сущность T в контекст.
    /// </summary>
    void Add<T>(T entity)
        where T : class;
}
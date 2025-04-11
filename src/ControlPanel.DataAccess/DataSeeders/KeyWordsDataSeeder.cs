using ControlPanel.DataAccess.Entities;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.DataSeeders;

/// <summary>
/// Класс для генерации данных ключевых слов уведомлений в базу данных.
/// </summary>
public class KeyWordsDataSeeder(ControlPanelDbContext context)
{
    /// <summary>
    /// Инициализирует посев ключевых слов для различных типов уведомлений.
    /// Вызывает методы для посева ключевых слов для каждого типа уведомления.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    public async Task SeedKeyWordsAsync()
    {
        await SeedIssFinAuthKeyWordsAsync();
        await SeedAcqFinAuthKeyWordsAsync();
        await SeedPinChangeKeyWordsAsync();
        await SeedCardStatusChangeKeyWordsAsync();
        await SeedUnholdKeyWordsAsync();
        await SeedOwiUserActionKeyWordsAsync();
        await SeedAcctBalChangeKeyWordsAsync();
        await SeedTokenStatusChangeKeyWordsAsync();
        await SeedAcsOtpKeyWordsAsync();
    }

    /// <summary>
    /// Посев ключевых слов для уведомлений типа IssFinAuth.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа IssFinAuth.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedIssFinAuthKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{TRANSTYPE}",
            "{REVERSAL}",
            "{PAN}",
            "{EXPDATE}",
            "{ACCOUNTID}",
            "{AUTHMONEY_AMOUNT}",
            "{AUTHMONEY_CURRENCY}",
            "{CONVMONEY_AMOUNT}",
            "{CONVMONEY_CURRENCY}",
            "{ACCOUNTBALANCE_AMOUNT}",
            "{ACCOUNTBALANCE_CURRENCY}",
            "{BILLINGMONEY__AMOUNT}",
            "{BILLINGMONEY__CURRENCY}",
            "{LOCALTIME}",
            "{TRANSATIONTIME}",
            "{RRN}",
            "{TERMINALID}",
            "{NAME}",
            "{CITY}",
            "{COUNTRY}",
            "{RESPONSECODE}",
            "{LIMIT}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.IssFinAuth, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа AcqFinAuth.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа AcqFinAuth.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedAcqFinAuthKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{TRANSTYPE}",
            "{REVERSAL}",
            "{PAN}",
            "{AUTHMONEY_AMOUNT}",
            "{AUTHMONEY_CURRENCY}",
            "{LOCALTIME}",
            "{RRN}",
            "{TERMINALID}",
            "{NAME}",
            "{CITY}",
            "{RESPONSECODE}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.AcqFinAuth, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа PinChange.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа PinChange.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedPinChangeKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{ACCTIDPANTAIL}",
            "{TRANSATIONTIME}",
            "{EXPDATE}",
            "{STATUS}",
            "{SERVICE}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.PinChange, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа CardStatusChange.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа CardStatusChange.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedCardStatusChangeKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{ACCTIDPANTAIL}",
            "{CHANGEDATE}",
            "{EXPDATE}",
            "{NEWSTATUS}",
            "{SERVICE}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.CardStatusChange, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа Unhold.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа Unhold.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedUnholdKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{TRANSTYPE}",
            "{REVERSAL}",
            "{PAN}",
            "{ACCOUNTID}",
            "{AUTHMONEY_AMOUNT}",
            "{AUTHMONEY_CURRENCY}",
            "{UNHOLDMONEY_AMOUNT}",
            "{UNHOLDMONEY_CURRENCY}",
            "{LOCALTIME}",
            "{TRANSATIONTIME}",
            "{RRN}",
            "{TERMINALID}",
            "{NAME}",
            "{CITY}",
            "{COUNTRY}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.Unhold, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа OwiUserAction.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа OwiUserAction.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedOwiUserActionKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{TRANSATIONTIME}",
            "{PAN}",
            "{EXPDATE}",
            "{ACTION}"
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.OwiUserAction, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа AcctBalChange.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа AcctBalChange.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedAcctBalChangeKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{REVERSAL}",
            "{TRANSATIONTIME}",
            "{ACCOUNTID}",
            "{PAN}",
            "{ACCOUNT_AMOUNT}",
            "{ACCOUNT_CURRENCY}",
            "{ACCOUNTBALANCE_AMOUNT}",
            "{ACCOUNTBALANCE_CURRENCY}",
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.AcctBalChange, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа TokenStatusChange.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа TokenStatusChange.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedTokenStatusChangeKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{PAYMENTSYSTEM}",
            "{STATUS}",
            "{CHANGEDATE}",
            "{DPANEXPDATE}",
            "{DEVICENAME}",
            "{DEVICEID}",
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.TokenStatusChange, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для уведомлений типа AcsOtp.
    /// Добавляет в базу данных ключевые слова с заполнителями для типа AcsOtp.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedAcsOtpKeyWordsAsync(CancellationToken cancellationToken = default)
    {
        var placeholders = new[]
        {
            "{OTP}",
            "{PAN}",
            "{TRANSATIONTIME}",
            "{AUTHMONEY_AMOUNT}",
            "{AUTHMONEY_CURRENCY}",
            "{NAME}",
            "{URL}",
        };
        
        await SeedKeyWordsForTypeAsync(NotificationMessageType.AcsOtp, placeholders, cancellationToken);
    }
    
    /// <summary>
    /// Посев ключевых слов для указанного типа уведомления.
    /// Производит выборку уже существующих ключевых слов, добавляет отсутствующие ключевые слова и сохраняет изменения в базе данных.
    /// </summary>
    /// <param name="type">Тип уведомления, для которого производится посев ключевых слов.</param>
    /// <param name="placeholders">Массив строк-заполнителей, представляющих ключевые слова для посева.</param>
    /// <param name="cancellationToken">Токен для отмены асинхронной операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию посева данных.</returns>
    private async Task SeedKeyWordsForTypeAsync(NotificationMessageType type, string[] placeholders, CancellationToken cancellationToken = default)
    {
        var existingKeyWords = await context.NotificationMessageKeyWords
            .Where(x => x.NotificationType == type)
            .Select(x => x.KeyWord)
            .ToListAsync(cancellationToken);

        var newKeyWords = placeholders
            .Where(ph => !existingKeyWords.Contains(ph))
            .Select(ph => NotificationMessageKeyWord.Create(ph, type))
            .ToList();

        if (newKeyWords.Count == 0)
            return;
        
        await context.NotificationMessageKeyWords.AddRangeAsync(newKeyWords, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}
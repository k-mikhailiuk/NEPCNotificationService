using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entites.Enum;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.DataSeeders;

public class KeyWordsDataSeeder
{
    private readonly ControlPanelDbContext _context;

    public KeyWordsDataSeeder(ControlPanelDbContext context)
    {
        _context = context;
    }

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
    
    private async Task SeedKeyWordsForTypeAsync(NotificationMessageType type, string[] placeholders, CancellationToken cancellationToken = default)
    {
        var existingKeyWords = await _context.NotificationMessageKeyWords
            .Where(x => x.NotificationType == type)
            .Select(x => x.KeyWord)
            .ToListAsync(cancellationToken);

        var newKeyWords = placeholders
            .Where(ph => !existingKeyWords.Contains(ph))
            .Select(ph => NotificationMessageKeyWord.Create(ph, type))
            .ToList();

        if (newKeyWords.Count == 0)
            return;
        
        await _context.NotificationMessageKeyWords.AddRangeAsync(newKeyWords, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
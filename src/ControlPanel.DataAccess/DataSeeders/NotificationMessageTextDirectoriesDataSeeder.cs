using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.DataSeeders;

public class NotificationMessageTextDirectoriesDataSeeder
{
    private readonly ControlPanelDbContext _context;
    
    private readonly NotificationOperationType[] operationTypes = [
        NotificationOperationType.DECREASE_AUTHORIZATION_AMOUNT,
        NotificationOperationType.INCREASE_AUTHORIZATION_AMOUNT,
        NotificationOperationType.UTIL_PAYMENT,
        NotificationOperationType.SBP_С2С_DEBIT,
        NotificationOperationType.SBP_С2B_DEBIT,
        NotificationOperationType.G2C_PAYMENT,
        NotificationOperationType.CASH_IN,
        NotificationOperationType.SBP_CREDIT,
        NotificationOperationType.CASH_BACK_PART,
        NotificationOperationType.EPOS_DEBIT_ONLINE,
        NotificationOperationType.PRE_EPURCHASE_AUTH,
        NotificationOperationType.EPOS_PURCHASE,
        NotificationOperationType.EPOS_REFUND,
        NotificationOperationType.FAKE_CREDIT,
        NotificationOperationType.FAKE_DEBIT,
        NotificationOperationType.PAYMENT,
        NotificationOperationType.ATM_WDL,
        NotificationOperationType.BALINQ,
        NotificationOperationType.COF_CASH_DEPOSIT,
        NotificationOperationType.CNP_CASH_IN,
        NotificationOperationType.PRE_PURCHASE_AUTHORIZATION,
        NotificationOperationType.PURCHASE_WITH_ELECTRONIC_CERTIFICATE,
        NotificationOperationType.PURCHASE_BY_ELECTRONIC_CERTITIFCATE_ONLY,
        NotificationOperationType.RETURN_REFAUND_BY_ELECTRONIC_CERTIFICATE,
        NotificationOperationType.RETURN_REFAUND_WITH_ELECTRONIC_CERTIFICATE,
        NotificationOperationType.EPOS_CREDIT_ONLINE,
        NotificationOperationType.PURCHASE,
        NotificationOperationType.REFUND,
        NotificationOperationType.PURCHASE_WITH_CASHBACK,
        NotificationOperationType.POS_CASH_ADVANCE_PVN,
        NotificationOperationType.P2P_DEBIT,
        NotificationOperationType.POS_BALANCE_INQ,
        NotificationOperationType.P2P_CREDIT,
        NotificationOperationType.EPOS_CREDIT,
        NotificationOperationType.EPOS_DEBIT,
        NotificationOperationType.D2C_PAYMENT
    ]; 

    public NotificationMessageTextDirectoriesDataSeeder(ControlPanelDbContext context)
    {
        _context = context;
    }

    public async Task SeedNotificationTextAsync()
    {
        await SeedParameterizedNotificationTextsAsync(
            NotificationMessageType.IssFinAuth);

        await SeedParameterizedNotificationTextsAsync(
            NotificationMessageType.AcqFinAuth);

        await SeedParameterizedNotificationTextsAsync(
            NotificationMessageType.Unhold);
        
        await SeedParameterizedNotificationTextsAsync(
            NotificationMessageType.AcctBalChange);

        await SeedNonParameterizedNotificationTextAsync(
            NotificationMessageType.CardStatusChange);

        await SeedNonParameterizedNotificationTextAsync(
            NotificationMessageType.PinChange);
        
        await SeedNonParameterizedNotificationTextAsync(
            NotificationMessageType.TokenStatusChange);
        
        await SeedNonParameterizedNotificationTextAsync(
            NotificationMessageType.OwiUserAction);
    }

    private async Task SeedParameterizedNotificationTextsAsync(
        NotificationMessageType type,
        CancellationToken cancellationToken = default)
    {
        var existingOperationTypes = await _context.NotificationMessageTextDirectories
            .Where(x => x.NotificationType == type && x.OperationType.HasValue)
            .Select(x => x.OperationType!.Value)
            .ToListAsync(cancellationToken);

        var newEntries = operationTypes
            .Where(op => !existingOperationTypes.Contains(op))
            .Select(op =>
            {
                var entry = NotificationMessageTextDirectory.Create(type, op);
                return entry;
            })
            .ToList();

        if (newEntries.Count != 0)
        {
            await _context.NotificationMessageTextDirectories.AddRangeAsync(newEntries, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    
    private async Task SeedNonParameterizedNotificationTextAsync(
        NotificationMessageType type,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context.NotificationMessageTextDirectories
            .AnyAsync(x => x.NotificationType == type && x.OperationType == null, cancellationToken);

        if (!exists)
        {
            var entry = NotificationMessageTextDirectory.Create(type);
            await _context.NotificationMessageTextDirectories.AddAsync(entry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
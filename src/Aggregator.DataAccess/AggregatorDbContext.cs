using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.ABSEntities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using DataIngrestorApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess;

/// <summary>
/// Контекст базы данных для Aggregator.
/// </summary>
/// <remarks>
/// Этот класс является точкой входа для взаимодействия с базой данных, содержащей таблицы уведомлений, операций,
/// деталей транзакций и другой связанной информации.
/// </remarks>
public class AggregatorDbContext(DbContextOptions<AggregatorDbContext> options) : DbContext(options)
{
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<IssFinAuth> IssFinAuths { get; set; }
    public DbSet<AcqFinAuth> AcqFinAuths { get; set; }
    public DbSet<CardStatusChange> CardStatusChanges { get; set; }
    public DbSet<PinChange> PinChanges { get; set; }
    public DbSet<TokenStatusChange> TokenStatusChanges { get; set; }
    public DbSet<Unhold> Unholds { get; set; }
    public DbSet<OwiUserAction> OwiUserActions { get; set; }
    public DbSet<AcctBalChange> AcctBalChanges { get; set; }
    public DbSet<AcsOtp> AcsOtps { get; set; }

    public DbSet<IssFinAuthDetails> IssFinAuthDetails { get; set; }
    public DbSet<AcqFinAuthDetails> AcqFinAuthDetails { get; set; }
    public DbSet<CardStatusChangeDetails> CardStatusChangeDetails { get; set; }
    public DbSet<PinChangeDetails> PinChangeDetails { get; set; }
    public DbSet<TokenStatusChangeDetails> TokenStatusChangeDetails { get; set; }
    public DbSet<UnholdDetails> UnholdDetails { get; set; }
    public DbSet<OwiUserActionDetails> OwiUserActionDetails { get; set; }
    public DbSet<AcctBalChangeDetails> AcctBalChangeDetails { get; set; }
    public DbSet<AcsOtpDetails> AcsOtpsDetails { get; set; }

    public DbSet<MerchantInfo> MerchantInfos { get; set; }
    public DbSet<AccountsInfoLimitWrapper> AccountsInfoLimitWrappers { get; set; }
    public DbSet<CardInfo> CardInfos { get; set; }
    public DbSet<CardInfoLimitWrapper> CardInfoLimitWrappers { get; set; }
    public DbSet<CheckedLimit> CheckedLimits { get; set; }
    public DbSet<ExtensionParameter> ExtensionParameter { get; set; }
    public DbSet<FinTransaction> FinTransactions { get; set; }
    public DbSet<AccountInfo> AccountsInfos { get; set; }
    public DbSet<Limit> Limits { get; set; }
    public DbSet<NotificationExtension> NotificationExtensions { get; set; }

    public DbSet<InboxArchiveMessage> InboxArchiveMessages { get; set; }

    public DbSet<NotificationMessage> NotificationMessages { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PushNotificationSettings> PushNotificationSettings { get; set; }

    /// <summary>
    /// Настраивает модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Объект для построения модели данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        modelBuilder.Entity<Notification>()
            .ToTable("Notifications")
            .HasKey(n => n.NotificationId);
        
        modelBuilder.Entity<Notification>()
            .Property(n => n.NotificationType).IsRequired();
        modelBuilder.Entity<Notification>()
            .Property(n => n.EventId).IsRequired();
        modelBuilder.Entity<Notification>()
            .Property(n => n.Time).IsRequired();
        
        modelBuilder.Entity<Notification>()
            .HasMany(n => n.Extensions)
            .WithOne(e => e.Notification)
            .HasForeignKey(e => e.NotificationId);

        modelBuilder.Entity<InboxMessage>()
            .ToTable("InboxMessages", "nepc", t => t.ExcludeFromMigrations());

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
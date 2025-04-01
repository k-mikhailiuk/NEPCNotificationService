using Aggregator.DataAccess.Configurations;
using Aggregator.DataAccess.Configurations.NotificationDetails;
using Aggregator.DataAccess.Configurations.Notifications;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using ControlPanel.DataAccess.Configurations;
using ControlPanel.DataAccess.Entites;
using DataIngrestorApi.DataAccess.Configurations;
using DataIngrestorApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess;

public class AggregatorDbContext : DbContext
{
    public DbSet<InboxMessage> InboxMessages { get; set; }
    public DbSet<IssFinAuth> IssFinAuths { get; set; }
    public DbSet<AcqFinAuth> AcqFinAuths { get; set; }
    public DbSet<CardStatusChange> CardStatusChanges { get; set; }
    public DbSet<PinChange> PinChanges { get; set; }
    public DbSet<TokenStatusChange> TokenStatusChanges { get; set; }
    public DbSet<Unhold> Unholds { get; set; }
    public DbSet<OwiUserAction> OwiUserActions { get; set; }
    public DbSet<AcctBalChange> AcctBalChanges { get; set; }
    
    public DbSet<IssFinAuthDetails> IssFinAuthDetails { get; set; }
    public DbSet<AcqFinAuthDetails> AcqFinAuthDetails { get; set; }
    public DbSet<CardStatusChangeDetails> CardStatusChangeDetails { get; set; }
    public DbSet<PinChangeDetails> PinChangeDetails { get; set; }
    public DbSet<TokenStatusChangeDetails> TokenStatusChangeDetails { get; set; }
    public DbSet<UnholdDetails> UnholdDetails { get; set; }
    public DbSet<OwiUserActionDetails> OwiUserActionDetails { get; set; }
    public DbSet<AcctBalChangeDetails> AcctBalChangeDetails { get; set; }
    
    public DbSet<MerchantInfo> MerchantInfos { get; set; }
    public DbSet<AccountsInfoLimitWrapper> AccountsInfoLimitWrappers { get; set; }
    public DbSet<AcctBalChangeAccountsInfo> AcctBalChangeAccountsInfos { get; set; }
    public DbSet<CardInfo> CardInfos { get; set; }
    public DbSet<CardInfoLimitWrapper> CardInfoLimitWrappers { get; set; }
    public DbSet<CheckedLimit> CheckedLimits { get; set; }
    public DbSet<ExtensionParameter> ExtensionParameter { get; set; }
    public DbSet<FinTransaction> FinTransactions { get; set; }
    public DbSet<IssFinAuthAccountsInfo> IssFinAuthAccountsInfos { get; set; }
    public DbSet<Limit> Limits { get; set; }
    public DbSet<NotificationExtension> NotificationExtensions { get; set; }
    
    public DbSet<InboxArchiveMessage> InboxArchiveMessages { get; set; }
    
    public DbSet<NotificationMessage> NotificationMessages { get; set; }
    
    public DbSet<NotificationMessageTextDirectory> NotificationMessageTextDirectories { get; set; }
    public DbSet<NotificationMessageKeyWord> NotificationMessageKeyWords { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    
    public AggregatorDbContext(DbContextOptions<AggregatorDbContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("nepc");
        
        modelBuilder.Entity<InboxMessage>()
            .ToTable("InboxMessages", "nepc", t => t.ExcludeFromMigrations());
        
        modelBuilder.Entity<NotificationMessageKeyWord>()
            .ToTable("NotificationMessageKeyWords", "nepc", t => t.ExcludeFromMigrations());
        
        modelBuilder.Entity<Currency>()
            .ToTable("Currencies", "nepc", t => t.ExcludeFromMigrations());
        
        modelBuilder.Entity<NotificationMessageTextDirectory>()
            .ToTable("NotificationMessageTextDirectories", "nepc", t => t.ExcludeFromMigrations());

        ApplyNotificationConfigurations(modelBuilder);
        ApplyNotificationDetailsConfigurations(modelBuilder);
        ApplyAdditionalConfigurations(modelBuilder);
    }

    private static void ApplyNotificationConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new IssFinAuthConfiguration());
        modelBuilder.ApplyConfiguration(new AcqFinAuthConfiguration());
        modelBuilder.ApplyConfiguration(new CardStatusChangeConfiguration());
        modelBuilder.ApplyConfiguration(new PinChangeConfiguration());
        modelBuilder.ApplyConfiguration(new TokenStatusChangeConfiguration());
        modelBuilder.ApplyConfiguration(new UnholdConfiguration());
        modelBuilder.ApplyConfiguration(new OwiUserActionConfiguration());
        modelBuilder.ApplyConfiguration(new AcctBalChangeConfiguration());
    }

    private static void ApplyNotificationDetailsConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AcqFinAuthDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new AcqFinAuthDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new CardStatusChangeDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new PinChangeDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new TokenStatusChangeDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new UnholdDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new OwiUserActionDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new AcctBalChangeDetailsConfiguration());
    }

    private static void ApplyAdditionalConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MerchantInfoConfiguration());
        modelBuilder.ApplyConfiguration(new AccountsInfoLimitWrapperConfiguration());
        modelBuilder.ApplyConfiguration(new AcctBalChangeAccountsInfoConfiguration());
        modelBuilder.ApplyConfiguration(new CardInfoConfiguration());
        modelBuilder.ApplyConfiguration(new CardInfoLimitWrapperConfiguration());
        modelBuilder.ApplyConfiguration(new CheckedLimitConfiguration());
        modelBuilder.ApplyConfiguration(new ExtensionParameterConfiguration());
        modelBuilder.ApplyConfiguration(new FinTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new IssFinAuthAccountsInfoConfiguration());
        modelBuilder.ApplyConfiguration(new LimitConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationExtensionConfiguration());
        
        modelBuilder.ApplyConfiguration(new InboxArchiveMessageConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationMessageConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
    }
}
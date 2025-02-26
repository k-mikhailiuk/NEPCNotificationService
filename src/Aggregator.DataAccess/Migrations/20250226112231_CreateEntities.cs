using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nepc");

            migrationBuilder.CreateTable(
                name: "AcqFinAuthDetails",
                schema: "nepc",
                columns: table => new
                {
                    AcqFinAuthDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reversal = table.Column<bool>(type: "bit", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrespondingAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AuthMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDirection = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    LocalTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false),
                    ApprovalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcqFee_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AcqFee_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcqFeeDirection = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    ConvMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    ConvMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysTerm = table.Column<bool>(type: "bit", nullable: false),
                    AuthorizationCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosEntryMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcqFinAuthDetails", x => x.AcqFinAuthDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "CardInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefPan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardStatusChangeDetails",
                schema: "nepc",
                columns: table => new
                {
                    CardStatusChangeDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatusChangeDetails", x => x.CardStatusChangeDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "IssFinAuthDetails",
                schema: "nepc",
                columns: table => new
                {
                    IssFinAuthDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reversal = table.Column<bool>(type: "bit", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    IssInstId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrespondingAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AuthMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDirection = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ConvMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    ConvMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountBalance_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AccountBalance_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    BillingMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: false),
                    ApprovalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rrn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcqFee_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AcqFee_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcqFeeDirection = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    IssFee_Amount = table.Column<long>(type: "bigint", nullable: true),
                    IssFee_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssFeeDirection = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    SvTrace = table.Column<long>(type: "bigint", nullable: true),
                    AuthorizationCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletProvider_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletProvider_PaymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthMoneyDetails_OwnFundsMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AuthMoneyDetails_OwnFundsMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthMoneyDetails_ExceedLimitMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AuthMoneyDetails_ExceedLimitMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssFinAuthDetails", x => x.IssFinAuthDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "Limits",
                schema: "nepc",
                columns: table => new
                {
                    LimitId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CycleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CycleLength = table.Column<int>(type: "int", nullable: true),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrsValue = table.Column<long>(type: "bigint", nullable: false),
                    UsedValue = table.Column<long>(type: "bigint", nullable: false),
                    LimitType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limits", x => x.LimitId);
                });

            migrationBuilder.CreateTable(
                name: "MerchantInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mcc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwiUserActionDetails",
                schema: "nepc",
                columns: table => new
                {
                    OwiUserActionDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwiUserActionDetails", x => x.OwiUserActionDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "PinChangeDetails",
                schema: "nepc",
                columns: table => new
                {
                    PinChangeDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseCode = table.Column<int>(type: "int", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinChangeDetails", x => x.PinChangeDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "TokenStatusChangeDetails",
                schema: "nepc",
                columns: table => new
                {
                    TokenStatusChangeDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DpanRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ChangeDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DpanExpDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FpanRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenStatusChangeDetails", x => x.TokenStatusChangeDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "UnholdDetails",
                schema: "nepc",
                columns: table => new
                {
                    UnholdId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reversal = table.Column<bool>(type: "bit", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    CorrespondingAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AuthMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnholdDirection = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    UnholdMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    UnholdMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ApprovalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rrn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssFee_Amount = table.Column<long>(type: "bigint", nullable: true),
                    IssFee_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssFeeDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SvTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletProvider_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletProvider_PaymentSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dpan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardIdentifier_CardIdentifierType = table.Column<int>(type: "int", nullable: true),
                    CardIdentifier_CardIdentifierValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnholdDetails", x => x.UnholdId);
                });

            migrationBuilder.CreateTable(
                name: "CardStatusChanges",
                schema: "nepc",
                columns: table => new
                {
                    CardStatusChangeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatusChanges", x => x.CardStatusChangeId);
                    table.ForeignKey(
                        name: "FK_CardStatusChanges_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CardStatusChanges_CardStatusChangeDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "CardStatusChangeDetails",
                        principalColumn: "CardStatusChangeDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckedLimits",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectType = table.Column<byte>(type: "tinyint", nullable: false),
                    Exceeded = table.Column<bool>(type: "bit", nullable: false),
                    IssFinAuthDetailsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckedLimits_IssFinAuthDetails_IssFinAuthDetailsId",
                        column: x => x.IssFinAuthDetailsId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuthDetails",
                        principalColumn: "IssFinAuthDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardInfoLimitWrappers",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LimitType = table.Column<byte>(type: "tinyint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false),
                    LimitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardInfoLimitWrappers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardInfoLimitWrappers_CardInfos_Id",
                        column: x => x.Id,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardInfoLimitWrappers_Limits_LimitId",
                        column: x => x.LimitId,
                        principalSchema: "nepc",
                        principalTable: "Limits",
                        principalColumn: "LimitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcqFinAuths",
                schema: "nepc",
                columns: table => new
                {
                    AcqFinAuthId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    MerchantInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcqFinAuths", x => x.AcqFinAuthId);
                    table.ForeignKey(
                        name: "FK_AcqFinAuths_AcqFinAuthDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "AcqFinAuthDetails",
                        principalColumn: "AcqFinAuthDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcqFinAuths_MerchantInfos_MerchantInfoId",
                        column: x => x.MerchantInfoId,
                        principalSchema: "nepc",
                        principalTable: "MerchantInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FinTransactions",
                schema: "nepc",
                columns: table => new
                {
                    FinTransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinTrans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranMoney_Amount = table.Column<long>(type: "bigint", nullable: true),
                    TranMoney_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    MerchantInfoId = table.Column<long>(type: "bigint", nullable: true),
                    CorrespondingAccountType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinTransactions", x => x.FinTransactionId);
                    table.ForeignKey(
                        name: "FK_FinTransactions_MerchantInfos_MerchantInfoId",
                        column: x => x.MerchantInfoId,
                        principalSchema: "nepc",
                        principalTable: "MerchantInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssFinAuths",
                schema: "nepc",
                columns: table => new
                {
                    IssFinAuthId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: true),
                    MerchantInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssFinAuths", x => x.IssFinAuthId);
                    table.ForeignKey(
                        name: "FK_IssFinAuths_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IssFinAuths_IssFinAuthDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuthDetails",
                        principalColumn: "IssFinAuthDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssFinAuths_MerchantInfos_MerchantInfoId",
                        column: x => x.MerchantInfoId,
                        principalSchema: "nepc",
                        principalTable: "MerchantInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OwiUserActions",
                schema: "nepc",
                columns: table => new
                {
                    OwiUserActionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwiUserActions", x => x.OwiUserActionId);
                    table.ForeignKey(
                        name: "FK_OwiUserActions_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OwiUserActions_OwiUserActionDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "OwiUserActionDetails",
                        principalColumn: "OwiUserActionDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PinChanges",
                schema: "nepc",
                columns: table => new
                {
                    PinChangeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinChanges", x => x.PinChangeId);
                    table.ForeignKey(
                        name: "FK_PinChanges_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PinChanges_PinChangeDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "PinChangeDetails",
                        principalColumn: "PinChangeDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenStatusChanges",
                schema: "nepc",
                columns: table => new
                {
                    TokenChangeStatusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenStatusChanges", x => x.TokenChangeStatusId);
                    table.ForeignKey(
                        name: "FK_TokenStatusChanges_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TokenStatusChanges_TokenStatusChangeDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "TokenStatusChangeDetails",
                        principalColumn: "TokenStatusChangeDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unholds",
                schema: "nepc",
                columns: table => new
                {
                    UnholdId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MerchantInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unholds", x => x.UnholdId);
                    table.ForeignKey(
                        name: "FK_Unholds_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Unholds_MerchantInfos_MerchantInfoId",
                        column: x => x.MerchantInfoId,
                        principalSchema: "nepc",
                        principalTable: "MerchantInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Unholds_UnholdDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "UnholdDetails",
                        principalColumn: "UnholdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcctBalChangeDetails",
                schema: "nepc",
                columns: table => new
                {
                    AcctBalChangeDetailsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reversal = table.Column<bool>(type: "bit", nullable: false),
                    TransType = table.Column<int>(type: "int", nullable: false),
                    TransactionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Auth_Id = table.Column<long>(type: "bigint", nullable: true),
                    Auth_Reversal = table.Column<bool>(type: "bit", nullable: true),
                    FinTransId = table.Column<long>(type: "bigint", nullable: true),
                    IssInstId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountAmount_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AccountAmount_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    AccountBalance_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AccountBalance_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcctBalChangeDetails", x => x.AcctBalChangeDetailsId);
                    table.ForeignKey(
                        name: "FK_AcctBalChangeDetails_FinTransactions_FinTransId",
                        column: x => x.FinTransId,
                        principalSchema: "nepc",
                        principalTable: "FinTransactions",
                        principalColumn: "FinTransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssFinAuthAccountsInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssFinAuthId = table.Column<long>(type: "bigint", nullable: false),
                    AccountsInfoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AviableBalance_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AviableBalance_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceedLimit_Amount = table.Column<long>(type: "bigint", nullable: true),
                    ExceedLimit_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssFinAuthAccountsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssFinAuthAccountsInfos_IssFinAuths_IssFinAuthId",
                        column: x => x.IssFinAuthId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuths",
                        principalColumn: "IssFinAuthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcctBalChanges",
                schema: "nepc",
                columns: table => new
                {
                    AcctBalChangeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DetailsId = table.Column<long>(type: "bigint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcctBalChanges", x => x.AcctBalChangeId);
                    table.ForeignKey(
                        name: "FK_AcctBalChanges_AcctBalChangeDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChangeDetails",
                        principalColumn: "AcctBalChangeDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcctBalChanges_CardInfos_CardInfoId",
                        column: x => x.CardInfoId,
                        principalSchema: "nepc",
                        principalTable: "CardInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AcctBalChangeAccountsInfos",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcctBalChangeId = table.Column<long>(type: "bigint", nullable: false),
                    AccountsInfoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AviableBalance_Amount = table.Column<long>(type: "bigint", nullable: true),
                    AviableBalance_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceedLimit_Amount = table.Column<long>(type: "bigint", nullable: true),
                    ExceedLimit_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcctBalChangeAccountsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcctBalChangeAccountsInfos_AcctBalChanges_AcctBalChangeId",
                        column: x => x.AcctBalChangeId,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChanges",
                        principalColumn: "AcctBalChangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationExtensions",
                schema: "nepc",
                columns: table => new
                {
                    ExtensionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    Critical = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationExtensions", x => x.ExtensionId);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_AcctBalChanges_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChanges",
                        principalColumn: "AcctBalChangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_AcqFinAuths_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "AcqFinAuths",
                        principalColumn: "AcqFinAuthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_CardStatusChanges_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "CardStatusChanges",
                        principalColumn: "CardStatusChangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_IssFinAuths_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuths",
                        principalColumn: "IssFinAuthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_OwiUserActions_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "OwiUserActions",
                        principalColumn: "OwiUserActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_PinChanges_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "PinChanges",
                        principalColumn: "PinChangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_TokenStatusChanges_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "TokenStatusChanges",
                        principalColumn: "TokenChangeStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationExtensions_Unholds_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "nepc",
                        principalTable: "Unholds",
                        principalColumn: "UnholdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsInfoLimitWrappers",
                schema: "nepc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LimitType = table.Column<byte>(type: "tinyint", nullable: false),
                    CardInfoId = table.Column<long>(type: "bigint", nullable: false),
                    LimitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsInfoLimitWrappers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsInfoLimitWrappers_AcctBalChangeAccountsInfos_Id",
                        column: x => x.Id,
                        principalSchema: "nepc",
                        principalTable: "AcctBalChangeAccountsInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsInfoLimitWrappers_IssFinAuthAccountsInfos_Id",
                        column: x => x.Id,
                        principalSchema: "nepc",
                        principalTable: "IssFinAuthAccountsInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsInfoLimitWrappers_Limits_LimitId",
                        column: x => x.LimitId,
                        principalSchema: "nepc",
                        principalTable: "Limits",
                        principalColumn: "LimitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtensionParameters",
                schema: "nepc",
                columns: table => new
                {
                    ExtensionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionParameters", x => x.ExtensionId);
                    table.ForeignKey(
                        name: "FK_ExtensionParameters_NotificationExtensions_ExtensionId",
                        column: x => x.ExtensionId,
                        principalSchema: "nepc",
                        principalTable: "NotificationExtensions",
                        principalColumn: "ExtensionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "AccountsInfoLimitWrappers",
                column: "LimitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeAccountsInfos_AcctBalChangeId",
                schema: "nepc",
                table: "AcctBalChangeAccountsInfos",
                column: "AcctBalChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChangeDetails_FinTransId",
                schema: "nepc",
                table: "AcctBalChangeDetails",
                column: "FinTransId",
                unique: true,
                filter: "[FinTransId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChanges_CardInfoId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "CardInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcctBalChanges_DetailsId",
                schema: "nepc",
                table: "AcctBalChanges",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcqFinAuths_DetailsId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcqFinAuths_MerchantInfoId",
                schema: "nepc",
                table: "AcqFinAuths",
                column: "MerchantInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardInfoLimitWrappers_LimitId",
                schema: "nepc",
                table: "CardInfoLimitWrappers",
                column: "LimitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardStatusChanges_CardInfoId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "CardInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardStatusChanges_DetailsId",
                schema: "nepc",
                table: "CardStatusChanges",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckedLimits_IssFinAuthDetailsId",
                schema: "nepc",
                table: "CheckedLimits",
                column: "IssFinAuthDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_FinTransactions_MerchantInfoId",
                schema: "nepc",
                table: "FinTransactions",
                column: "MerchantInfoId",
                unique: true,
                filter: "[MerchantInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuthAccountsInfos_IssFinAuthId",
                schema: "nepc",
                table: "IssFinAuthAccountsInfos",
                column: "IssFinAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuths_CardInfoId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "CardInfoId",
                unique: true,
                filter: "[CardInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuths_DetailsId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssFinAuths_MerchantInfoId",
                schema: "nepc",
                table: "IssFinAuths",
                column: "MerchantInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExtensions_NotificationId",
                schema: "nepc",
                table: "NotificationExtensions",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_OwiUserActions_CardInfoId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "CardInfoId",
                unique: true,
                filter: "[CardInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OwiUserActions_DetailsId",
                schema: "nepc",
                table: "OwiUserActions",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinChanges_CardInfoId",
                schema: "nepc",
                table: "PinChanges",
                column: "CardInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinChanges_DetailsId",
                schema: "nepc",
                table: "PinChanges",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_CardInfoId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "CardInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokenStatusChanges_DetailsId",
                schema: "nepc",
                table: "TokenStatusChanges",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unholds_CardInfoId",
                schema: "nepc",
                table: "Unholds",
                column: "CardInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unholds_DetailsId",
                schema: "nepc",
                table: "Unholds",
                column: "DetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unholds_MerchantInfoId",
                schema: "nepc",
                table: "Unholds",
                column: "MerchantInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsInfoLimitWrappers",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "CardInfoLimitWrappers",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "CheckedLimits",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "ExtensionParameters",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "AcctBalChangeAccountsInfos",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "IssFinAuthAccountsInfos",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "Limits",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "NotificationExtensions",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "AcctBalChanges",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "AcqFinAuths",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "CardStatusChanges",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "IssFinAuths",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "OwiUserActions",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "PinChanges",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "TokenStatusChanges",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "Unholds",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "AcctBalChangeDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "AcqFinAuthDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "CardStatusChangeDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "IssFinAuthDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "OwiUserActionDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "PinChangeDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "TokenStatusChangeDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "CardInfos",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "UnholdDetails",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "FinTransactions",
                schema: "nepc");

            migrationBuilder.DropTable(
                name: "MerchantInfos",
                schema: "nepc");
        }
    }
}

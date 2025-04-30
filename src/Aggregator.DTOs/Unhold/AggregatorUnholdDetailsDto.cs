namespace Aggregator.DTOs.Unhold;

/// <summary>
/// Подробная информация о снятии холда
/// </summary>
public record AggregatorUnholdDetailsDto
{
    /// <summary>
    /// Внутренний идентификатор авторизации (utrnno)
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Признак отмены (0 - false, 1 - true)
    /// </summary>
    public int Reversal { get; init; }

    /// <summary>
    /// Тип транзакции
    /// </summary>
    public int TransType { get; init; }

    /// <summary>
    /// Идентификатор института-корреспондента
    /// </summary>
    public string CorrespondingAccount { get; init; }

    /// <summary>
    /// Номер счета
    /// </summary>
    public string AccountId { get; init; }

    /// <summary>
    /// Сумма авторизации в валюте операции. Включает эквайринговую комиссию
    /// </summary>
    public AggregatorMoneyDto AuthMoney { get; init; }

    /// <summary>
    /// Направление движения средств относительно счета карты. C - счет кредитуется, D - счет дебетуется.
    /// Направление движения средств расхолдирования обратно движению средств оригинальной авторизации
    /// </summary>
    public char UnholdDirection { get; init; }

    /// <summary>
    /// Сумма авторизации в валюте счета. Не включает эмитентскую комиссию
    /// </summary>
    public AggregatorMoneyDto UnholdMoney { get; init; }

    /// <summary>
    /// Локальное время совершения авторизации на устройстве (YYYYMMDDHH24MISS)
    /// </summary>
    public string? LocalTime { get; init; }

    /// <summary>
    /// Время регистрации авторизации в ПЦ (YYYYMMDDHH24MISS)
    /// </summary>
    public string TransactionTime { get; init; }

    /// <summary>
    /// Код авторизации эмитента
    /// </summary>
    public string ApprovalCode { get; init; }

    /// <summary>
    /// Retrieval Reference Number
    /// </summary>
    public string? RRN { get; init; }

    /// <summary>
    /// Комиссия банка-эмитента в валюте счета
    /// </summary>
    public AggregatorMoneyDto? IssFee { get; init; }

    /// <summary>
    /// Направление движения эмитентской комиссии относительно счета карты оригинальной авторизации.
    /// Заполняется, если присутствует эмитентская комиссия. C - счет кредитуется, D - счет дебетуется
    /// </summary>
    public char? IssFeeDirection { get; init; }

    /// <summary>
    /// Идентификатор группы операций
    /// </summary>
    public long? SvTrace { get; init; }

    /// <summary>
    /// Мобильный кошелек
    /// </summary>
    public AggregatorWalletProviderDto? WalletProvider { get; init; }

    /// <summary>
    /// Токен карты
    /// </summary>
    public string? Dpan { get; init; }

    /// <summary>
    /// Список идентификаторов карты
    /// </summary>
    public IEnumerable<AggregatorCardIdentifierDto>? CardIdentifier { get; init; }
}
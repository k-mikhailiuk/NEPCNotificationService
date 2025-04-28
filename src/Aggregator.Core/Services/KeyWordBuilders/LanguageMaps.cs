using Common.Enums;

namespace Aggregator.Core.Services.KeyWordBuilders;

public static class LanguageMaps
{
    /// <summary>
    /// Словарь соответствий отмены для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<Language, string> Reversal { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Reversal",
            [Language.Russian] = "Отмена",
            [Language.Kyrgyz] = "Жокко чыгаруу",
        };

    /// <summary>
    /// Словарь соответствий кодов ответа для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<Language, string> ResponseCode { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Transaction declined. Please contact the bank.",
            [Language.Russian] = "Операция отклонена. Обратитесь в банк.",
            [Language.Kyrgyz] = "Операция четке кагылды. Банкка кайрылыңыз.",
        };

    /// <summary>
    /// Словарь соответствий статусов для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<int, (string Ru, string En, string Kg)> Status { get; }
        = new Dictionary<int, (string Ru, string En, string Kg)>()
        {
            [0] = ("Активный", "Active", "Активдүү"),
            [13] = ("Закрытый", "Closed", "Жабык"),
            [15] = ("Закрытый", "Closed", "Жабык"),
            [4] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
            [12] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
            [17] = ("Неактивированный ", "Inactive", "Активдүү эмес"),
            [1] = ("Заблокированный", "Locked", "Блоктолгон"),
            [2] = ("Заблокированный", "Locked", "Блоктолгон"),
            [3] = ("Заблокированный", "Locked", "Блоктолгон"),
            [5] = ("Заблокированный", "Locked", "Блоктолгон"),
            [6] = ("Заблокированный", "Locked", "Блоктолгон"),
            [7] = ("Заблокированный", "Locked", "Блоктолгон"),
            [8] = ("Заблокированный", "Locked", "Блоктолгон"),
            [9] = ("Заблокированный", "Locked", "Блоктолгон"),
            [10] = ("Заблокированный", "Locked", "Блоктолгон"),
            [11] = ("Заблокированный", "Locked", "Блоктолгон"),
            [14] = ("Заблокированный", "Locked", "Блоктолгон"),
            [24] = ("Заблокированный", "Locked", "Блоктолгон"),
            [25] = ("Заблокированный", "Locked", "Блоктолгон"),
        };

    /// <summary>
    /// Словарь соответствий действий для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<string, (string Ru, string En, string Kg)> Action { get; }
        = new Dictionary<string, (string Ru, string En, string Kg)>
        {
            ["addRedPathExclusion"] = ("Добавление карты в исключение RedPath", "addRedPathExclusion",
                "RedPath тизмесине картаны кошуу"),
            ["delRedPathExclusion"] = ("Удаление карты из исключения RedPath", "delRedPathExclusion",
                "RedPath тизмесинен картаны өчүрүү"),
            ["resetPinCounter"] = ("Сброс счетчика ПИНа", "resetPinCounter", "ПИН коду эсептегичин кайра баштоо"),
        };

    /// <summary>
    /// Словарь соответствий успешного статуса для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<Language, string> SuccessStatus { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Successfully",
            [Language.Russian] = "Успешно",
            [Language.Kyrgyz] = "Ийгиликтүү",
        };

    /// <summary>
    /// Словарь соответствий провального статуса для разных языков.
    /// </summary>
    public static IReadOnlyDictionary<Language, string> FailureStatus { get; }
        = new Dictionary<Language, string>
        {
            [Language.English] = "Unsuccessfully",
            [Language.Russian] = "Неуспешно",
            [Language.Kyrgyz] = "Ийгиликсиз",
        };
}
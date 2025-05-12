using Common;

namespace Aggregator.DataAccess.Entities.Enum;

public enum TransType
{
    [MultiLanguageDescription(
        "Техническая операция. Формируется, если после обработки транзакции в Бэк-офисе был уменьшен остаток на счете карты (лимит авторизации).",
        "Авторизация чеги төмөндөтүү", "Decreasing the authorization limit")]
    DECREASE_AUTHORIZATION_AMOUNT = 432,

    [MultiLanguageDescription(
        "Техническая операция. Формируется, если после обработки транзакции в Бэк-офисе был увеличен остаток на счете карты (лимит авторизации).",
        "Авторизация чегин көтөрүү", "Increasing the authorization limit")]
    INCREASE_AUTHORIZATION_AMOUNT = 433,

    [MultiLanguageDescription("Оплата Услуг", "кырг", "англ")]
    UTIL_PAYMENT = 508,

    [MultiLanguageDescription("Отправка перевода С2С по СБП с карты Банка",
        "Банк картасынан СБП аркылуу C2C трансфертин жөнөтүү", "Sending C2C transfer via SBP from the Bank's card")]
    SBP_С2С_DEBIT = 512,

    [MultiLanguageDescription("Отправка перевода С2B по СБП с карты Банка",
        "Банк картасынан СБП аркылуу C2B трансфертин жөнөтүү",
        "Sending C2B transfer via SBP from the Bank's card")]
    SBP_С2B_DEBIT = 513,

    [MultiLanguageDescription("Зачисление денежных средств на карты МИР от имени Оператора",
        "Оператор тарабынан МИР карталарына акча каражатын кредиттөө",
        "Credit of funds to MIR cards on behalf of the Operator")]
    G2C_PAYMENT = 554,

    [MultiLanguageDescription("Внесение средств через ATM\\PVN", "ATM/PVN аркылуу акча салуу",
        "Deposit of funds through ATM/PVN")]
    CASH_IN = 618,

    [MultiLanguageDescription("Перевод на карту Банка в сети СБП (Система Быстрых Платежей)",
        "СБП тармагындагы Банк картасына трансферт (Жылдам төлөмдөр системасы)",
        "Transfer to the Bank's card in the SBP network (System of Fast Payments)")]
    SBP_CREDIT = 622,

    [MultiLanguageDescription("Часть выдачи наличных для покупки с выдачей наличных",
        "Наличные алуу үчүн сатып алуу боюнча калыбына келтирүү",
        "Part of cash withdrawal for purchase with cash issuance")]
    CASH_BACK_PART = 653,

    [MultiLanguageDescription("Онлайн списание банком", "Банк тарабынан онлайн алым", "Online charge by the bank")]
    EPOS_DEBIT_ONLINE = 659,

    [MultiLanguageDescription("Предавторизация в е-POS", "e-POS системасында алдын ала авторизация",
        "Pre-authorization in e-POS")]
    PRE_EPURCHASE_AUTH = 677,

    [MultiLanguageDescription("Оплата в интернете", "Интернет аркылуу төлөө", "Payment online")]
    EPOS_PURCHASE = 680,

    [MultiLanguageDescription("Возврат товара для MO/TO", "MO/TO үчүн товарды кайтаруу", "Goods return for MO/TO")]
    EPOS_REFUND = 681,

    [MultiLanguageDescription("Пополнение из АБС (Банком)", "АБС (Банк тарабынан) толуктоо",
        "Replenishment from the ABS (by the Bank)")]
    FAKE_CREDIT = 687,

    [MultiLanguageDescription("Списание из АБС (Банком)", "АБС (Банк тарабынан) каражатты чыгаруу",
        "Charge from the ABS (by the Bank)")]
    FAKE_DEBIT = 688,

    [MultiLanguageDescription("Зачисление средств на карту", "Картка акча каражатын кредиттөө",
        "Credit of funds to the card")]
    PAYMENT = 698,

    [MultiLanguageDescription("Получение наличных в банкомате", "Банкоматтан акча алуу", "Cash withdrawal from ATM")]
    ATM_WDL = 700,

    [MultiLanguageDescription("Запрос баланса", "Баланс боюнча суроо", "Balance inquiry")]
    BALINQ = 702,

    [MultiLanguageDescription("Пополнение по сохраненным реквизитам карты",
        "Сакталган картанын реквизиттери боюнча толуктоо", "Replenishment by saved card details")]
    COF_CASH_DEPOSIT = 711,

    [MultiLanguageDescription("Пополнение карты МИР наличными без предъявления карты",
        "МИР картасын акча менен картаны көрсөтпөстөн толуктоо",
        "Replenishment of MIR card with cash without presenting the card")]
    CNP_CASH_IN = 712,

    [MultiLanguageDescription("Пре-авторизация в POS", "POS терминалында алдын ала авторизация",
        "Pre-authorization in POS")]
    PRE_PURCHASE_AUTHORIZATION = 736,

    [MultiLanguageDescription("Завершение пре-авторизации POS", "POS терминалында алдын ала авторизацияны аяктоо",
        "Completion of pre-authorization in POS")]
    POS_PRE_PURCH_CMPL = 737,

    [MultiLanguageDescription("Увеличение пре-авторизации POS", "POS терминалында алдын ала авторизацияны жогорулатуу",
        "Increase in pre-authorization in POS")]
    POS_INCR_PRE_PURCH = 739,

    [MultiLanguageDescription("Покупка за счет собственных средств при оплате с использованием ЭС",
        "ЭС аркылуу төлөөдө өз каражаттары менен сатып алуу", "Purchase with own funds when paying with E-money")]
    PURCHASE_WITH_ELECTRONIC_CERTIFICATE = 741,

    [MultiLanguageDescription("Покупка за счет средств ЭС", "ЭС каражаттары менен сатып алуу",
        "Purchase with E-money funds")]
    PURCHASE_BY_ELECTRONIC_CERTITIFCATE_ONLY = 745,

    [MultiLanguageDescription("Возврат покупки за счет средств ЭС", "ЭС каражаттары менен сатып алууну кайтаруу",
        "Return of purchase with E-money funds")]
    RETURN_REFAUND_BY_ELECTRONIC_CERTIFICATE = 746,

    [MultiLanguageDescription("Возврат покупки за счет собственных средств при оплате использованием ЭС",
        "ЭС аркылуу төлөөдө өз каражаттары менен сатып алууну кайтаруу",
        "Return of purchase with own funds when paying using E-money")]
    RETURN_REFAUND_WITH_ELECTRONIC_CERTIFICATE = 747,

    [MultiLanguageDescription("Credit Adjustment (ручной реверсал на операцию, совершенную в АТМ, по карте VISA)",
        "Кредиттик жөнгө салуу (VISA картасы менен ATMде жасалган транзакцияны кол менен артка кайтаруу)",
        "Credit Adjustment (manual reversal of a transaction made at ATM, using VISA card)")]
    VSMS_CREDITADJ = 750,

    [MultiLanguageDescription("Онлайн пополнение банком", "Банк тарабынан онлайн толуктоо",
        "Online deposit by the bank")]
    EPOS_CREDIT_ONLINE = 760,

    [MultiLanguageDescription("SMS-chargeback (Chargeback на операцию, совершенную в АТМ, по карте VISA)",
        "SMS-кайтаруу (ATMде VISA картасы менен жасалган транзакция боюнча кайтаруу)",
        "SMS-chargeback (Chargeback on a transaction made at ATM, using VISA card)")]
    VSMS_CHARGEBACK = 767,

    [MultiLanguageDescription("Оплата в торговом терминале", "Сатуу терминалында төлөм",
        "Payment in a point of sale terminal")]
    PURCHASE = 774,

    [MultiLanguageDescription("Операция возврата по безналичной операции", "Натыйжасыз операция үчүн товарды кайтаруу",
        "Return operation for cashless transaction")]
    REFUND = 775,

    [MultiLanguageDescription("Покупка с выдачей наличных", "Накталуу акча берүү менен сатып алуу",
        "Purchase with cash withdrawal")]
    PURCHASE_WITH_CASHBACK = 776,

    [MultiLanguageDescription("Получение наличных в кассовом терминале", "Кассалык терминалдан акча алуу",
        "Cash withdrawal from a cashier terminal")]
    POS_CASH_ADVANCE_PVN = 777,

    [MultiLanguageDescription("P2P (перевод карта-карта)", "P2P (картадан картага трансфер)",
        "P2P (card-to-card transfer)")]
    P2P_DEBIT = 781,

    [MultiLanguageDescription("Запрос баланса (в POS)", "Баланс боюнча суроо", "Balance inquiry (in POS)")]
    POS_BALANCE_INQ = 784,

    [MultiLanguageDescription("P-to-P (перевод карта-карта)", "P-to-P (картадан картага трансфер)",
        "P-to-P (card-to-card transfer)")]
    P2P_CREDIT = 785,

    [MultiLanguageDescription("Оф-лайн Пополнение (Банком)", "Офлайн толуктоо (Банк тарабынан)",
        "Offline Replenishment (by the Bank)")]
    EPOS_CREDIT = 888,

    [MultiLanguageDescription("Оф-лайн Списание (Банком)", "Офлайн алым (Банк тарабынан)",
        "Offline Charge (by the Bank)")]
    EPOS_DEBIT = 889,

    [MultiLanguageDescription("Зачисление денежных средств на карты МИР от имени Оператора (Выплата АСВ)",
        "Оператор тарабынан МИР карталарына акча каражатын кредиттөө (АСВ төлөмү)",
        "Credit of funds to MIR cards on behalf of the Operator (ASV payment)")]
    D2C_PAYMENT = 898
}
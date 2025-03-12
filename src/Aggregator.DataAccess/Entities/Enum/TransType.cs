using System.ComponentModel;

namespace Aggregator.DataAccess.Entities.Enum;

public enum TransType
{
    [Description("Оплата Услуг")]
    UTIL_PAYMENT = 508,
    
    [Description("Отправка перевода С2С по СБП с карты Банка")]
    SBP_С2С_DEBIT = 512,
    
    [Description("Отправка перевода С2B по СБП с карты Банка")]
    SBP_С2B_DEBIT = 513,
    
    [Description("Зачисление денежных средств на карты МИР от имени Оператора")]
    G2C_PAYMENT = 554,
    
    [Description("Внесение средств через ATM\\PVN")]
    CASH_IN = 618,
    
    [Description("Перевод на карту Банка в сети СБП (Система Быстрых Платежей)")]
    SBP_CREDIT = 622,
    
    [Description("Часть выдачи наличных для покупки с выдачей наличных")]
    CASH_BACK_PART = 653,
    
    [Description("Онлайн списание банком")]
    EPOS_DEBIT_ONLINE = 659,
    
    [Description("Предавторизация в е-POS")]
    PRE_EPURCHASE_AUTH = 677,
    
    [Description("Оплата в интернете")]
    EPOS_PURCHASE = 680,
    
    [Description("Возврат товара для MO/TO")]
    EPOS_REFUND = 681,
    
    [Description("Пополнение из АБС (Банком)")]
    FAKE_CREDIT = 687,
    
    [Description("Списание из АБС (Банком)")]
    FAKE_DEBIT = 688,
    
    [Description("Зачисление средств на карту")]
    PAYMENT = 698,
    
    [Description("Получение наличных в банкомате")]
    ATM_WDL = 700,
    
    [Description("Запрос баланса")]
    BALINQ = 702,
    
    [Description("Пополнение по сохраненным реквизитам карты")]
    COF_CASH_DEPOSIT = 711,
    
    [Description("Пополнение карты МИР наличными без предъявления карты")]
    CNP_CASH_IN = 712,
    
    [Description("Пре-авторизация в POS")]
    PRE_PURCHASE_AUTHORIZATION = 736,
    
    [Description("Покупка за счет собственных средств при оплате с использованием ЭС")]
    PURCHASE_WITH_ELECTRONIC_CERTIFICATE = 741,
    
    [Description("Покупка за счет средств ЭС")]
    PURCHASE_BY_ELECTRONIC_CERTIFICATE_ONLY = 745,
    
    [Description("Возврат покупки за счет средств ЭС")]
    RETURN_REFAUND_BY_ELECTRONIC_CERTIFICATE = 746,
    
    [Description("Возврат покупки за счет собственных средств при оплате использованием ЭС")]
    RETURN_REFAUND_WITH_ELECTRONIC_CERTIFICATE = 747,
    
    [Description("Онлайн пополнение банком")]
    EPOS_CREDIT_ONLINE = 760,
    
    [Description("Оплата в торговом терминале")]
    PURCHASE = 774,
    
    [Description("Операция возврата по безналичной операции")]
    REFUND = 775,
    
    [Description("Покупка с выдачей наличных")]
    PURCHASE_WITH_CASHBACK = 776,
    
    [Description("Получение наличных в кассовом терминале")]
    POS_CASH_ADVANCE_PVN = 777,
    
    [Description("P2P  (перевод карта-карта)")]
    P2P_DEBIT = 781,
    
    [Description("Запрос баланса (в POS)")]
    POS_BALANCE_INQ = 784,
    
    [Description("P-to-P (перевод карта-карта)")]
    P2P_CREDIT = 785,
    
    [Description("Оф-лайн Пополнение (Банком)")]
    EPOS_CREDIT = 888,
    
    [Description("Оф-лайн Списание (Банком)")]
    EPOS_DEBIT = 889,
    
    [Description("Зачисление денежных средств на карты МИР от имени Оператора (Выплата АСВ)\n")]
    D2C_PAYMENT = 898
}
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Core.Models;

public record NotificationDataLoad<T>
{
    public IEnumerable<T> Messages { get; set; }
    public Dictionary<long, NotificationMessageTextDirectory> NotificationTextById { get; init; }
    public Dictionary<long, int> NotificationToCustomer { get; init; }
    public Dictionary<int, int> CustomerSettingsMap { get; init; }
}
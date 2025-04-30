namespace DataIngrestorApi.DTOs.Abstractions;

public record NotificationDto<TDetails> : NotificationBaseDto
{
    public TDetails Details { get; set; }
}
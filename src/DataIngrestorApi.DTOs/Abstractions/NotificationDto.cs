namespace DataIngrestorApi.DTOs.Abstractions;

public record NotificationDto<TDetails> : NotificationBaseDto
{
    TDetails Details { get; set; }
}
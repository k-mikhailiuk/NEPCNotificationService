using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace DataIngrestorApi.App.Extensions;

/// <summary>
/// Кастомный форматтер для обработки запросов с типом содержимого "text/plain".
/// Поддерживает кодировки ISO-8859-1 и UTF-8.
/// </summary>
public class CustomTextPlainInputFormatter : TextInputFormatter
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomTextPlainInputFormatter"/> и задает поддерживаемые медиа типы и кодировки.
    /// </summary>
    public CustomTextPlainInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
        SupportedEncodings.Add(Encoding.GetEncoding("ISO-8859-1"));
        SupportedEncodings.Add(Encoding.UTF8);
    }

    /// <summary>
    /// Определяет, поддерживается ли тип для чтения.
    /// </summary>
    /// <param name="type">Тип данных, для которого проверяется возможность чтения.</param>
    /// <returns>
    /// <c>true</c>, если тип является <see cref="string"/>, иначе <c>false</c>.
    /// </returns>
    protected override bool CanReadType(Type type)
    {
        return type == typeof(string);
    }

    /// <summary>
    /// Читает тело запроса и возвращает его содержимое в виде строки.
    /// </summary>
    /// <param name="context">Контекст форматтера, содержащий информацию о HTTP-запросе.</param>
    /// <param name="encoding">Кодировка, используемая для чтения потока запроса.</param>
    /// <returns>
    /// Асинхронная задача, содержащая результат чтения тела запроса.
    /// </returns>
    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context,
        Encoding encoding)
    {
        var request = context.HttpContext.Request;
        using var reader = new StreamReader(request.Body, encoding);
        var content = await reader.ReadToEndAsync();
        return await InputFormatterResult.SuccessAsync(content);
    }
}
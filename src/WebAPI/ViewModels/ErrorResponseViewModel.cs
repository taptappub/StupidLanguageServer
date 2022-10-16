using System.Collections;
using System.Text.Json;

namespace WebAPI.ViewModels;

/// <summary>
/// Описание ошибки
/// </summary>
public class ErrorResponseViewModel
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Доп. данные при ошибке
    /// </summary>
    public ICollection<KeyValuePair<string, string>>? Data { get; set; }

    /// <summary>
    /// Ctor
    /// </summary>
    public ErrorResponseViewModel(Exception ex)
    {
        Message = ex.Message;
        Data = GetErrorData(ex.Data);
    }


    private static List<KeyValuePair<string, string>> GetErrorData(IDictionary data)
    {
        var result = new List<KeyValuePair<string, string>>();
        var iterator = data.GetEnumerator();

        while (iterator.MoveNext())
        {
            var key = JsonSerializer.Serialize(iterator.Key);
            var value = JsonSerializer.Serialize(iterator.Value);

            result.Add(new KeyValuePair<string, string>(key, value));
        }
        return result;
    }
}

using MediatR;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateWords;

/// <summary>
/// Создание или обновление списка слов
/// </summary>
public class CreateOrUpdateWordListCommnad : IRequest<List<WordDto>>
{
    /// <summary>
    /// Список слов
    /// </summary>
    public List<CreateOrUpdateWordCommnad> Words { get; set; } = new List<CreateOrUpdateWordCommnad>(0);
}

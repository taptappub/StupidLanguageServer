namespace WebAPI.Infrastructure.MVC;

/// <summary>
/// Авторизация
/// </summary>
public class AuthorizeUserAttribute : Attribute
{
    /// <summary>
    /// Обязательная авторизация
    /// </summary>
    public bool Required { get; set; } = true;
}

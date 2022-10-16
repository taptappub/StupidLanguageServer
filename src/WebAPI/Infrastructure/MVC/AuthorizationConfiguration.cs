namespace WebAPI.Infrastructure.MVC;

class AuthorizationConfiguration : IAuthorizationConfiguration
{
    public string Header { get; set; } = string.Empty;
}

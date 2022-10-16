namespace Infrastructure.Authorization;

public static class AuthorizationContext
{
    private static AsyncLocal<AuthorizedUser> _authUser = new AsyncLocal<AuthorizedUser>() { Value = AuthorizedUser.Anonymous };

    public static AuthorizedUser CurrentUser => _authUser.Value!;

    public static void SetUser(AuthorizedUser user)
    {
        _authUser.Value = user ?? throw new ArgumentNullException(nameof(user));
    }

    public static void ResetUser() => _authUser.Value = AuthorizedUser.Anonymous;
}

public class AuthenticationManager
{
    private static AuthenticationManager instance;

    private string authToken;
    private string refreshToken;

    private AuthenticationManager() { }

    public static AuthenticationManager Instance()
    {
        if (instance == null)
        {
            instance = new AuthenticationManager();
            instance.authToken = "";
            instance.refreshToken = "";
        }
        return instance;
    }

    public void SetAuthToken(string authToken)
    {
        this.authToken = authToken;
    }

    public void SetRefreshToken(string refreshToken)
    {
        this.refreshToken = refreshToken;
    }

    public string GetAuthToken()
    {
        return authToken;
    }

    public string GetRefreshToken()
    {
        return refreshToken;
    }

    public void RemoveAuthTokens()
    {
        this.authToken = "";
        this.refreshToken = "";
    }
}

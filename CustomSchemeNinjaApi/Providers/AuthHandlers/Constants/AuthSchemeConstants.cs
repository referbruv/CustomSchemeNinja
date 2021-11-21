namespace CustomSchemeNinjaApi.Providers.AuthHandlers.Constants
{
    public class AuthSchemeConstants
    {
        public const string MyNinjaAuthScheme = "Ninpo";
        public const string NToken = $"{MyNinjaAuthScheme} (?<token>.*)";
    }
}
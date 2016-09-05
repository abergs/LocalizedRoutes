namespace LocalizedRoutes
{
    public interface IRouteTokenReplacer
    {
        string CleanUp(string template);
        string ReplaceTokens(string template, string translation);
    }
}
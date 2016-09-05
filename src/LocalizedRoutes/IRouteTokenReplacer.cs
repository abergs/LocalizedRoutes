namespace LocalizedRoutes
{
    public interface IRouteTokenReplacer
    {
        /// <summary>
        /// Will be called to clean up tokenized-routes that does not have a translation. Making them valid untranslated routes.
        /// </summary>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        string CleanUp(string routeTemplate);
        /// <summary>
        /// Called to translate a route template with the value of a translation
        /// </summary>
        /// <param name="routeTemplate"></param>
        /// <param name="translation"></param>
        /// <returns></returns>
        string ReplaceTokens(string routeTemplate, string translation);
        /// <summary>
        /// Used to check if this route is tokenized.
        /// </summary>
        /// <param name="routeName"></param>
        /// <returns></returns>
        bool IsTokenized(string routeName);
    }
}
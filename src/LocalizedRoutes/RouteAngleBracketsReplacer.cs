using System;
using System.Text.RegularExpressions;

namespace LocalizedRoutes
{
    public class RouteAngleBracketsReplacer : IRouteTokenReplacer
    {
        private readonly Regex _reg = new Regex(@"(\<[^>]*\>)");

        public string ReplaceTokens(string routeTemplate, string translation)
        {
            var result = string.Empty;
            var templateParts = _reg.Split(routeTemplate);
            var parsedTranslation = translation.Split(',');
            var partsIndex = 0;
            foreach (var part in templateParts)
                if (part.StartsWith("<", StringComparison.Ordinal))
                {
                    if (partsIndex >= parsedTranslation.Length)
                    {
                        throw new InvalidRouteTranslationException(
                            $"Template has more tokens to replace than translation offers. Template: {routeTemplate}. Translation: {translation}");
                    }
                    result += parsedTranslation[partsIndex];
                    partsIndex++;
                }
                else
                {
                    result += part;
                }

            return CleanUp(result);
        }

        public bool IsTokenized(string routeName)
        {
            return routeName.StartsWith("<", StringComparison.Ordinal);
        }

        public string CleanUp(string routeTemplate)
        {
            var result = string.Empty;
            var templateParts = _reg.Split(routeTemplate);

            foreach (var part in templateParts)
                if (part.StartsWith("<", StringComparison.Ordinal))
                    result += part.Replace("<", "").Replace(">", "");
                else
                    result += part;

            return result;
        }
    }
}
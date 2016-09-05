using System;
using System.Text.RegularExpressions;

namespace LocalizedRoutes
{
    public class RouteAngleBracketsReplacer : IRouteTokenReplacer
    {
        private readonly Regex _reg = new Regex(@"(\<[^>]*\>)");

        public string ReplaceTokens(string template, string translation)
        {
            var result = string.Empty;
            var templateParts = _reg.Split(template);
            var parsedTranslation = translation.Split(',');
            var partsIndex = 0;
            foreach (var part in templateParts)
                if (part.StartsWith("<", StringComparison.Ordinal))
                {
                    if (partsIndex >= parsedTranslation.Length)
                    {
                        throw new InvalidRouteTranslationException(
                            $"Template has more tokens to replace than translation offers. Template: {template}. Translation: {translation}");
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

        public string CleanUp(string template)
        {
            var result = string.Empty;
            var templateParts = _reg.Split(template);

            foreach (var part in templateParts)
                if (part.StartsWith("<", StringComparison.Ordinal))
                    result += part.Replace("<", "").Replace(">", "");
                else
                    result += part;

            return result;
        }
    }
}
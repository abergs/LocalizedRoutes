using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LocalizedRoutes.Examples
{
    /// <summary>
    /// Example of how you could build a routes localizer
    /// </summary>
    /// <example>
    /// 
    /// This one parses a /routes.json file that has this schema:
    /// "Culture": {
    ///     "Theme": {
    ///         "RouteName":  "Translation"
    ///     }
    /// }
    /// 
    /// for example:
    /// 
    /// "sv-SE": {
    ///     "Dog": {
    ///         "how-it-works":"hur-det-fungerar"
    /// }}
    ///
    /// 
    /// </example>
    public class JsonRoutesLocalizerExample : IRouteLocalizationsAccessor
    {
        private readonly string _culture;
        private readonly JObject _jObject;
        private readonly string _theme;

        public JsonRoutesLocalizerExample(string culture, string theme)
        {
            _culture = culture;
            _theme = theme;

            var routes = File.ReadAllText("routes.json");
            _jObject = JObject.Parse(routes);
        }

        public Dictionary<string, LocalizedRouteInformation> GetLocalizations()
        {
            var translation = _jObject.SelectToken($"{_culture}.{_theme}");
            var values = translation.ToObject<Dictionary<string, string>>();

            var routedic = values.ToDictionary(
                pair => pair.Key,
                pair => new LocalizedRouteInformation(pair.Value));

            return routedic;
        }
    }
}
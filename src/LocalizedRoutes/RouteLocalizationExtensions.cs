using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LocalizedRoutes
{
    public static class RouteLocalizationExtensions
    {
        public static void AddLocalizedRoutes(this MvcOptions options,
            Dictionary<string, LocalizedRouteInformation> localizedRoutes)
        {
            var conf = new ConventionConfiguraiton(new DictionaryAccessor(localizedRoutes));
            options.Conventions.Insert(0, new LocalizedRouteConvention(conf));
        }

        public static void AddLocalizedRoutes(this MvcOptions options,
            ConventionConfiguraiton config)
        {
            options.Conventions.Insert(0, new LocalizedRouteConvention(config));
        }

        private class DictionaryAccessor : IRouteLocalizationsAccessor
        {
            private readonly Dictionary<string, LocalizedRouteInformation> _dic;

            public DictionaryAccessor(Dictionary<string, LocalizedRouteInformation> dic)
            {
                _dic = dic;
            }

            public Dictionary<string, LocalizedRouteInformation> GetLocalizations()
            {
                return _dic;
            }
        }
    }
}
using System.Collections.Generic;

namespace LocalizedRoutes
{
    public interface IRouteLocalizationsAccessor
    {
        Dictionary<string, LocalizedRouteInformation> GetLocalizations();
    }
}
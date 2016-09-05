using Microsoft.AspNetCore.Mvc;

namespace LocalizedRoutes
{
    public class LocalizedRouteAttribute : RouteAttribute
    {
        public LocalizedRouteAttribute(string template) : base(template)
        {
            //Culture = "sv-SE";
        }

        //public string Culture { get; set; }
    }
}
namespace LocalizedRoutes
{
    public class ConventionConfiguration
    {
        public ConventionConfiguration(IRouteLocalizationsAccessor localizationsAccessor)
        {
            LocalizationsAccessor = localizationsAccessor;
        }

        public IRouteTokenReplacer TokenReplacer { get; set; } = new RouteAngleBracketsReplacer();
        public IRouteLocalizationsAccessor LocalizationsAccessor { get; set; }
    }
}
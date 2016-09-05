namespace LocalizedRoutes
{
    public class ConventionConfiguraiton
    {
        public ConventionConfiguraiton(IRouteLocalizationsAccessor localizationsAccessor)
        {
            LocalizationsAccessor = localizationsAccessor;
        }

        public IRouteTokenReplacer TokenReplacer { get; set; } = new RouteAngleBracketsReplacer();
        public IRouteLocalizationsAccessor LocalizationsAccessor { get; set; }
    }
}
namespace LocalizedRoutes
{
    public class LocalizedRouteInformation
    {
        public LocalizedRouteInformation(string template)
        {
            //Culture = culture;
            Template = template;
        }

        //public string Culture { get; }
        public string Template { get; }

        // We could enter more information such as source for translation for debug reasons.
    }
}
using System;

namespace LocalizedRoutes
{
    public class InvalidRouteTranslationException : Exception
    {
        public InvalidRouteTranslationException(string s) : base(s)
        {
            
        }
    }
}
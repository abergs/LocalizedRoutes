using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace LocalizedRoutes
{
    public class LocalizedRouteConvention : IApplicationModelConvention
    {
        private readonly Dictionary<string, LocalizedRouteInformation> _localizedRoutes;
        private readonly IRouteTokenReplacer _tokenReplacer;


        public LocalizedRouteConvention(ConventionConfiguration options)
        {
            _localizedRoutes = options.LocalizationsAccessor.GetLocalizations();
            _tokenReplacer = options.TokenReplacer;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                SetSelectors(controller.Attributes, controller.Selectors);
                foreach (var action in controller.Actions)
                {
                    SetSelectors(action.Attributes, action.Selectors);
                }
            }
        }

        private void SetSelectors(IReadOnlyList<object> attributes, IList<SelectorModel> selectors)
        {
            // find localization attributes
            var localizedRouteAttributes = attributes.OfType<LocalizedRouteAttribute>().ToArray();
            // skip to next if this is not a localized route
            if (!localizedRouteAttributes.Any()) return;

            foreach (var localizedRouteAttribute in localizedRouteAttributes)
            {
                // process the name of the route and the route template
                var pr = ProcessRoute(localizedRouteAttribute.Name, localizedRouteAttribute.Template);

                // update the selectors that match the one we configured
                var matchedSelectors =
                    selectors.Where(s => s.AttributeRouteModel.Name == localizedRouteAttribute.Name)
                        .ToList();

                foreach (var selectorModel in matchedSelectors)
                    selectorModel.AttributeRouteModel = pr;

                // this is left over from strathweb, keep it for reference for how to implement more cultures
                //newAction.Properties["culture"] = new CultureInfo(((LocalizedRouteAttribute)localizedVersion.Attribute).Culture ?? "en-Ca");
                //newAction.Filters.Add(new LocalizedRouteFilter());
                //newActions.Add(newAction);*/
            }
        }

        private AttributeRouteModel ProcessRoute(string name, string template)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Localized route needs names");

            LocalizedRouteInformation translation;

            var isDirty = IsDirtyRoute(name);
            var newTemplate = template;


            // check if we have translation
            if (_localizedRoutes.TryGetValue(name, out translation))
            {
                // if dirty
                if (isDirty)
                    newTemplate = _tokenReplacer.ReplaceTokens(template, translation.Template);
                else
                    newTemplate = translation.Template;

                return
                    new AttributeRouteModel(new LocalizedRouteAttribute(newTemplate)
                    {
                        Name = name
                        //Culture = translation.Culture
                    });
            }
            if (isDirty)
                newTemplate = _tokenReplacer.CleanUp(template);

            return new AttributeRouteModel(new LocalizedRouteAttribute(newTemplate)
            {
                Name = name
            });
        }

        private bool IsDirtyRoute(string name)
        {
            return _tokenReplacer.IsTokenized(name);
        }
    }
}
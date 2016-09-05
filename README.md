# LocalizedRoutes
ASP.NET Core 1.0 Library and nuget package to translate routes

Based of [Strathweb blog post](http://www.strathweb.com/2015/11/localized-routes-with-asp-net-5-and-mvc-6/) but repurposed and compatible with ASP.NET Core 1.0 RTM.

## Install
`Install-Package LocalizedRoutes`

or add to `project.json` under dependencies:
`"LocalizedRoutes":"1.0.0"`

## Examples & How to use it

This library will allow you to specify attribroute and then replace them with localized versions on app startup:

```
[LocalizedRoutes("how-it-works", Name = "how")
public IActionResult Index() { ... }
```

You can store your translations however you want, for example in a dictionary or in a json file (you have to write the provider, look at the example:

`{"how-it-works":"hur-det-fungerar"}`

Add it to your Mvc service in `Startup.cs`.
```
// Get your translations however you want by implementing IRouteLocalizationsAccessor
var routeLocalizer = new JsonRoutesLocalizer(Configuration["Culture"], Configuration["Theme"]);
var routes = routeLocalizer.GetLocalizations();
services.AddMvc(o => o.AddLocalizedRoutes(routes))
```
### Supports advanced patterns without having to write your template pattern in your translations

You can write advanced routes and just store what you want to translate by using `<token>`.

```
[LocalizedRoutes("<account>/{id:int}/<details>", Name = "<how>")
public IActionResult Index() { ... }
```
Swedish:
Translation file: `{"<how>":"<konto>,<detaljer>"}`

Will render a route of: `konto/{id:int}/detaljer`.

### Advanced customization

You can replace a lot of the functionality by changing the configuration and creating your own implementaions of Interfaces:

* `IRouteTokenReplacer` - Create your own token pattern, if you dont want to use the `<token>` and `<token>,<token>` pattern.
* `IRouteLocalizationsAccessor`- Store your translation in any way you want, I use a JsonFile, but you could use a Dictionary, .ini-file or whatever.


## Purpose

I built this library to power a MVC site that runs them same code for multiple sites. I use this code to theme my URLs. 

**What it does:**  

1. Replace hardcoded attributeroutes with values from a config on startup
2. Allow you just replace parts of your url template


**What it does not support:**  

1. It does not auto translate anything.
2. It does not make use of Culture in anyway, yet.
3. It does not currently support multiple paths to the same action (multiple translations) in the same app instance runtime.
4. Currently it's more of a themable route library than a proper localization lib, but support should be easy to add.

However, the list above should be easy to support and if you could probably fork this lib to make it happen. Look at Starthwebs blog post to get the blueprint.

More documentation really needs to be written. Also look at the current Issue list to see more functionality / missing patterns. 


using Customer.GeneratedServices;
using EasyMicroservices.UI.Customers.Blazor.TestUI;
using EasyMicroservices.UI.Customers.ViewModels.PersonCategories;
using EasyMicroservices.UI.Customers.ViewModels.Persons;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new PersonClient("http://localhost:2009", sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new PersonCategoryClient("http://localhost:2009", sp.GetService<HttpClient>()));

builder.Services.AddScoped<AddOrUpdatePersonViewModel>();
builder.Services.AddScoped<FilterPersonsListViewModel>();

builder.Services.AddScoped<AddOrUpdatePersonCategoryViewModel>();
builder.Services.AddScoped<FilterPersonCategoriesListViewModel>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});

await builder.Build().RunAsync();

using Customer.GeneratedServices;
using EasyMicroservices.Domain.Contracts.Common;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Customers.Blazor.TestUI;
using EasyMicroservices.UI.Customers.ViewModels.PersonCategories;
using EasyMicroservices.UI.Customers.ViewModels.Persons;
using EasyMicroservices.UI.Places.ViewModels.Cities;
using EasyMicroservices.UI.Places.ViewModels.Countries;
using EasyMicroservices.UI.Places.ViewModels.Provinces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Places.GeneratedServices;
LoadLanguage("en-US");
BaseViewModel.CurrentApplicationLanguage = "en-US";
BaseViewModel.IsRightToLeft = false;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new PersonClient("http://localhost:2009", sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new PersonCategoryClient("http://localhost:2009", sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new CountryClient("http://localhost:2008", sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new CityClient("http://localhost:2008", sp.GetService<HttpClient>()));
builder.Services.AddScoped(sp => new ProvinceClient("http://localhost:2008", sp.GetService<HttpClient>()));

builder.Services.AddScoped<AddOrUpdatePersonViewModel>();
builder.Services.AddScoped<FilterPersonsListViewModel>();

builder.Services.AddScoped<AddOrUpdatePersonCategoryViewModel>();
builder.Services.AddScoped<FilterPersonCategoriesListViewModel>();
builder.Services.AddScoped<FilterCountriesListViewModel>();
builder.Services.AddScoped<FilterProvincesListViewModel>();
builder.Services.AddScoped<FilterCitiesListViewModel>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});

await builder.Build().RunAsync();

void LoadLanguage(string languageShortName)
{
    BaseViewModel.AppendLanguage("Saving", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Saving"
    });
    BaseViewModel.AppendLanguage("Save", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Save"
    });
    BaseViewModel.AppendLanguage("Search", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Search"
    });
    BaseViewModel.AppendLanguage("Processing", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Processing"
    });
    BaseViewModel.AppendLanguage("Id", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Id"
    });
    BaseViewModel.AppendLanguage("Name", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Name"
    });
    BaseViewModel.AppendLanguage("Actions", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Actions"
    });
    BaseViewModel.AppendLanguage("Delete", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete"
    });
    BaseViewModel.AppendLanguage("Deleting", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Deleting"
    });
    BaseViewModel.AppendLanguage("Cancel", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Cancel"
    });
    BaseViewModel.AppendLanguage("DeleteQuestion_Content", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Do you really want to delete these records? This process cannot be undone."
    });
    BaseViewModel.AppendLanguage("Customers_PersonCategories_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Categories"
    });
    BaseViewModel.AppendLanguage("Ordering_ValueAddedTax_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Value Added Tax"
    });
    BaseViewModel.AppendLanguage("Customers_PersonCategory_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Category"
    });
    BaseViewModel.AppendLanguage("Customers_PersonCategory_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete Person Category"
    });
    BaseViewModel.AppendLanguage("Customers_UpdatePersonCategory_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Update Person Category"
    });
    BaseViewModel.AppendLanguage("Customers_UpdatePersonCategory_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Category updated."
    });
    BaseViewModel.AppendLanguage("Customers_AddPersonCategory_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Add Person Category"
    });
    BaseViewModel.AppendLanguage("Customers_AddPersonCategory_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Category added."
    });
    BaseViewModel.AppendLanguage("Customers_DeletePersonCategory_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Category deleted!"
    });
    BaseViewModel.AppendLanguage("Customers_ExternalServiceIdentifier_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "External Service Identifier"
    });
    BaseViewModel.AppendLanguage("Customers_PersonType_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person Type"
    });
    BaseViewModel.AppendLanguage("Customers_Persons_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Persons"
    });
    BaseViewModel.AppendLanguage("Customers_DeletePerson_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Delete Person"
    });
    BaseViewModel.AppendLanguage("Customers_DeletePerson_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person deleted!"
    });
    BaseViewModel.AppendLanguage("Customers_UpdatePerson_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Update Person"
    });
    BaseViewModel.AppendLanguage("Customers_UpdatePerson_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person updated."
    });
    BaseViewModel.AppendLanguage("Ordering_UpdateProduct_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Product updated."
    });
    BaseViewModel.AppendLanguage("Customers_AddPerson_Title", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Add Person"
    });
    BaseViewModel.AppendLanguage("Customers_AddPerson_Message", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Person added."
    });
    BaseViewModel.AppendLanguage("FirstName", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "First Name"
    });
    BaseViewModel.AppendLanguage("PassportNumber", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Passport Number"
    });
    BaseViewModel.AppendLanguage("VisaNumber", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Visa Number"
    });
    BaseViewModel.AppendLanguage("PhoneNumber", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Phone Number"
    });
    BaseViewModel.AppendLanguage("EmailAddress", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Email Address"
    });
    BaseViewModel.AppendLanguage("Country", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Country"
    });
    BaseViewModel.AppendLanguage("LastName", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "LastName"
    });
    BaseViewModel.AppendLanguage("WebsiteAddress", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "WebsiteAddress"
    });
    BaseViewModel.AppendLanguage("PostalCode", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "PostalCode"
    });
    BaseViewModel.AppendLanguage("MobileNumber", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "MobileNumber"
    });
    BaseViewModel.AppendLanguage("City", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "City"
    });
    BaseViewModel.AppendLanguage("Address", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Address"
    });
    BaseViewModel.AppendLanguage("Province", new LanguageContract()
    {
        ShortName = languageShortName,
        Value = "Province"
    });
}
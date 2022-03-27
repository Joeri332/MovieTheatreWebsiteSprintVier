//using Microsoft.EntityFrameworkCore;

using System.Globalization;
using Microsoft.AspNetCore.Localization;
using MovieTheatreDatabase;
using Microsoft.EntityFrameworkCore;
using Stripe;
using MovieTheatreUtility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieTheatreDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieTheatreWebsiteContext")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//TODO Wrong input in PriceController,
var defaultDateCulture = "nl-NL";
var ci = new CultureInfo(defaultDateCulture);
ci.NumberFormat.NumberDecimalSeparator = ".";
ci.NumberFormat.CurrencyDecimalSeparator = ".";

// Configure the Localization middleware
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo>
    {
        ci,
    },
    SupportedUICultures = new List<CultureInfo>
    {
        ci,
    }
});

app.Run();

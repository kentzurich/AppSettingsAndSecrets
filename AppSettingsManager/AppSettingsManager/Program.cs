using AppSettingsManager;
using AppSettingsManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Configurations
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSettings"));
builder.Services.AddConfiguration<TwilioSettings>(builder.Configuration, "Twilio");

//Setting Default Hierarchy.
var hostingEnvironment = builder.Environment.EnvironmentName;
//var hostingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{hostingEnvironment}.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"customJson.json", optional: true, reloadOnChange: true);

if (hostingEnvironment.Equals("Development"))
    builder.Configuration.AddUserSecrets<Program>();
else
    builder.Configuration.AddAzureKeyVault($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
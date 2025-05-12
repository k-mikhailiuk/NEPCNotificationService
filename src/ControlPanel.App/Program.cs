using ControlPanel.App;
using ControlPanel.DataAccess.DataSeeders;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false,
    reloadOnChange: true);

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var identityDataSeeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
    var keyWordsDataSeeder = scope.ServiceProvider.GetRequiredService<KeyWordsDataSeeder>();
    var notificationMessageTextDirectoriesDataSeeder =
        scope.ServiceProvider.GetRequiredService<NotificationMessageTextDirectoriesDataSeeder>();

    await identityDataSeeder.SeedDefaultUserAsync(app.Services);
    await keyWordsDataSeeder.SeedKeyWordsAsync();
    await notificationMessageTextDirectoriesDataSeeder.SeedNotificationTextAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NotificationMessageKeyWords}/{action=Index}");

app.Run();
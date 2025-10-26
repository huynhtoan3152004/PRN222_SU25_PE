using LionPetManagement_ToanHH.Hubs;
using LionPetManagement_ToanHH_Service;
using Microsoft.AspNetCore.Authentication.Cookies;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<ILionTypeService, LionTypeService>();
builder.Services.AddScoped<ILionProfileService, LionProfileService>();
builder.Services.AddScoped<ILionAccountService, LionAccountService>();

// Add Cookie
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/Forbidden";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });
// -------------------------------------------------------------------------------------
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
/// Add authentication middleware
app.MapRazorPages().RequireAuthorization();
/// ----------------------------------------

app.MapHub<LionProfileHubs>("/LionProfileHubs");

app.Run();

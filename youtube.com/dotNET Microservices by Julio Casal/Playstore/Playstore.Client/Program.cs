using Microsoft.AspNetCore.Authentication.Cookies;
using Playstore.Client.DependencyInjection;
using Playstore.Client.Services;
using Playstore.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IMvcBuilder mvcBuilder = builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services.AddServiceCients();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });
builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

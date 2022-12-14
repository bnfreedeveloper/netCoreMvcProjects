using authAuthorization.Models.Entities;
using authAuthorization.Repositories.IRepositories;
using authAuthorization.Repositories.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
{
    options.Lockout = new LockoutOptions()
    {
        AllowedForNewUsers = true,
        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1),
        MaxFailedAccessAttempts = 5,
    };
})
    .AddEntityFrameworkStores<DatabaseContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Authentication/login";
});
builder.Services.AddScoped<IUserAuthRepo, UserAuthRepository>();

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();

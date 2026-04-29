using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMedical.Data;
using WebMedical.Models.Domain;
using WebMedical.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebMedicalContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WebMedicaldbConn")));
builder.Services.AddIdentity<UserLogin, IdentityRole>()
    .AddEntityFrameworkStores<WebMedicalContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

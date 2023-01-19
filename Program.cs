using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blog_Remastered.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.");

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

app.UseAuthentication();

app.UseMvcWithDefaultRoute();

app.UseStaticFiles();

app.Run();

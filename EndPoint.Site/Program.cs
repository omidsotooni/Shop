
using Microsoft.EntityFrameworkCore;
using Shop.Presentation.Contexts;
using Shop.Application.Interfaces.Contexts;
using Shop.Application.Services.Users.Queries.GetUsers;
using Shop.Application.Services.Users.Queries.GetRoles;
using Shop.Application.Services.Users.Commands.RegisterUser;
using Shop.Application.Services.Users.Commands.RemoveUser;
using Shop.Application.Services.Users.Commands.UserSatusChange;
using Shop.Application.Services.Users.Commands.EditUser;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shop.Application.Services.Users.Commands.UserLogin;
using Shop.Application.Interfaces.FacadPatterns;
using Shop.Application.Services.Products.FacadPattern;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15.0);
});

// AddEntityFrameWorkSqlServer or AddEntityFrameWorkPostgreSQL | AddEntityFrameworkNpgsql();
// Add framework services [UseNpgsql]
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();


//FacadeInject
builder.Services.AddScoped<IProductFacad, ProductFacad>();
builder.Services.AddScoped<IProductFacadForSite, ProductFacadForSite>();


//{ "DefaultConnection": "Host = ; Port = 5432; Username = ; Password = ; Database = Users; SSL Mode = Require"
//"Host=127.0.0.1;Port=5432;Database=store;Username=omid;Password=12345678"
string PostgreSqlConnectionString = @"Host=127.0.0.1;Port=5432;Database=ShopDB;Username=postgres;Password=123456";
builder.Services.AddEntityFrameworkNpgsql().
    AddDbContext<DataBaseContext>(options => options.UseNpgsql(PostgreSqlConnectionString));
//------- this is for DateTime
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(" / Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();

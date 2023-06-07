using IdentityServer.Data;
using IdentityServer.Extensions;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();

var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false; //only dev development 
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddIdentityServer()
    //.AddTestUsers(InMemoryConfig.GetUsers())
    .AddAspNetIdentity<ApplicationUser>()
    .AddDeveloperSigningCredential() //not something we want to use in a production environment
    .AddProfileService<CustomProfileService>()
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = c => c.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddOperationalStore(opt =>
    {
        opt.ConfigureDbContext = o => o.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(migrationAssembly));
    });
//.AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
//.AddInMemoryApiResources(InMemoryConfig.GetApiResources())
//.AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
//.AddTestUsers(InMemoryConfig.GetUsers())
//.AddInMemoryClients(InMemoryConfig.GetClients())
//.AddDeveloperSigningCredential();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapControllers();

app.Run();

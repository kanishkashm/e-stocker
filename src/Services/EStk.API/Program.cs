using EStk.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();

builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer("Bearer", opt =>
   {
       opt.RequireHttpsMetadata = false;
       opt.Authority = "https://localhost:7000"; //to do get this url from config
       opt.Audience = "eStokerApi";
   });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

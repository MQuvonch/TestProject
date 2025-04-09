using Microsoft.EntityFrameworkCore;
using TestProject.Data.Contexts;
using TestProject.Extensions;
using TestProject.Handlers;
using TestProject.RoleSeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefalutConnection")));

builder.Services.AddCustomExtenstion();
builder.Services.AddSwaggerService();
builder.Services.AddJwtService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await SeedData.SeedRolesAsync(app.Services);

app.UseMiddleware<CustomExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

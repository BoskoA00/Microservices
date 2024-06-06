using Microsoft.EntityFrameworkCore;
using UserServer.Data;
using UserServer.DataServices;
using UserServer.Interfaces;
using UserServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUser, userService>();
builder.Services.AddHttpClient<IAdsHttpClient,AdsHttpClient>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("UsersDb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors( o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();

app.Run();

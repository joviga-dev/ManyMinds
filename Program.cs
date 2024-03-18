using ManyMinds.Data;
using ManyMindsApi.Controllers;
using ManyMindsApi.Models;
using ManyMindsApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080", "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ManyMindsApiConnection");

builder.Services.AddDbContext<ManyMindsApiContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddScoped<GerenciadorDeSenhas>();
builder.Services.AddScoped<ISystemLogService, SystemLogService>();


var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using CD.Infra.Models;
using Microsoft.Extensions.Options;
using HostOptions = CD.Infra.Models.HostOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.Configure<Variable>(builder.Configuration.GetSection("Variables"));

builder.Services.AddOptions<HostOptions>().Bind(builder.Configuration.GetSection("HostOptions"));

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/variables", (IOptions<Variable> options) =>
    {
        Console.WriteLine("/Variables called");
        return options.Value;
    })
    .WithName("GetVariables")
    .WithOpenApi();

app.MapGet("/hosts", (IOptions<HostOptions> options) =>
    {
        Console.WriteLine("/Variables called");
        
        return options.Value;
    })
    .WithName("GetHosts")
    .WithOpenApi();

Console.WriteLine("Api starting");

app.Run();

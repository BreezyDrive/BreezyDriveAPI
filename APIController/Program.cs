using APIGateway.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Gateway",
        Version = "v1",
        Description = "API Gateway for Microservices"
    });
});

//Add reverse proxy
// builder.Services.AddReverseProxy()
//     .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddApiGatewayServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Endpoint Swagger của API Gateway
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
        
        // Hiển thị Swagger từ CarService
        //c.SwaggerEndpoint("http://localhost:8280/swagger/v1/swagger.json", "Car Service API");

        c.RoutePrefix = "swagger";  // Truy cập tại http://localhost:5000/swagger
    });}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Endpoint Swagger của API Gateway
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");

        // Hiển thị Swagger từ CarService
        //c.SwaggerEndpoint("http://localhost:8280/swagger/v1/swagger.json", "Car Service API");

        c.RoutePrefix = "swagger";  // Truy cập tại http://localhost:5000/swagger
    });}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

//add reverse proxy
app.MapReverseProxy();
app.Run();
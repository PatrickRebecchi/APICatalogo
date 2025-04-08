using APICatalogo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);

builder.Services.AddControllers(); // Serviço do Controllers para API
builder.Services.AddRouting(options => options.LowercaseUrls = true); // URLs minúsculas

builder.Services.AddOpenApi(); // v1.json

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "weather api"));
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Mapeia os controllers
app.Run();
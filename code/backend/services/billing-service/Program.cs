using billing_service.Data;
using billing_service.Infrastructure;
using billing_service.Middleware;
using billing_service.Service.Interfaces;
using billing_service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IInvoiceService, InvoiceService>();


builder.Services.AddHttpClient<StockClient>(client =>
{
    client.BaseAddress = new Uri("http://stock-service:8080/");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billing Service V1");
    c.RoutePrefix = string.Empty; 
});


app.UseAuthorization();
app.MapControllers();

app.Run();
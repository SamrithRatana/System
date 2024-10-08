using DevExpress.XtraCharts;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Api.Models;
using ServiceMaintenance.Services;

var builder = WebApplication.CreateBuilder(args);
// Configure database context
builder.Services.AddDbContext<ServiceMantenanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
// Add services to the container.

// Configure ServerDbContext
builder.Services.AddDbContext<ServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerDBConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IServiceReportDataRespository, ServiceReportDataRespository>();
builder.Services.AddScoped<ICustomerServiceRespository, CustomerServiceRespository>();
builder.Services.AddScoped<ISparePartServiceRespository, SparePartServiceRespository>();
builder.Services.AddScoped<IPrinterModelServiceRespository, PrinterModelServiceRespository>();
builder.Services.AddScoped<IManufacturerRespository, ManufacturerRespository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

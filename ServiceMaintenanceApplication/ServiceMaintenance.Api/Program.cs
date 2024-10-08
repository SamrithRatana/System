using DevExpress.XtraCharts;
using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Api.Models;

var builder = WebApplication.CreateBuilder(args);
// Configure database context
builder.Services.AddDbContext<ServiceMantenanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ServiceMantenanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped<IServiceReportDataRespository, ServiceReportDataRespository>();
builder.Services.AddScoped<ICustomerServiceRespository, CustomerServiceRespository>();


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

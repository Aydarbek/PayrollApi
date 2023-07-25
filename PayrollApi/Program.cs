using Microsoft.EntityFrameworkCore;
using PayrollApi.Models;
using PayrollApi.Repository.Abstract;
using PayrollApi.Repository.Concrete;
using PayrollApi.Repository.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PayrollContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("mssql")).EnableSensitiveDataLogging()
);

builder.Services.AddTransient<IRepository<Payroll>, PayrollRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller}/{action}/{id?}");
});

app.Run();
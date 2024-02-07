using Invoice.Api.HelperClasses;
using Invoice.Application.Interfaces;
using Invoice.Application.Services;
using Invoice.DataAccess.Data;
using Invoice.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddDbContext<InvoiceDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceDB")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStoreService, StoreService>(); 
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ICalculationService, CalculationService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#region Api Versioning
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
#endregion Api Versioning
builder.Services.AddSwaggerGen();

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

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.Run();

using AV.ForeignExchangeRates.Application.AppServices;
using AV.ForeignExchangeRates.Application.Interfaces;
using AV.ForeignExchangeRates.Application.Validators;
using AV.ForeignExchangeRates.Domain.Configuration;
using AV.ForeignExchangeRates.Domain.Interfaces.Repositories;
using AV.ForeignExchangeRates.Domain.Interfaces.Services;
using AV.ForeignExchangeRates.Domain.Interfaces;
using AV.ForeignExchangeRates.Infra.Configuration;
using AV.ForeignExchangeRates.Infra.Repositories;
using AV.ForeignExchangeRates.Services.Http;
using AV.ForeignExchangeRates.Services.Queue;
using AV.ForeignExchangeRates.Services.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AV.ForeignExchangeRates.API.Middlewares;


//var builder = WebApplication.CreateBuilder(args)
//    .UseStartup<Startup>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining(typeof(ExchangeRateRequestValidator));

//Ifactory
builder.Services.AddHttpClient();


//app services
builder.Services.AddScoped<ICurrencyAppService, CurrencyAppService>();

//services
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencyExchangeRateService, CurrencyExchangeRateService>();

//repositories
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();

//other
builder.Services.AddTransient<IHttpHelper, HttpHelper>();
builder.Services.AddTransient<IQueueService, QueueService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(ErrorMiddleware));

app.MapControllers();

app.Run();

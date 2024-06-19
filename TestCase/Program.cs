using Serilog;
using TestCase.Logging;
using TestCase.Services;
using TestCase.Core.Interfaces;
using TestCase.Core.Configurations;
using TestCase.Infra.DataProviders;
using TestCase.Services.Interfaces;
using TestCase.Middlewares;
using static TestCase.Logging.HttpContextEnricher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient();
builder.Services.Configure<AlphaVantageApiConfiguration>(builder.Configuration.GetSection("AlphaVantageApi"));
builder.Services.AddSingleton<WebSocketHandler>();
builder.Services.AddSingleton<IAlphaVantageProvider, AlphaVantageProvider>();
builder.Services.AddSingleton<IPriceService, PriceService>();
builder.Services.AddSingleton<IRequestBuilder, RequestBuilder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.MapControllers();
app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseWebSockets();
app.UseMiddleware<CustomWebSocketMiddleware>();

app.Run();


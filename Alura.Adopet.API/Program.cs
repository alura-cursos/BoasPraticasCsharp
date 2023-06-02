using Alura.Adopet.API;
using Alura.Adopet.API.Dados.Context;
using Alura.Adopet.API.Service;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);// Criando uma aplicação Web.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
ConfigureLogRequestSerilogExtension.AddSerialogAPI(builder);

//DI
builder.Services.AddControllers().AddNewtonsoftJson(options =>
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<IEventoService,EventoService>()               
                .AddDbContext<DataBaseContext>(opt => {
                    opt.UseInMemoryDatabase("AdopetDB");
                    opt.UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder.AddSerilog();
                    }));
                });

//Habilitando o swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
//{
//    builder.Services.AddHttpLogging(opt =>
//    {
//        opt.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders|
//                            HttpLoggingFields.ResponsePropertiesAndHeaders|
//                            HttpLoggingFields.ResponseBody| 
//                            HttpLoggingFields.ResponseBody;
//    });

//}

//Adicionando serviços.
var serviceProvider = builder.Services.BuildServiceProvider();
var eventoService = serviceProvider.GetService<IEventoService>();

var app = builder.Build();
eventoService.GenerateFakeDate();

// Ativando o Swagger
app.UseSwagger();


// Ativando a interface Swagger
app.UseSwaggerUI(
    c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
        c.RoutePrefix = string.Empty;
    }
);

app.MapControllers();
// Roda a aplicação
app.Run();

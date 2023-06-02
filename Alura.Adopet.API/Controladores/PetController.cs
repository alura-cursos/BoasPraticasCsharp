using Alura.Adopet.API.Dados.Context;
using Alura.Adopet.API.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.IO;

namespace Alura.Adopet.API.Controladores
{
    [ApiController]
    [Route("/pet/")]
    public class PetController:ControllerBase
    {     

        [HttpGet]
        [Route("list")]
        public async Task<IResult> ListaDePet()
        {
            //
            var _logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File("AdoPetAPI-Actions-logs.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();


            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase("AdopetDB")
            .Options;
            try
            {
                DataBaseContext _context = new(options);
                var listaDePet = await _context.Pets.Include(x => x.Proprietario).ToListAsync();
                _logger.Information("Listagem gerada com sucesso!");
                return Results.Ok(listaDePet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex,"Ocorreu uma Execeção:", ex.Message);
            }
            return Results.Ok();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IResult> CadatrarPet([FromBody]Pet pet)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            string _data = DateTime.Now.ToString("yyyy-MM-dd_HH");
            string? path = configuration["LoggerBasePath"];
            string? template = configuration["LoggerFileTemplate"];
            string filename = $@"{path}\{_data}.adopet.log";

            Log.Logger = new LoggerConfiguration()
                          .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                          .Enrich.FromLogContext()
                          .WriteTo.Console()
                          .WriteTo.File(filename, outputTemplate: template)
                          .CreateLogger();


            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase("AdopetDB")
            .Options;

            try
            {
                DataBaseContext _context = new(options);
                await _context.Pets.AddAsync(pet);
                _context.SaveChanges();
                Log.Information("Pet cadastrado com sucesso!");
               
                return Results.Ok("Pet cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Ocorreu uma Execeção:", ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }


            return Results.Ok();

        }
    }
}

using Serilog.Events;
using Serilog;
using Serilog.Filters;

namespace Alura.Adopet.API
{
    public static class ConfigureLogRequestSerilogExtension
    {
        public static void AddSerialogAPI(this WebApplicationBuilder builder)
        {
            string _data = DateTime.Now.ToString("yyyy-MM-dd_HH");
            string? path = builder.Configuration.GetSection("LoggerBasePath").Value;
            string? template = builder.Configuration.GetSection("LoggerFileTemplate").Value;
            string filename = $@"{path}\{_data}.adopet.log";
            Log.Logger = new LoggerConfiguration()
                          .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                          .Enrich.FromLogContext()                    
                          .WriteTo.Console()
                          .WriteTo.File(filename, outputTemplate: template)
                          .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);
        }
    }
}

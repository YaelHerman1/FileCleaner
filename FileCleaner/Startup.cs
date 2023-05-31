using FileCleaner.Controllers;
using FileCleaner.Implementation;
using FileCleaner.Interfaces;
using Microsoft.OpenApi.Models;

namespace FileCleaner
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(FileSanitizerController).Assembly);
            services.AddControllers();

            services.AddSingleton<IFileFormatHandler<ABCFormatHandler>, ABCFormatHandler>();
            services.AddSingleton<IFileFormatHandler<EFGFormatHandler>, EFGFormatHandler>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "File Sanitizer", Version = "v1" });


            });


        }
        public void Configure(IApplicationBuilder app)
        {

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }
            );
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "File Sanitizer");
            });

        }
    }
}

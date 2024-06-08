﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiAutores2.Controllers;
using WebApiAutores2.Servicios;

namespace WebApiAutores2
{
    public class StartUp
    {
        //Código de ejemplo
        //public StartUp(IConfiguration configuration)
        //{
        //    var autoresController = new AutoresController(new ApplicationDbContext(null),
        //        new ServicioA(new Logger()));
        //    Configuration = configuration;
        //}

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("Connection2")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

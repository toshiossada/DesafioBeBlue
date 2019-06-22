using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeBlue.Desafio.Entities.Spotify;
using BeBlue.Desafio.Entities.Spotify.Interfaces;
using BeBlue.Desafio.lib.repository;
using BeBlue.Desafio.lib.repository.Interface;
using BeBlue.Desafio.lib.service;
using BeBlue.Desafio.lib.service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace BeBlue.Desafio.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            var connString = Configuration.GetConnectionString ("BeBlue");
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddTransient<IClient, SpotifyWebClient> ();
            services.AddTransient<ISpotifyService, SpotifyService> ();
            services.AddTransient<ITrackService, TrackService> ();
            services.AddTransient<ISaleService, SaleService> ();
            

            services.AddTransient<ITrackRepository> (f => new TrackRepository (connString));
            services.AddTransient<IGenreRepository> (f => new GenreReposiotory (connString));
            services.AddTransient<ISaleItemRepository> (f => new SaleItemRepository (connString));
            services.AddTransient<ISaleRepository> (f => new SaleRepository (connString));

            //Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", null);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseSwagger ();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}
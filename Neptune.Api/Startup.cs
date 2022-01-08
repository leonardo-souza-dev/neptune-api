using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neptune.Application;
using Neptune.Infra;

namespace Neptune.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader();
                        builder.WithOrigins("https://127.0.0.1:5001").AllowAnyMethod().AllowAnyHeader();
                        builder.WithOrigins("http://localhost:5000").AllowAnyMethod().AllowAnyHeader();
                        builder.WithOrigins("http://127.0.0.1:5000").AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddSingleton<ITransacaoService, TransacaoService>();
            services.AddSingleton<IContaService, ContaService>();

            services.AddSingleton<ITransacaoRepository, TransacaoRepository>();
            services.AddSingleton<IContaRepository, ContaRepository>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

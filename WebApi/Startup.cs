using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Musili.WebApi.Services.Db;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Services;
using Musili.WebApi.Services.Grabbers;
using Musili.WebApi.Services.Grabbers.Yandex;
using System.Net.Http;
using Microsoft.AspNetCore.HttpOverrides;

namespace Musili.WebApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddHostedService<TracksUpdaterBackgroundService>();

            services.AddHttpClient<IYandexMusicClient, YandexMusicClient>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { MaxConnectionsPerServer = 2 });

            // grabbers for each service
            services.AddSingleton<YandexTracksGrabber>();

            // factory for grabber for each music service
            services.AddSingleton<Func<TracksSourceService, ITracksGrabber>>(serviceProvider => {
                return source => {
                    switch (source) {
                        case TracksSourceService.Yandex:
                            return serviceProvider.GetService<YandexTracksGrabber>();
                        default:
                            throw new NotImplementedException();
                    }
                };
            });

            // common tracks grabber
            services.AddSingleton<ICommonTracksGrabber, TracksGrabber>();

            services.AddDbContext<AppDbContext>(opts => {
                opts.UseNpgsql(Configuration.GetConnectionString("MusiliDatabase"));
            });
            services.AddScoped<ITracksSourcesRepository, TracksSourceRepository>();
            services.AddScoped<ITracksRepository, TracksRepository>();
            services.AddScoped<ITracksProvider, TracksProvider>();
            services.AddScoped<ITracksUpdater, TracksUpdater>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc();
        }
    }
}

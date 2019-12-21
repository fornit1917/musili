﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Polly.Extensions.Http;
using Polly;
using Polly.Retry;
using FluentMigrator.Runner;
using Musili.WebApi.Services.Db;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Services;
using Musili.WebApi.Services.Grabbers;
using Musili.WebApi.Services.Grabbers.Yandex;
using Musili.WebApi.Migrations;
using FluentMigrator.Runner.VersionTableInfo;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Musili.WebApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var appConfig = new AppConfig();
            Configuration.GetSection("AppConfig").Bind(appConfig);
            services.AddSingleton(appConfig);

            services.AddHostedService<TracksUpdaterBackgroundService>();

            services.AddHttpClient<IYandexMusicClient, YandexMusicClient>()
                .ConfigureHttpClient(YandexMusicClient.ConfigureHttpClient)
                .AddPolicyHandler(GetHttpRetryPolicy());

            services.AddSingleton<ISemaphores, Semaphores>();
            services.AddSingleton<ITracksRequestsRating, TracksRequestsRating>();

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

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder => {
                    builder
                        .AddPostgres()
                        .WithGlobalConnectionString("MusiliDatabase")
                        .WithVersionTable(new VersionTable())
                        .ScanIn(typeof(VersionTable).Assembly).For.Migrations();
                })
                .AddLogging(lb => lb.AddNLog());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }


        private static RetryPolicy<System.Net.Http.HttpResponseMessage> GetHttpRetryPolicy() {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(1));
        }
    }
}

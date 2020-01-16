using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Musili.ApiApp {
    public class Program {
        public static void Main(string[] args) {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try {
                logger.Info("Init application...");
                var host = BuildWebHost(args);
                
                using (var scope = host.Services.CreateScope()) {
                    IMigrationRunner migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    migrationRunner.MigrateUp();
                }

                host.Run();
            } catch (Exception ex) {
                logger.Error(ex, "Stopped program because of exception");
            } finally {
                LogManager.Shutdown();
            }
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                })
                .UseNLog()
                .Build();
    }
}

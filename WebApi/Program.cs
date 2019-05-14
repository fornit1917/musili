using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Musili.WebApi {
    public class Program {
        public static void Main(string[] args) {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try {
                logger.Info("Init application...");
                BuildWebHost(args).Run();
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

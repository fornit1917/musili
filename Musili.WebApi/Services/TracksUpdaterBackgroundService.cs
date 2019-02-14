using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musili.WebApi.Interfaces;

namespace Musili.WebApi.Services
{
    public class TracksUpdaterBackgroundService : IHostedService, IDisposable
    {
        private IServiceProvider services;
        private Timer timer;
        private bool started = false;

        public TracksUpdaterBackgroundService(IServiceProvider services) {
            this.services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(60*30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWorkAsync(object state) {
            if (started) {
                return;
            }

            started = true;
            using (var scope = services.CreateScope()) {
                ITracksUpdater tracksUpdater = scope.ServiceProvider.GetRequiredService<ITracksUpdater>();
                try {
                    await tracksUpdater.RemoveOldTracksAsync();
                    await tracksUpdater.LoadNewTracksForAllCriteriasAsync();
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                } finally {
                    started = false;
                }
            }   
        }

        public void Dispose() {
            timer?.Dispose();
        }
    }
}
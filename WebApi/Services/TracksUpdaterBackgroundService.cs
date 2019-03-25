using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musili.WebApi.Interfaces;

namespace Musili.WebApi.Services {
    public class TracksUpdaterBackgroundService : IHostedService, IDisposable {
        private IServiceProvider _services;
        private Timer _timer;
        private bool _started = false;
        private AppConfig _appConfig;

        public TracksUpdaterBackgroundService(IServiceProvider services, AppConfig appConfig) {
            _services = services;
            _appConfig = appConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            if (_appConfig.DeleteOldTracksInBackground || _appConfig.LoadNewTracksInBackground) {
                _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(_appConfig.TracksUpdaterTimeoutSeconds));
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWorkAsync(object state) {
            if (_started) {
                return;
            }

            _started = true;
            using (var scope = _services.CreateScope()) {
                ITracksUpdater tracksUpdater = scope.ServiceProvider.GetRequiredService<ITracksUpdater>();
                try {
                    if (_appConfig.DeleteOldTracksInBackground) {
                        await tracksUpdater.RemoveOldTracksAsync();
                    }
                    if (_appConfig.LoadNewTracksInBackground) {
                        await tracksUpdater.LoadNewTracksForAllCriteriasAsync(_appConfig.TracksUpdaterMaxDurationSeconds);
                    }
                } catch (Exception e) {
                    // todo: add logger
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                } finally {
                    _started = false;
                }
            }
        }

        public void Dispose() {
            _timer?.Dispose();
        }
    }
}
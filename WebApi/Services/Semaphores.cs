using System;
using System.Collections.Generic;
using System.Threading;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services {
    public class Semaphores : ISemaphores {
        private Dictionary<TracksSourceService, SemaphoreSlim> _semaphores;

        public Semaphores(AppConfig appConfig) {
            _semaphores = new Dictionary<TracksSourceService, SemaphoreSlim>();
            foreach(TracksSourceService service in Enum.GetValues(typeof(TracksSourceService))) {
                int maxConnections = GetMaxConnections(appConfig, service);
                var semaphore = new SemaphoreSlim(maxConnections); // todo: get from config
                _semaphores.Add(service, semaphore);
            }
        }

        public SemaphoreSlim GetSemaphoreForService(TracksSourceService service) {
            return _semaphores[service];
        }

        private static int GetMaxConnections(AppConfig appConfig, TracksSourceService service) {
            switch (service) {
                case TracksSourceService.Yandex:
                    return appConfig.MaxConnectionsYandex;
                default:
                    return 3;
            }
        }
    }
}
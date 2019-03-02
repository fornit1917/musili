using System;
using System.Collections.Generic;
using System.Threading;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services {
    public class Semaphores : ISemaphores {
        private Dictionary<TracksSourceService, SemaphoreSlim> _semaphores;

        public Semaphores() {
            _semaphores = new Dictionary<TracksSourceService, SemaphoreSlim>();
            foreach(TracksSourceService service in Enum.GetValues(typeof(TracksSourceService))) {
                var semaphore = new SemaphoreSlim(3); // todo: get from config
                _semaphores.Add(service, semaphore);
            }
        }

        public SemaphoreSlim GetSemaphoreForService(TracksSourceService service) {
            return _semaphores[service];
        }
    }
}
using System.Threading;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.Tests.Mocks {
    class SemaphoresMock : ISemaphores
    {
        private SemaphoreSlim _semaphore = new SemaphoreSlim(100);

        public SemaphoreSlim GetSemaphoreForService(TracksSourceService service)
        {
            return _semaphore;
        }
    }
}
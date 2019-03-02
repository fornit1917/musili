using Musili.WebApi.Models;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Services;
using Musili.WebApi.Services.Grabbers;
using Musili.WebApi.Services.Grabbers.Yandex;
using System;

namespace Musili.Tests.Mocks
{
    public class GrabbersMocksFactory
    {
        private static ISemaphores _semaphores = new SemaphoresMock();

        public static ICommonTracksGrabber CreateMockedGrabber() {
            Func<TracksSourceService, ITracksGrabber> grabbersProvider = tracksSourceService => {
                switch (tracksSourceService) {
                    case TracksSourceService.Yandex:
                        return new YandexTracksGrabber(new YandexMusicClientMock());
                    default:
                        throw new NotImplementedException();
                }
            };

            return new TracksGrabber(grabbersProvider, _semaphores);
        }
    }
}
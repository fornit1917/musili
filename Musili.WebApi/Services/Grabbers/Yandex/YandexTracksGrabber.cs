using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexTracksGrabber : ITracksGrabber
    {
        private IYandexMusicClient client;

        public YandexTracksGrabber(IYandexMusicClient client) {
            this.client = client;
        }

        public Task<Track[]> GrabRandomTracksAsync(TracksSource tracksSource) {
            throw new System.NotImplementedException();
        }
    }
}
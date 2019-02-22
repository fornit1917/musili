using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Utils;

namespace Musili.WebApi.Services.Grabbers.Yandex {
    public class YandexTracksGrabber : ITracksGrabber {
        private IYandexMusicClient _client;
        private YandexUrlParser _urlParser = new YandexUrlParser();

        public TimeSpan LinkLifeTime { get; } = new TimeSpan(0, 50, 0);

        public YandexTracksGrabber(IYandexMusicClient client) {
            _client = client;
        }

        public Task<List<Track>> GrabRandomTracksAsync(TracksSource tracksSource) {
            switch (tracksSource.SourceType) {
                case TracksSourceType.YandexTracks:
                    return GrabTracksFromList(tracksSource.Value);
                default:
                    throw new NotSupportedException($"Source {tracksSource.SourceType.ToString()} is not supported by YandexTracksGrabber.");
            }
        }

        private async Task<List<Track>> GrabTracksFromList(string url) {
            YandexPlaylistParams playlistParams = _urlParser.ParsePlaylistUrl(url);
            List<string> ids = null;
            int count = 1;
            switch (playlistParams.type) {
                case YandexPlaylistType.UserPlaylist:
                    ids = await _client.GetTracksIdsByUserPlaylistAsync(playlistParams.userId, playlistParams.playlistId);
                    count = 2;
                    break;
                case YandexPlaylistType.Artist:
                    ids = await _client.GetTracksIdsByArtistAsync(playlistParams.playlistId);
                    break;
                case YandexPlaylistType.Album:
                    ids = await _client.GetTracksIdsByAlbumAsync(playlistParams.playlistId);
                    break;
            }

            ids = RandomUtils.GetRandomItems(ids, count);
            return await _client.GetTracksByIdsAsync(ids);
        }
    }
}
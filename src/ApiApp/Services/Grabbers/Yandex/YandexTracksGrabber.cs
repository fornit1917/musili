using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Musili.ApiApp.Interfaces;
using Musili.ApiApp.Models;
using Musili.ApiApp.Utils;

namespace Musili.ApiApp.Services.Grabbers.Yandex {
    public class YandexTracksGrabber : ITracksGrabber {
        private IYandexMusicClient _client;
        private ILogger<YandexTracksGrabber> _logger;
        private YandexUrlParser _urlParser = new YandexUrlParser();

        public TimeSpan LinkLifeTime { get; } = new TimeSpan(0, 50, 0);

        public YandexTracksGrabber(IYandexMusicClient client, ILogger<YandexTracksGrabber> logger) {
            _client = client;
            _logger = logger;
        }

        public Task<List<Track>> GrabRandomTracks(TracksSource tracksSource) {
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

            _logger.LogInformation("Load tracks from {0}", url);
            switch (playlistParams.type) {
                case YandexPlaylistType.UserPlaylist:
                    ids = await _client.GetTracksIdsByUserPlaylist(playlistParams.userId, playlistParams.playlistId);
                    count = 2;
                    break;
                case YandexPlaylistType.Artist:
                    ids = await _client.GetTracksIdsByArtist(playlistParams.playlistId);
                    break;
                case YandexPlaylistType.Album:
                    ids = await _client.GetTracksIdsByAlbum(playlistParams.playlistId);
                    break;
            }
            _logger.LogInformation("Tracks ids loaded, total: {0}", ids == null ? 0 : ids.Count);

            ids = RandomUtils.GetRandomItems(ids, count);
            return await _client.GetTracksByIds(ids);
        }
    }
}
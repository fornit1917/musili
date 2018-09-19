using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexTracksGrabber : ITracksGrabber
    {
        private IYandexMusicClient client;
        private YandexUrlParser urlParser = new YandexUrlParser();

        public YandexTracksGrabber(IYandexMusicClient client) {
            this.client = client;
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
            YandexPlaylistParams playlistParams = urlParser.ParsePlaylistUrl(url);
            List<int> ids = null;
            switch (playlistParams.type) {
                case YandexPlaylistType.UserPlaylist:
                    ids = await client.GetTracksIdsByUserPlaylistAsync(playlistParams.userId, playlistParams.playlistId);
                    break;
                case YandexPlaylistType.Artist:
                    ids = await client.GetTracksIdsByArtistAsync(playlistParams.playlistId);
                    break;
                case YandexPlaylistType.Album:
                    ids = await client.GetTracksIdsByAlbumAsync(playlistParams.playlistId);
                    break;        
            }

            // todo: get random subset from ids

            return await client.GetTracksByIdsAsync(ids);
        }
    }
}
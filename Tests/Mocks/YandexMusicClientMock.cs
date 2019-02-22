using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Services.Grabbers.Yandex;

namespace Musili.Tests.Mocks
{
    public class YandexMusicClientMock : IYandexMusicClient
    {
        public Task<List<Track>> GetTracksByIdsAsync(List<string> ids)
        {
            List<Track> tracks = ids.Select(id => new Track() {
                Artist = "Artist",
                Title = $"Track{id}",
                Url = $"http://url-to-{id}",
            }).ToList();
            return Task.FromResult(tracks);
        }

        public Task<List<string>> GetTracksIdsByAlbumAsync(string albumId)
        {
            return Task.FromResult(GetTracksIds(albumId));
        }

        public Task<List<string>> GetTracksIdsByArtistAsync(string artistId)
        {
            return Task.FromResult(GetTracksIds(artistId));
        }

        public Task<List<string>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId)
        {
            return Task.FromResult(GetTracksIds(playlistId));
        }

        private List<string> GetTracksIds(string id) {
            int start = Int32.Parse(id);
            return new List<string> {
                (start + 1).ToString(),
                (start + 2).ToString(),
                (start + 3).ToString(),
                (start + 4).ToString(),
                (start + 5).ToString(),
            };
        }
    }
}
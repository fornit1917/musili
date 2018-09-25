using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexMusicClient : IYandexMusicClient
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
            return Task.FromResult(new List<string>() { "1", "2", "3", "4", "5" });
        }

        public Task<List<string>> GetTracksIdsByArtistAsync(string artistId)
        {
            return Task.FromResult(new List<string>() { "1", "2", "3", "4", "5" });
        }

        public Task<List<string>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId)
        {
            return Task.FromResult(new List<string>() { "1", "2", "3", "4", "5" });
        }
    }
}
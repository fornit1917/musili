using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexMusicClient : IYandexMusicClient
    {
        public Task<List<Track>> GetTracksByIdsAsync(List<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<int>> GetTracksIdsByAlbumAsync(string albumId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<int>> GetTracksIdsByArtistAsync(string artistId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<int>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId)
        {
            throw new System.NotImplementedException();
        }
    }
}
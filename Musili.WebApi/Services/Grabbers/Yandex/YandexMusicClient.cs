using System.Threading.Tasks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexMusicClient : IYandexMusicClient
    {
        public Task<Track[]> GetTracksByIdsAsync(int[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<int[]> GetTracksIdsByAlbumAsync(string albumId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int[]> GetTracksIdsByArtistAsync(string artistId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int[]> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId)
        {
            throw new System.NotImplementedException();
        }
    }
}
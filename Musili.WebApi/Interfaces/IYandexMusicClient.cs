using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces 
{
    public interface IYandexMusicClient
    {
        Task<List<int>> GetTracksIdsByAlbumAsync(string albumId);
        Task<List<int>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId);
        Task<List<int>> GetTracksIdsByArtistAsync(string artistId);
        Task<List<Track>> GetTracksByIdsAsync(List<int> ids);
    }
}


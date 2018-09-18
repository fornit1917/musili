using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces 
{
    public interface IYandexMusicClient
    {
        Task<int[]> GetTracksIdsByAlbumAsync(string albumId);
        Task<int[]> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId);
        Task<int[]> GetTracksIdsByArtistAsync(string artistId);
        Task<Track[]> GetTracksByIdsAsync(int[] ids);
    }
}


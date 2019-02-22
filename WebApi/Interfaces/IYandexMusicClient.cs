using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface IYandexMusicClient {
        Task<List<string>> GetTracksIdsByAlbumAsync(string albumId);
        Task<List<string>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId);
        Task<List<string>> GetTracksIdsByArtistAsync(string artistId);
        Task<List<Track>> GetTracksByIdsAsync(List<string> ids);
    }
}


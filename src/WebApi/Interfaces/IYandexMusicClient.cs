using System.Collections.Generic;
using System.Threading.Tasks;
using Musili.WebApi.Models;

namespace Musili.WebApi.Interfaces {
    public interface IYandexMusicClient {
        Task<List<string>> GetTracksIdsByAlbum(string albumId);
        Task<List<string>> GetTracksIdsByUserPlaylist(string userId, string playlistId);
        Task<List<string>> GetTracksIdsByArtist(string artistId);
        Task<List<Track>> GetTracksByIds(List<string> ids);
    }
}


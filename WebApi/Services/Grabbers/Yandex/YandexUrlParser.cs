using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Musili.WebApi.Services.Grabbers.Yandex {
    public class YandexUrlParser {
        private static Regex _regexArtistUrl = new Regex(@"https://music.yandex.ru/artist/(\d+)");
        private static Regex _regexAlbumUrl = new Regex(@"https://music.yandex.ru/album/(\d+)");
        private static Regex _regexPlaylistUrl = new Regex(@"https://music.yandex.ru/users/([\d\w\.\-]+)/playlists/(\d+)");

        public YandexPlaylistParams ParsePlaylistUrl(string url) {
            Match match = _regexPlaylistUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.UserPlaylist, match.Groups[1].Value, match.Groups[2].Value);
            }

            match = _regexArtistUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.Artist, match.Groups[1].Value);
            }

            match = _regexAlbumUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.Album, match.Groups[1].Value);
            }

            throw new Exception($"Incorrect or not supported yandex playlist URL: {url}");
        }
    }
}
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexUrlParser
    {
        private static Regex regexArtistUrl = new Regex(@"https://music.yandex.ru/artist/(\d+)");
        private static Regex regexAlbumUrl = new Regex(@"https://music.yandex.ru/album/(\d+)");
        private static Regex regexPlaylistUrl = new Regex(@"https://music.yandex.ru/users/([\d\w\.\-]+)/playlists/(\d+)");

        public YandexPlaylistParams ParsePlaylistUrl(string url) {
            Match match = regexPlaylistUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.UserPlaylist, match.Groups[1].Value, match.Groups[2].Value);
            }

            match = regexArtistUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.Artist, match.Groups[1].Value);
            }

            match = regexAlbumUrl.Match(url);
            if (match.Success) {
                return new YandexPlaylistParams(YandexPlaylistType.Album, match.Groups[1].Value);
            }

            throw new Exception($"Incorrect or not supported yandex playlist URL: {url}");
        }
    }
}
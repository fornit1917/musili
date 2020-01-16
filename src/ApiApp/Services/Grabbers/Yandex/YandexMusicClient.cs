using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Musili.ApiApp.Interfaces;
using Musili.ApiApp.Models;
using Musili.ApiApp.Utils;

namespace Musili.ApiApp.Services.Grabbers.Yandex {
    public class YandexMusicClient : IYandexMusicClient {
        private static string SALT = "XGRlBW9FXlekgbPrRHuSiA";

        private readonly HttpClient _httpClient;

        public YandexMusicClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Track>> GetTracksByIds(List<string> ids) {
            string query = String.Join(",", ids);
            string url = $"https://music.yandex.ru/api/v2.1/handlers/tracks?tracks={query}";
            JToken data = await RequestJson(url);
            List<Track> tracks = (
                from t in data
                let version = t["version"]
                select new Track() {
                    OriginalId = (string)t["id"],
                    Title = version == null ? (string)t["title"] : $"{t["title"]} ({version})",
                    Artist = (string)t["artists"][0]["name"],
                    Duration = t["durationMs"] != null ? Convert.ToInt32(t["durationMs"]) / 1000 : 0,
                }
            ).ToList();

            int i = 0;
            foreach (Track track in tracks) {
                track.Url = await GetTrackUrl(track.OriginalId);
                if (tracks.Count > 1 && i < tracks.Count - 1) {
                    await Task.Delay(1000);
                }
                i++;
            }

            return tracks;
        }

        public async Task<List<string>> GetTracksIdsByAlbum(string albumId) {
            string url = $"https://music.yandex.ru/handlers/album.jsx?album={albumId}&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.3792734650109968";
            JToken data = await RequestJson(url);
            return (
                from volume in data["volumes"]
                from track in volume
                select (string)track["id"]
            ).ToList();
        }

        public async Task<List<string>> GetTracksIdsByArtist(string artistId) {
            string url = $"https://music.yandex.ru/handlers/artist.jsx?artist={artistId}&what=tracks&sort=&dir=&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.329131147428392";
            JToken data = await RequestJson(url);
            return GetTrackIdsFromJToken(data["trackIds"]);
        }

        public async Task<List<string>> GetTracksIdsByUserPlaylist(string userId, string playlistId) {
            string url = $"https://music.yandex.ru/handlers/playlist.jsx?owner={userId}&kinds={playlistId}&light=true&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.5872919215747372";
            JToken data = await RequestJson(url);
            return GetTrackIdsFromJToken(data["playlist"]["trackIds"]);
        }

        private async Task<string> GetTrackUrl(string trackId) {
            string trackInfoUrl = $"https://music.yandex.ru/api/v2.1/handlers/track/{trackId}/track/download/m?hq=1";
            JToken trackInfoData = await RequestJson(trackInfoUrl);

            string trackSrcUrl = (string)trackInfoData["src"] + "&format=json";
            JToken trackSrcData = await RequestJson(trackSrcUrl);

            string path = (string)trackSrcData["path"];
            string s = (string)trackSrcData["s"];
            string hash = CryptoUtils.GetMd5Hash($"{SALT}{path.Substring(1)}{s}");
            string host = (string)trackSrcData["host"];
            string ts = (string)trackSrcData["ts"];

            return $"https://{host}/get-mp3/{hash}/{ts}{path}";
        }

        private async Task<JToken> RequestJson(string url) {
            HttpResponseMessage resp = await _httpClient.GetAsync(url);
            string s = await resp.Content.ReadAsStringAsync();
            return JToken.Parse(s);
        }

        private List<string> GetTrackIdsFromJToken(JToken data) {
            return (from tId in data select ((string)tId).Split(':')[0]).ToList();
        }

        public static void ConfigureHttpClient(HttpClient httpClient) {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("referer", "https://music.yandex.ru/");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.8,ru;q=0.6");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "identity");
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            httpClient.DefaultRequestHeaders.Add("DNT", "1");
            httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            httpClient.DefaultRequestHeaders.Add("Cookie", "_ym_uid=1502906756176177940; mda=0; _ym_isad=2; yandexuid=5733988641502906756; yp=1818266756.yrts.1502906756; _ym_visorc_10630330=w; spravka=dD0xNTAyOTA2NzYxO2k9NDYuMTY0LjE5OC4xNzU7dT0xNTAyOTA2NzYxNTgzNzUzMTAzO2g9ZDQwZmZlNmFkYjQ1NWJhNjNhMWE5MmM3NTFjYTg5MWY=; device_id=\"bd83f65309fad0c369c4a221b8faa9991ee873936\"; _ym_visorc_1028356=b; i=UpYyqKGVjzBkESSuqqttj9x4BrOVszvLI2iE/DLIR1QNZU3zHshVTgnApfM5J8FdYT3DFTK3lftra/BrPQjqlxgsU0U=");
            httpClient.DefaultRequestHeaders.Add("X-Retpath-Y", "https%3A%2F%2Fmusic.yandex.ru");
        }
    }
}
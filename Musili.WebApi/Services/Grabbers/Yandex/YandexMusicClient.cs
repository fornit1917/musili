using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;
using Musili.WebApi.Utils;



namespace Musili.WebApi.Services.Grabbers.Yandex
{
    public class YandexMusicClient : IYandexMusicClient
    {
        private static string SALT = "XGRlBW9FXlekgbPrRHuSiA";

        public async Task<List<Track>> GetTracksByIdsAsync(List<string> ids) {
            string query = String.Join(",", ids);
            String url = $"https://music.yandex.ru/api/v2.1/handlers/tracks?tracks={query}";
            JToken data = await RequestJsonAsync(url);
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
                string trackInfoUrl = $"https://music.yandex.ru/api/v2.1/handlers/track/{track.OriginalId}/track/download/m?hq=1";
                JToken trackInfoData = await RequestJsonAsync(trackInfoUrl);

                string trackSrcUrl = (string)trackInfoData["src"] + "&format=json";
                JToken trackSrcData = await RequestJsonAsync(trackSrcUrl);

                string path = (string)trackSrcData["path"];
                string s = (string)trackSrcData["s"];
                string hash = CryptoUtils.GetMd5Hash($"{SALT}{path.Substring(1)}{s}");
                string host = (string)trackSrcData["host"];
                string ts = (string)trackSrcData["ts"];

                track.Url = $"https://{host}/get-mp3/{hash}/{ts}{path}";

                if (tracks.Count > 1 && i < tracks.Count - 1) {
                    await Task.Delay(1000);
                }

                i++;
            }

            return tracks;
        }

        public async Task<List<string>> GetTracksIdsByAlbumAsync(string albumId) {
            string url = $"https://music.yandex.ru/handlers/album.jsx?album={albumId}&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.3792734650109968";
            JToken data = await RequestJsonAsync(url);
            return (
                from volume in data["volumes"]
                from track in volume
                select (string)track["id"]
            ).ToList();
        }

        public async Task<List<string>> GetTracksIdsByArtistAsync(string artistId) {
            string url = $"https://music.yandex.ru/handlers/artist.jsx?artist={artistId}&what=tracks&sort=&dir=&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.329131147428392";
            JToken data = await RequestJsonAsync(url);
            return GetTrackIdsFromJToken(data["trackIds"]);
        }

        public async Task<List<string>> GetTracksIdsByUserPlaylistAsync(string userId, string playlistId) {
            string url = $"https://music.yandex.ru/handlers/playlist.jsx?owner={userId}&kinds={playlistId}&light=true&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.5872919215747372";
            JToken data = await RequestJsonAsync(url);
            return GetTrackIdsFromJToken(data["playlist"]["trackIds"]);
        }


        private HttpWebRequest CreateRequest(string url) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers.Clear();
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            req.Method = "GET";
            req.Referer = "https://music.yandex.ru/";

            req.Headers.Add("Accept-Language", "en-US,en;q=0.8,ru;q=0.6");
            req.Headers.Add("Accept-Encoding", "identity");
            req.Headers.Add("Cache-Control", "max-age=0");
            req.Headers.Add("DNT", "1");
            req.Headers.Add("Upgrade-Insecure-Requests", "1");
            req.Headers.Add("Cookie", "_ym_uid=1502906756176177940; mda=0; _ym_isad=2; yandexuid=5733988641502906756; yp=1818266756.yrts.1502906756; _ym_visorc_10630330=w; spravka=dD0xNTAyOTA2NzYxO2k9NDYuMTY0LjE5OC4xNzU7dT0xNTAyOTA2NzYxNTgzNzUzMTAzO2g9ZDQwZmZlNmFkYjQ1NWJhNjNhMWE5MmM3NTFjYTg5MWY=; device_id=\"bd83f65309fad0c369c4a221b8faa9991ee873936\"; _ym_visorc_1028356=b; i=UpYyqKGVjzBkESSuqqttj9x4BrOVszvLI2iE/DLIR1QNZU3zHshVTgnApfM5J8FdYT3DFTK3lftra/BrPQjqlxgsU0U=");
            req.Headers.Add("X-Retpath-Y", "https%3A%2F%2Fmusic.yandex.ru");

            return req;
        }

        private async Task<JToken> RequestJsonAsync(string url) {
            HttpWebRequest req = CreateRequest(url);
            WebResponse resp = await req.GetResponseAsync();
            return await ResponseToJTokenAsync(resp);
        }

        private async Task<JToken> ResponseToJTokenAsync(WebResponse resp) {
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string s = await sr.ReadToEndAsync();
                return JToken.Parse(s);
            }
        }

        private List<string> GetTrackIdsFromJToken(JToken data) {
            return (from tId in data select ((string)tId).Split(':')[0]).ToList();
        }
    }
}
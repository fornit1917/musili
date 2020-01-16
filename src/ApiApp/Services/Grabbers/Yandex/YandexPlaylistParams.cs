namespace Musili.ApiApp.Services.Grabbers.Yandex {
    public struct YandexPlaylistParams {
        public readonly YandexPlaylistType type;
        public readonly string userId;
        public readonly string playlistId;

        public YandexPlaylistParams(YandexPlaylistType type, string playlistId) {
            this.type = type;
            this.userId = null;
            this.playlistId = playlistId;
        }

        public YandexPlaylistParams(YandexPlaylistType type, string userId, string playlistId) {
            this.type = type;
            this.userId = userId;
            this.playlistId = playlistId;
        }
    }
}
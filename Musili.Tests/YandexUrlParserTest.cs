using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.WebApi.Services.Grabbers.Yandex;

namespace Musili.Tests
{
    [TestClass]
    public class YandexUrlParserTest
    {
        private YandexUrlParser parser = new YandexUrlParser();

        [TestMethod]
        public void TestParseUserPlaylistUrl() {
            var result = parser.ParsePlaylistUrl("https://music.yandex.ru/users/username/playlists/1003");
            Assert.AreEqual(YandexPlaylistType.UserPlaylist, result.type);
            Assert.AreEqual("username", result.userId);
            Assert.AreEqual("1003", result.playlistId);
        }

        [TestMethod]
        public void TestParseAlbumUrl() {
            var result = parser.ParsePlaylistUrl("https://music.yandex.ru/artist/123");
            Assert.AreEqual(YandexPlaylistType.Artist, result.type);
            Assert.AreEqual("123", result.playlistId);
        }

        [TestMethod]
        public void TestParseArtistUrl() {
            var result = parser.ParsePlaylistUrl("https://music.yandex.ru/album/123");
            Assert.AreEqual(YandexPlaylistType.Album, result.type);
            Assert.AreEqual("123", result.playlistId);
        }

        [TestMethod]
        public void TestParseIncorrectUrl() {
            Exception expectedException = null;
            try {
                parser.ParsePlaylistUrl("asdwqsdwf");
            } catch (System.Exception e) {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);

            expectedException = null;
            try {
                parser.ParsePlaylistUrl("https://music.yandex.ru/users/vit.fornit.1917/playlists");
            } catch (System.Exception e) {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
        }
    }
}
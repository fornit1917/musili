using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.Tests.Mocks;
using Musili.WebApi.Interfaces;
using Musili.WebApi.Models;

namespace Musili.Tests
{
    [TestClass]
    public class YandexGrabberTest
    {
        private ICommonTracksGrabber grabber = GrabbersMocksFactory.CreateMockedGrabber();

        [TestMethod]
        public void TestGrabFromAlbum() {
            TracksSource tracksSource = new TracksSource() {
                Service = TracksSourceService.Yandex,
                SourceType = TracksSourceType.YandexTracks,
                Genre = Genre.Rock,
                Tempo = Tempo.Soft,
                Value = "https://music.yandex.ru/album/1000"
            };

            List<Track> tracks = grabber.GrabRandomTracksAsync(tracksSource).Result;
            CheckTracks(tracks, 2, tracksSource, DateTime.Now.AddMinutes(50));
        }

        [TestMethod]
        public void TestGrabByArtist() {
            TracksSource tracksSource = new TracksSource() {
                Service = TracksSourceService.Yandex,
                SourceType = TracksSourceType.YandexTracks,
                Genre = Genre.Metal,
                Tempo = Tempo.Soft,
                Value = "https://music.yandex.ru/artist/2000"
            };

            List<Track> tracks = grabber.GrabRandomTracksAsync(tracksSource).Result;
            CheckTracks(tracks, 2, tracksSource, DateTime.Now.AddMinutes(50));            
        }

        [TestMethod]
        public void TestGrabByPlaylist() {
            TracksSource tracksSource = new TracksSource() {
                Service = TracksSourceService.Yandex,
                SourceType = TracksSourceType.YandexTracks,
                Genre = Genre.Electronic,
                Tempo = Tempo.Rhytmic,
                Value = "https://music.yandex.ru/users/UserId/playlists/3000"
            };

            List<Track> tracks = grabber.GrabRandomTracksAsync(tracksSource).Result;
            CheckTracks(tracks, 3, tracksSource, DateTime.Now.AddMinutes(50));                        
        }

        [TestMethod]
        public void TestGrabIncorrectUrl() {
            TracksSource tracksSource = new TracksSource() {
                Service = TracksSourceService.Yandex,
                SourceType = TracksSourceType.YandexTracks,
                Genre = Genre.Electronic,
                Tempo = Tempo.Rhytmic,
                Value = "https://music.yandex.ru/incorrecturl"
            };

            Exception expectedException = null;
            try {
                grabber.GrabRandomTracksAsync(tracksSource).Wait();
            } catch (Exception e) {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void TestNotSupportedSource() {
            TracksSource tracksSource = new TracksSource() {
                Service = TracksSourceService.Yandex,
                SourceType = TracksSourceType.VkGroupWall,
                Genre = Genre.Electronic,
                Tempo = Tempo.Rhytmic,
                Value = "https://music.yandex.ru/incorrecturl"
            };
            
            Exception expectedException = null;
            try {
                grabber.GrabRandomTracksAsync(tracksSource).Wait();
            } catch (Exception e) {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
        }

        private void CheckTracks(List<Track> tracks, int maxCount, TracksSource source, DateTime maxExpDatetime) {
            Assert.IsNotNull(tracks);
            Assert.IsTrue(tracks.Count <= maxCount);
            Assert.IsTrue(tracks.Count > 0);
            
            foreach (Track track in tracks) {
                Assert.IsNotNull(track.Artist);
                Assert.IsNotNull(track.Title);
                Assert.IsNotNull(track.Url);
                Assert.IsNotNull(track.ExpirationDatetime);
                Assert.IsNotNull(track.TracksSource);
                Assert.IsTrue(track.ExpirationDatetime <= maxExpDatetime);
                Assert.AreEqual(source.Genre, track.TracksSource.Genre);
                Assert.AreEqual(source.Tempo, track.TracksSource.Tempo);
                Assert.AreEqual(source.Id, track.TracksSource.Id);
            }
        }
    }
}

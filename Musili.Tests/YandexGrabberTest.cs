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
        private ITracksGrabber grabber = GrabbersMocksFactory.CreateMockedGrabber();

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
            Assert.IsNotNull(tracks);
            Assert.IsTrue(tracks.Count <= 2 && tracks.Count >= 0);
        }
    }
}
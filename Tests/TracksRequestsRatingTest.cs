using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.WebApi.Models;
using Musili.WebApi.Services;

namespace Musili.Tests {
    [TestClass]
    public class TracksRequestsRatingTest {
        [TestMethod]
        public void AddsCriteriasAndReturnsOnlyHot() {
            var tracksRequestsRating = new TracksRequestsRating();
            var hotCriteria = new TracksCriteria("soft", "metal,rock");
            var oldCriteria = new TracksCriteria("soft", "classic");
            var firstDateTime = new DateTime(2000, 1, 1);
            var secondDateTime = new DateTime(1990, 1, 1);
            var minDateTime = new DateTime(1999, 1, 1);

            tracksRequestsRating.AddCriteria(hotCriteria, firstDateTime);
            tracksRequestsRating.AddCriteria(oldCriteria, secondDateTime);
            TracksCriteria[] result = tracksRequestsRating.GetHotCriterias(minDateTime);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2, result[0].Genres.Count);
            Assert.AreEqual(Genre.Metal, result[0].Genres[0]);
            Assert.AreEqual(Genre.Rock, result[0].Genres[1]);

            Assert.AreEqual(1, result[0].Tempos.Count);
            Assert.AreEqual(Tempo.Soft, result[0].Tempos[0]);
        }

        [TestMethod]
        public void UpdatesRequestTime() {
            var tracksRequestsRating = new TracksRequestsRating();
            var hotCriteria = new TracksCriteria("soft", "metal,rock");
            var firstDateTime = new DateTime(2000, 1, 1);
            var secondDateTime = new DateTime(2010, 1, 1);
            var minDateTime = new DateTime(2005, 1, 1);

            tracksRequestsRating.AddCriteria(hotCriteria, firstDateTime);
            tracksRequestsRating.AddCriteria(hotCriteria, secondDateTime);
            TracksCriteria[] result = tracksRequestsRating.GetHotCriterias(minDateTime);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void RemovesOldItems() {
            var tracksRequestsRating = new TracksRequestsRating();
            var hotCriteria = new TracksCriteria("soft", "metal,rock");
            var oldCriteria = new TracksCriteria("soft", "classic");
            var firstDateTime = new DateTime(2000, 1, 1);
            var secondDateTime = new DateTime(1990, 1, 1);
            var minDateTime = new DateTime(1999, 1, 1);

            tracksRequestsRating.AddCriteria(hotCriteria, firstDateTime);
            tracksRequestsRating.AddCriteria(oldCriteria, secondDateTime);
            tracksRequestsRating.RemoveOldRequests(minDateTime);
            TracksCriteria[] result = tracksRequestsRating.GetHotCriterias(new DateTime(1980, 1, 1));

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2, result[0].Genres.Count);
            Assert.AreEqual(Genre.Metal, result[0].Genres[0]);
            Assert.AreEqual(Genre.Rock, result[0].Genres[1]);

            Assert.AreEqual(1, result[0].Tempos.Count);
            Assert.AreEqual(Tempo.Soft, result[0].Tempos[0]);
        }
    }
}

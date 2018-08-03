using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.WebApi.Models;

namespace Musili.Tests
{
    [TestClass]
    public class TracksCriteriaSetTest
    {
        [TestMethod]
        public void TestParse() {
            var criteriaSet = new TracksCriteriaSet("", "");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteriaSet("any", "any");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteriaSet("sadsad", "asdsad,asd");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);            
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteriaSet("soft", "rock,metal");
            Assert.AreEqual(1, criteriaSet.Tempos.Count);
            Assert.AreEqual(2, criteriaSet.Genres.Count);                        

            criteriaSet = new TracksCriteriaSet("soft,rhytmic,any", "rock,metal,classical,jazz,electronic");
            Assert.AreEqual(2, criteriaSet.Tempos.Count);
            Assert.AreEqual(5, criteriaSet.Genres.Count);
        }

        [TestMethod]
        public void TestIsAnyTempo() {
            var criteriaSet = new TracksCriteriaSet("any", "rock,");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteriaSet("", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteriaSet("asdsa", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteriaSet("soft", "");
            Assert.AreEqual(false, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteriaSet("soft,rhytmic", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);
        }

        [TestMethod]
        public void TestIsAnyGenre() {
            var criteriaSet = new TracksCriteriaSet("any", "any");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteriaSet("", "");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteriaSet("soft", "asdsdad");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteriaSet("soft", "rock");
            Assert.AreEqual(false, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteriaSet("soft,rhytmic", "rock,jazz");
            Assert.AreEqual(false, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteriaSet("soft,rhytmic", String.Join(',', Enum.GetNames(typeof(Genre)).Where(item => item != "Any").ToArray()));
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);            
        }
    }
}
